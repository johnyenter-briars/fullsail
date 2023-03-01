import json


class SearchResult(dict):
    def __init__(self, magnet_link, number_seeders, number_leechers, name, number_downloads, size, date_posted) -> None:
        dict.__init__(self, magnet_link=magnet_link,
                      number_seeders=number_seeders,
                      number_leechers=number_leechers,
                      name=name,
                      number_downloads=number_downloads,
                      size=size,
                      date_posted=date_posted,
                      )
