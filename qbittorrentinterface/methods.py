from typing import List
import aiohttp
import requests
import json
from json import JSONEncoder
import json_tricks as json_tricks
import pickle
import jsonpickle

from qbittorrentinterface.qbittorrentfile import QBitTorrentFile


async def get_running_torrents() -> List[QBitTorrentFile]:
    async with aiohttp.ClientSession() as session:
        qbittorrent_url = 'http://localhost:8080/api/v2/torrents/info'
        async with session.get(qbittorrent_url) as resp:
            raw_json: List[dict] = await resp.json()

            return [
                QBitTorrentFile.from_dict(idk) for idk in raw_json]
