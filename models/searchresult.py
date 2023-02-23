import json


class SearchResult(dict):
    def __init__(self, torrent_name, number_seeders) -> None:
        dict.__init__(self, torrent_name=torrent_name, number_seeders=number_seeders)
        # self.torrent_name:str = torrent_name
        # self.number_seeders: int = number_seeders 

    # def toJSON(self):
    #     return json.dumps(self, default=lambda o: o.__dict__,
    #                       sort_keys=True, indent=4)
