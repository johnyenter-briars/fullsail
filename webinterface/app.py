import asyncio
from typing import List
from mediatransfer.listfiles import list_files
import fsconfig
from mediatransfer import send_file
from aiohttp import web
from magnetlinkscraper import t1337x_search
from magnetlinkscraper import solidtorrent_search
import json
import jsonpickle
from models import StartTorrentsRequest
from models import UpdateTorrentsRequest
from models.qbtfile import QBTFile, current_torrents_response_from_dict, current_torrents_response_to_dict

from qbittorrentinterface.methods import delete_torrent, get_running_torrents, pause_torrent, add_torrent, resume_torrent

futures = []
future_id = 0

routes = web.RouteTableDef()


@routes.get('/api/job/start')
async def start_job(request):
    future = asyncio.create_task(schedule_send_file_job())

    futures.append(future)

    global future_id
    future_id += 1

    return web.Response(text=f"Future scheduled!")


async def schedule_send_file_job():
    for i in range(10, 20):
        await send_file(f"test{i}-{future_id}.txt")

    futures.remove(asyncio.current_task())


@routes.get('/api/jobs/list')
async def list_jobs(request):
    return web.Response(text=f"num futures:{len(futures)}")


@routes.get('/api/media/list')
async def list_jobs(request):
    response = await list_files()

    return web.json_response(response)


@routes.get('/api/torrents/list')
async def list_torrents(request):
    files: List[dict] = await get_running_torrents()

    return web.json_response(files)


@routes.post('/api/torrents/add')
async def add_torrents(request):
    r = StartTorrentsRequest.from_dict(await request.json())

    for magnet_link in r.magnet_links:
        response = await add_torrent(magnet_link)

    return web.Response(text="idk")


@routes.post('/api/torrents/resume')
async def resume_torrents(request):
    r = UpdateTorrentsRequest.from_dict(await request.json())

    for hash in r.hashes:
        response = await resume_torrent(hash)

    return web.Response(text="idk")


@routes.post('/api/torrents/pause')
async def pause_torrents(request):
    r = UpdateTorrentsRequest.from_dict(await request.json())

    for hash in r.hashes:
        response = await pause_torrent(hash)

    return web.Response(text="idk")


@routes.post('/api/torrents/delete')
async def delete_torrents(request):
    r = UpdateTorrentsRequest.from_dict(await request.json())

    for hash in r.hashes:
        response = await delete_torrent(hash)

    return web.Response(text="idk")


@routes.get('/api/search/{torrent_site}/{search_term}')
async def search_torrents(request):
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
