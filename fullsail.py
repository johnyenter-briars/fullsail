from typing import Dict
from magnetlinkscraper import solidtorrent_search
from qbittorrentinterface import start_qbittorrentinterface
from filetransfer import send_file
import json
import asyncio
from asyncio import Task
import fsconfig


CONFIG = {}


def main():
    with open('config.json') as f:
        config = json.load(f)

        fsconfig.CONFIG = {key:value for key,value in config.items()}

        start_qbittorrentinterface(config)

if __name__ == "__main__":
    # asyncio.run(main())
    main()
