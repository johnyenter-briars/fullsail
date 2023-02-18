from magnetlinkscraper import solidtorrent_search
from qbittorrentinterface import start_qbittorrentinterface
from filetransfer import send_file
import json


def main():
    with open('config.json') as f:
        config = json.load(f)
        ms_username = config["media-system-username"]
        ms_password = config["media-system-password"]
        # query = "avengers end game"
        # # t1337x_search(query)
        # solidtorrent_search(query)
        # start_qbittorrentinterface()
        send_file(ms_username, ms_password)


if __name__ == "__main__":
    main()
