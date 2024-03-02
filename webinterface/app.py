import psutil
import os
import ssl
import asyncio
import logging
import subprocess
from typing import Awaitable, Callable, List
from magnetlinkscraper.piratebay import piratebay_search
import fsconfig
from mediatransfer import send_file_to_mediasystem
from aiohttp import web
from magnetlinkscraper import t1337x_search
from magnetlinkscraper import solidtorrent_search
from mediatransfer.deletefilemediastore import delete_item
from mediatransfer.deletefilemediasystem import delete_file_mediasystem
from mediatransfer.listfilesmediastore import list_media_files_in_folder
from mediatransfer.listfilesmediasystem import list_files_mediasystem
from mediatransfer.movefilemediastore import move_item
from mediatransfer.sendfile import send_file_to_laptop
from qbittorrentinterface import delete_torrent, get_running_torrents, pause_torrent, add_torrent, resume_torrent
from subtitle_api.opensubtitles import open_subtitles_download, open_subtitles_search
from typing import Tuple
import re

media_transfer_jobs = []

routes = web.RouteTableDef()


def vpn_running() -> Tuple[bool, str]:
    country = ""
    connected = False

    ps_output = subprocess.check_output(
        f'ps -aux | grep openvpn',
        shell=True, 
        stderr=subprocess.STDOUT
    )

    proton_running = "protonvpn" in str(ps_output)

    if not proton_running:
        return (False, country)

    pattern = r'--config ([a-zA-Z0-9-]+)\.'

    match = re.search(pattern, ps_output.decode())

    if match:
        vpn_location = match.group(1).split('-')[0]

        try:
            subprocess.check_output(['ping', '-c', '1', '-W', '2', '8.8.8.8'])
            return (True, vpn_location)
        except subprocess.CalledProcessError:
            return (False, "")
    else:
        return (False, "")

@routes.get('/api/healthcheck')
async def _health_check(request):
    connected, country = vpn_running()

    cpu_usage = psutil.cpu_percent()
    gb_memory = psutil.virtual_memory()[3]/1000000000

    return web.json_response({
        "nordStatus": connected,
        "country": country,
        "cpuUsed": cpu_usage,
        "memoryUsed": f"{round(gb_memory, 1)} GB",
    })


@routes.post('/api/mediatransfer/start')
async def _start_job(request):
    r = await request.json()

    file_name = r["fileName"]
    computerDestination = r["computerDestination"]

    if computerDestination == "laptop":
        media_transfer_jobs.append(
            (asyncio.create_task(send_file_to_laptop(file_name)), f"{file_name}-{computerDestination}"))
    elif computerDestination == "mediaSystem":
        media_transfer_jobs.append(
            (asyncio.create_task(send_file_to_mediasystem(file_name)), f"{file_name}-{computerDestination}"))

    return web.json_response({
        "numberRunningJobs": len(media_transfer_jobs)
    })


@routes.get('/api/mediatransfer/listjobs')
async def _list_jobs(request):
    done_jobs = list(filter(lambda j: j[0].done(), media_transfer_jobs))

    if len(done_jobs) != 0:
        for job in done_jobs:
            media_transfer_jobs.remove(job)

    files = [job[1] for job in media_transfer_jobs]

    return web.json_response({"message": "jobs found", "jobs": files})


@routes.get('/api/media/list/{folder}')
async def _list_media_in_folder(request):
    folder = request.match_info['folder']

    files = await list_media_files_in_folder(folder)

    return web.json_response(files)


@routes.delete('/api/media/delete')
async def _delete_media_file(request):
    r = await request.json()

    file_name = r["fileName"]

    await delete_item(file_name)

    return web.json_response({
        "message": "file deleted"
    })


@routes.post('/api/media/move')
async def _move_media_file(request):
    r = await request.json()

    file_name = r["fileName"]
    destination = r["destination"]

    moved_successfully = await move_item(file_name, destination)

    return web.json_response({
        "message": f"Moved successfully: {moved_successfully}"
    })


