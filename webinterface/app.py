import asyncio
import logging
from typing import List
from mediatransfer import media_files, subtitle_files
import fsconfig
from mediatransfer import send_file
from aiohttp import web
from magnetlinkscraper import t1337x_search
from magnetlinkscraper import solidtorrent_search
from distutils.util import strtobool
from concurrent.futures import ThreadPoolExecutor
from mediatransfer.listfilesmediasystem import list_files_mediasystem

from qbittorrentinterface import delete_torrent, get_running_torrents, pause_torrent, add_torrent, resume_torrent

media_transfer_jobs = []

routes = web.RouteTableDef()


@routes.post('/api/mediatransfer/start')
async def _start_job(request):
    r = await request.json()

    file_name = r["fileName"]

    media_transfer_jobs.append((asyncio.create_task(send_file(file_name)), file_name))

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

    return web.json_response(files)


@routes.get('/api/media/list')
async def _list_media(request):
    get_duration = request.query["duration"]

    get_duration = bool(strtobool(get_duration))

    response = await media_files(get_duration)

    return web.json_response(response)

@routes.get('/api/media-system/list')
async def _list_medimediasystem(request):
    response = await list_files_mediasystem()

    return web.json_response(response)


@routes.get('/api/media/list/subs')
async def _list_subs(request):
    response = await subtitle_files()

    return web.json_response(response)


@routes.get('/api/torrents/list')
async def _list_torrents(request):
    files: List[dict] = await get_running_torrents()

    return web.json_response(files)


@routes.post('/api/torrents/add')
async def _add_torrents(request):
    r = await request.json()

    for magnet_link in r["magnetLinks"]:
        response = await add_torrent(magnet_link)

    return web.Response(text="torrents added")


@routes.post('/api/torrents/resume')
async def _resume_torrents(request):
    r = await request.json()

    for hash in r["hashes"]:
        response = await resume_torrent(hash)

    return web.Response(text="torrents resumed")


@routes.post('/api/torrents/pause')
async def pause_torrents(request):
    r = await request.json()

    for hash in r["hashes"]:
        response = await pause_torrent(hash)

    return web.Response(text="torrents paused")


@routes.post('/api/torrents/delete')
async def _delete_torrents(request):
    r = await request.json()

    for hash in r["hashes"]:
        response = await delete_torrent(hash)

    return web.Response(text="torrents deleted")


@routes.get('/api/search/{torrent_site}/{search_term}')
async def _search_torrents(request):
    search_term = request.match_info['search_term']
    torrent_site = request.match_info['torrent_site']

    if torrent_site == "t1337x":
        results = t1337x_search(search_term)
    elif torrent_site == "solid":
        results = solidtorrent_search(search_term)

    return web.json_response(results)


def start_webinterface(config: dict):
    host_name = fsconfig.CONFIG["fullsail-hostname"]
    port = fsconfig.CONFIG["fullsail-port"]

    app = web.Application()

    logging.basicConfig(level=logging.DEBUG)

    app.add_routes(routes)

    web.run_app(app, host=host_name, port=port)
