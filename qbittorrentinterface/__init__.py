# from aiopg.sa import create_engine
from asyncio import events
from http.server import BaseHTTPRequestHandler, HTTPServer
import time
import requests
from requests import Response
import json
import asyncio
from asyncio import Task
import fsconfig
from filetransfer import send_file

import json
from uuid import uuid4  # Third-party library
from aiohttp import web

import os
from typing import Dict
import paramiko

hostName = "localhost"
serverPort = 8082


futures = []
future_id = 0


async def start_job(request):
    future = asyncio.create_task(blocks_current_thread())

    futures.append(future)

    global future_id
    future_id += 1

    return web.Response(text=f"Future scheduled!")


async def list_jobs(request):
    return web.Response(text=f"num futures:{len(futures)}")


def start_qbittorrentinterface(config: dict):
    hostname = fsconfig.CONFIG["fullsail-hostname"]
    port = fsconfig.CONFIG["fullsail-port"]

    app = web.Application()
    app.add_routes([web.get('/', start_job)])
    app.add_routes([web.get('/list', list_jobs)])
    web.run_app(app, host=hostName, port=port)


async def blocks_current_thread():
    for i in range(10, 20):
        await send_file(f"test{i}-{future_id}.txt")

    futures.remove(asyncio.current_task())