@routes.get('/api/media-system/list')
async def _list_medimediasystem(request):
    response = await list_files_mediasystem()

    return web.json_response(response)


@routes.delete('/api/media-system/delete')
async def _delete_files_mediasystem(request):
    r = await request.json()

    file_name = r["fileName"]

    await delete_file_mediasystem(file_name)

    return web.json_response({"message": "file deleted"})


@routes.get('/api/torrents/list')
async def _list_torrents(request):
    files: List[dict] = await get_running_torrents()

    return web.json_response({"running_torrents": files})


@routes.post('/api/torrents/add')
async def _add_torrents(request):
    if fsconfig.CONFIG["check-vpn"] and not vpn_running()[0]:
        print("VPN is not running!")
        raise web.HTTPInternalServerError

    r = await request.json()

    for magnet_link in r["magnetLinks"]:
        _ = await add_torrent(magnet_link)

    return web.json_response({"message": "torrents added"})


@routes.post('/api/torrents/resume')
async def _resume_torrents(request):
    r = await request.json()

    for hash in r["hashes"]:
        _ = await resume_torrent(hash)

    return web.json_response({"message": "torrents resumed"})


@routes.post('/api/torrents/pause')
async def pause_torrents(request):
    r = await request.json()

    for hash in r["hashes"]:
        _ = await pause_torrent(hash)

    return web.json_response({"message": "torrents paused"})


@routes.post('/api/torrents/delete')
async def _delete_torrents(request):
    r = await request.json()

    for hash in r["hashes"]:
        _ = await delete_torrent(hash)

    return web.json_response({"message": "torrents deleted"})


@routes.get('/api/search/magnetlink/{torrent_site}/{search_term}')
async def _search_torrents(request):
    search_term = request.match_info['search_term']
    torrent_site = request.match_info['torrent_site']

    if torrent_site == "t1337x":
        results = t1337x_search(search_term)
    elif torrent_site == "solid":
        results = solidtorrent_search(search_term)
    elif torrent_site == "piratebay":
        results = piratebay_search(search_term)

    return web.json_response(results)

@routes.post('/api/subtitle/download/{type}/{link}')
async def _download_subtitle(request):
    subtitle_type = request.match_info['type']
    link = request.match_info['link']

    if subtitle_type == "opensubtitles":
        open_subtitles_download(link)

    return web.json_response({"message": "man I hope this works"})



@routes.get('/api/search/subtitle/{type}/{search_term}')
async def _search_subtitles(request):
    search_term = request.match_info['search_term']
    type = request.match_info['type']
    if type == "opensubtitles":
        results = open_subtitles_search(search_term)
    else:
        results = []

    return web.json_response(results)


@web.middleware
async def api_key_middleware(request: web.Request,
                             handler: Callable[[web.Request], Awaitable[web.Response]]):
    api_key = request.headers['x-api-key']

    if api_key == fsconfig.CONFIG["fullsail-api-key"]:
        return await handler(request)
    else:
        raise web.HTTPUnauthorized


def start_webinterface(config: dict):
    if fsconfig.CONFIG["check-vpn"] and not vpn_running()[0]:
        raise Exception("VPN is not running!")

    host_name = fsconfig.CONFIG["fullsail-hostname"]
    port = fsconfig.CONFIG["fullsail-port"]

    app = web.Application(middlewares=[api_key_middleware])

    logging.basicConfig(level=logging.INFO)
    asyncssh_logger = logging.getLogger("asyncssh")
    asyncssh_logger.setLevel(logging.WARNING)

    ssl_context = ssl.create_default_context(ssl.Purpose.CLIENT_AUTH)

    ssl_context.load_cert_chain(
        fsconfig.CONFIG["fullsail-cert-name"], fsconfig.CONFIG["fullsail-key-name"])

    app.add_routes(routes)

    web.run_app(app, ssl_context=ssl_context, host=host_name, port=port)
