from codecs import encode
import sys
from typing import List
import aiohttp
import requests
import json
from json import JSONEncoder
import json_tricks as json_tricks
import pickle
import jsonpickle

from models import QBitTorrentFile


async def get_running_torrents() -> List[QBitTorrentFile]:
    async with aiohttp.ClientSession() as session:
        qbittorrent_url = 'http://localhost:8080/api/v2/torrents/info'
        async with session.get(qbittorrent_url) as resp:
            raw_json: List[dict] = await resp.json()

            return [
                QBitTorrentFile.from_dict(idk) for idk in raw_json]


async def start_torrent(magnet_link: str) -> str:
    url = "http://localhost:8080/api/v2/torrents/add"
    headers = {
        'Content-type': 'multipart/form-data; boundary=wL36Yn8afVp8Ag7AmP8qZ0SA4n1v9T'
    }

    dataList = []
    boundary = 'wL36Yn8afVp8Ag7AmP8qZ0SA4n1v9T'
    dataList.append(encode('--' + boundary))
    dataList.append(encode('Content-Disposition: form-data; name=urls;'))
    dataList.append(encode('Content-Type: {}'.format('text/plain')))
    dataList.append(encode(''))
    dataList.append(encode(magnet_link))
    dataList.append(encode('--'+boundary+'--'))
    dataList.append(encode(''))
    body = b'\r\n'.join(dataList)
    payload = body

    async with aiohttp.ClientSession() as session:
        async with session.post(url, headers=headers, data=payload) as response:
            response_text = await response.text()
            print(response_text)


async def pause_torrent(hashes: List[str]) -> str:
    async with aiohttp.ClientSession() as session:
        qbittorrent_url = 'http://localhost:8080/api/v2/torrents/pause'
        headers = {"Content-Type": "multipart/form-data"}
        data = {"urls": None}
        async with session.post(qbittorrent_url, headers=headers, data=data) as resp:
            response = await resp.text()

            return response


async def delete_torrent(hashes: List[str]) -> str:
    async with aiohttp.ClientSession() as session:
        qbittorrent_url = 'http://localhost:8080/api/v2/torrents/delete'
        headers = {"Content-Type": "multipart/form-data"}
        data = {"urls": None}
        async with session.post(qbittorrent_url, headers=headers, data=data) as resp:
            response = await resp.text()

            return response
