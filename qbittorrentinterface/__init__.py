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


async def get_index(request):
    username = fsconfig.CONFIG["media-system-username"]
    password = fsconfig.CONFIG["media-system-password"]

    # future = asyncio.create_task(
    #     doesnt_block_current_thread(username, password, fsconfig.CONFIG, f"test11.txt"))

    future = asyncio.create_task(
        blocks_current_thread(username, password, fsconfig.CONFIG, f"test11.txt"))

    futures.append(future)

    global future_id
    future_id += 1

    return web.Response(text=f"num futures:{len(futures)}")


def start_qbittorrentinterface(config: dict):
    app = web.Application()
    app.add_routes([web.get('/', get_index)])
    web.run_app(app, host="localhost", port=8082)


async def doesnt_block_current_thread(username: str, password: str, config: dict, file_name: str):
    for i in range(10, 20):
        await asyncio.sleep(1)
        print(f"sent file: {i}")

    futures.remove(asyncio.current_task())


async def blocks_current_thread(username: str, password: str, config: dict, file_name: str, result=None):
    for i in range(10, 20):
        await send_file(username, password, fsconfig.CONFIG, f"test{i}-{future_id}.txt")

    futures.remove(asyncio.current_task())
