import asyncio
from mediatransfer.listfiles import list_files
import fsconfig
from mediatransfer import send_file
from aiohttp import web
from magnetlinkscraper import t1337x_search
from magnetlinkscraper import solidtorrent_search
import json
import jsonpickle
from models import StartTorrentRequest

from qbittorrentinterface.methods import get_running_torrents, start_torrent

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
    resposne = await list_files()
    return web.Response(text=f"num futures:{len(futures)}")


@routes.get('/api/torrents/list')
async def list_torrents(request):

    files = await get_running_torrents()

    response = jsonpickle.encode(files)

    return web.json_response(response)

@routes.post('/api/torrents/start')
async def list_torrents(request):
    request = StartTorrentRequest.from_dict(await request.json())

    response = await start_torrent(request.magnet_link)

    return web.Response(response)

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
