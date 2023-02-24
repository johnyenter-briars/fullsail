import asyncio
from typing import List
from mediatransfer.listfiles import media_files, subtitle_files
import fsconfig
from mediatransfer import send_file
from aiohttp import web
from magnetlinkscraper import t1337x_search
from magnetlinkscraper import solidtorrent_search
import json
import jsonpickle
from models.qbtfile import QBTFile, current_torrents_response_from_dict, current_torrents_response_to_dict
from distutils.util import strtobool

from qbittorrentinterface.methods import delete_torrent, get_running_torrents, pause_torrent, add_torrent, resume_torrent

media_transfer_jobs = []

routes = web.RouteTableDef()


@routes.post('/api/mediatransfer/start')
async def _start_job(request):
    r = await request.json()

    file_name = r["fileName"]

    future = asyncio.create_task(_schedule_send_file_job(file_name))

    media_transfer_jobs.append(future)

    return web.Response(text=f"Future scheduled!")


async def _schedule_send_file_job(file_name):
    await send_file(file_name)

    media_transfer_jobs.remove(asyncio.current_task())


@routes.get('/api/mediatransfer/listjobs')
async def _list_jobs(request):
    return web.Response(text=f"num futures:{len(media_transfer_jobs)}")


@routes.get('/api/media/list')
async def _list_media(request):
    get_duration = request.query["duration"]

    get_duration = bool(strtobool(get_duration))

    response = await media_files(get_duration)

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

    app.add_routes(routes)

    web.run_app(app, host=host_name, port=port)
