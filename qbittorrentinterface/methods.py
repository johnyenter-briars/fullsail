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

from models.qbtfile import QBTFile


async def get_running_torrents() -> List[QBTFile]:
    async with aiohttp.ClientSession() as session:
        qbittorrent_url = 'http://localhost:8080/api/v2/torrents/info'
        async with session.get(qbittorrent_url) as resp:
            foo = await resp.json()
            print(foo)
            raw_json: List[dict] = await resp.json()
            parsed = [QBTFile.from_dict(d) for d in raw_json]
            return parsed


async def add_torrent(magnet_link: str) -> str:
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
            return response_text


async def pause_torrent(hash: str) -> str:
    url = "http://localhost:8080/api/v2/torrents/pause"
    headers = {
        'Content-type': 'multipart/form-data; boundary=wL36Yn8afVp8Ag7AmP8qZ0SA4n1v9T'
    }

    dataList = []
    boundary = 'wL36Yn8afVp8Ag7AmP8qZ0SA4n1v9T'
    dataList.append(encode('--' + boundary))
    dataList.append(encode('Content-Disposition: form-data; name=hashes;'))
    dataList.append(encode('Content-Type: {}'.format('text/plain')))
    dataList.append(encode(''))
    dataList.append(encode(hash))
    dataList.append(encode('--'+boundary+'--'))
    dataList.append(encode(''))
    body = b'\r\n'.join(dataList)
    payload = body

    async with aiohttp.ClientSession() as session:
        async with session.post(url, headers=headers, data=payload) as response:
            response_text = await response.text()
            return response_text


async def resume_torrent(hash: str) -> str:
    url = "http://localhost:8080/api/v2/torrents/resume"
    headers = {
        'Content-type': 'multipart/form-data; boundary=wL36Yn8afVp8Ag7AmP8qZ0SA4n1v9T'
    }

    dataList = []
    boundary = 'wL36Yn8afVp8Ag7AmP8qZ0SA4n1v9T'
    dataList.append(encode('--' + boundary))
    dataList.append(encode('Content-Disposition: form-data; name=hashes;'))
    dataList.append(encode('Content-Type: {}'.format('text/plain')))
    dataList.append(encode(''))
    dataList.append(encode(hash))
    dataList.append(encode('--'+boundary+'--'))
    dataList.append(encode(''))
    body = b'\r\n'.join(dataList)
    payload = body

    async with aiohttp.ClientSession() as session:
        async with session.post(url, headers=headers, data=payload) as response:
            response_text = await response.text()
            return response_text


async def delete_torrent(hash: str):
    url = "http://localhost:8080/api/v2/torrents/delete"
    boundary = 'wL36Yn8afVp8Ag7AmP8qZ0SA4n1v9T'
    headers = {
        'Content-type': 'multipart/form-data; boundary={}'.format(boundary)}

    async with aiohttp.ClientSession() as session:
        dataList = []
        dataList.append('--' + boundary)
        dataList.append('Content-Disposition: form-data; name=hashes;')
        dataList.append('Content-Type: {}'.format('text/plain'))
        dataList.append('')
        dataList.append(hash)

        dataList.append('--' + boundary)
        dataList.append('Content-Disposition: form-data; name=deleteFiles;')
        dataList.append('Content-Type: {}'.format('text/plain'))
        dataList.append('')
        dataList.append("false")

        dataList.append('--'+boundary+'--')
        dataList.append('')

        body = "\r\n".join(dataList)
        async with session.post(url, data=body, headers=headers) as resp:
            data = await resp.text()
            print(data)
