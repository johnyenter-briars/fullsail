import asyncio
import fsconfig
from filetransfer import send_file
from aiohttp import web
from magnetlinkscraper import t1337x_search
from magnetlinkscraper import solidtorrent_search
import json
import jsonpickle

from qbittorrentinterface.methods import get_running_torrents

futures = []
future_id = 0

routes = web.RouteTableDef()


@routes.get('/')
async def start_job(request):
    future = asyncio.create_task(blocks_current_thread())

    futures.append(future)

    global future_id
    future_id += 1

    return web.Response(text=f"Future scheduled!")


@routes.get('/list/jobs')
async def list_jobs(request):
    return web.Response(text=f"num futures:{len(futures)}")

@routes.get('/list/torrents')
async def list_torrents(request):

    files = await get_running_torrents()

    response = jsonpickle.encode(files)

    return web.json_response(response)


@routes.get('/search/{torrent_site}/{search_term}')
async def search_torrents(request):
    search_term = request.match_info['search_term']
    torrent_site = request.match_info['torrent_site']

    if torrent_site == "t1337x":
        results = t1337x_search(search_term)
    elif torrent_site == "solid":
        results = solidtorrent_search(search_term)

    data = json.dumps(results)

    return web.json_response(data)


def start_webinterface(config: dict):
    host_name = fsconfig.CONFIG["fullsail-hostname"]
    port = fsconfig.CONFIG["fullsail-port"]

    app = web.Application()

    app.add_routes(routes)

    web.run_app(app, host=host_name, port=port)


async def blocks_current_thread():
    for i in range(10, 20):
        await send_file(f"test{i}-{future_id}.txt")

    futures.remove(asyncio.current_task())
