import json


class SubtitleSearchResult(dict):
    def __init__(self, name, full_link, date_posted=None) -> None:
        dict.__init__(self,
                      name=name,
                      full_link=full_link,
                      date_posted=date_posted,
                      )
