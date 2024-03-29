import json


class MagnetLinkSearchResult(dict):
    def __init__(self, magnet_link, number_seeders=None, number_leechers=None, name=None, number_downloads=None, size=None, date_posted=None) -> None:
        dict.__init__(self, magnet_link=magnet_link,
                      number_seeders=number_seeders,
                      number_leechers=number_leechers,
                      name=name,
                      number_downloads=number_downloads,
                      size=size,
                      date_posted=date_posted,
                      )
