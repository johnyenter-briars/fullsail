# class QBitTorrentFile:

#     # def __init__(self, added_on: int, amount_left: int, auto_tmm: bool, availability: int, category: str, completed: int, completion_on: int, content_path: str, dl_limit: int, dlspeed: int, download_path: str, downloaded: int, downloaded_session: int, eta: int, f_l_piece_prio: bool, force_start: bool, hash: str, infohash_v1: str, infohash_v2: str, last_activity: int, magnet_uri: str, max_ratio: int, max_seeding_time: int, name: str, num_complete: int, num_incomplete: int, num_leechs: int, num_seeds: int, priority: int, progress: int, ratio: int, ratio_limit: int, save_path: str, seeding_time: int, seeding_time_limit: int, seen_complete: int, seq_dl: bool, size: int, state: str, super_seeding: bool, tags: str, time_active: int, total_size: int, tracker: str, trackers_count: int, up_limit: int, uploaded: int, uploaded_session: int, upspeed: int) -> None:
#     def __init__(self, in_dict) -> None:
#         assert isinstance(in_dict, dict)
#         for key, val in in_dict.items():
#             if isinstance(val, (list, tuple)):
#                setattr(self, key, [QBitTorrentFile(x) if isinstance(x, dict) else x for x in val])
#             else:
#                setattr(self, key, QBitTorrentFile(val) if isinstance(val, dict) else val)

#         # dict.__init__(self, added_on=added_on, amount_left=amount_left, auto_tmm=auto_tmm, availability=availability, category=category, completed=completed, completion_on=completion_on, content_path: str, dl_limit: int, dlspeed: int, download_path: str, downloaded: int, downloaded_session: int, eta: int, f_l_piece_prio: bool, force_start: bool, hash: str, infohash_v1: str, infohash_v2: str, last_activity: int, magnet_uri: str, max_ratio: int, max_seeding_time: int, name: str, num_complete: int, num_incomplete: int, num_leechs: int, num_seeds: int, priority: int, progress: int, ratio: int, ratio_limit: int, save_path: str, seeding_time: int, seeding_time_limit: int, seen_complete: int, seq_dl: bool, size: int, state: str, super_seeding: bool, tags: str, time_active: int, total_size: int, tracker: str, trackers_count: int, up_limit: int, uploaded: int, uploaded_session: int, upspeed: int)
#         # self.added_on = added_on
#         # self.amount_left = amount_left
#         # self.auto_tmm = auto_tmm
#         # self.availability = availability
#         # self.category = category
#         # self.completed = completed
#         # self.completion_on = completion_on
#         # self.content_path = content_path
#         # self.dl_limit = dl_limit
#         # self.dlspeed = dlspeed
#         # self.download_path = download_path
#         # self.downloaded = downloaded
#         # self.downloaded_session = downloaded_session
#         # self.eta = eta
#         # self.f_l_piece_prio = f_l_piece_prio
#         # self.force_start = force_start
#         # self.hash = hash
#         # self.infohash_v1 = infohash_v1
#         # self.infohash_v2 = infohash_v2
#         # self.last_activity = last_activity
#         # self.magnet_uri = magnet_uri
#         # self.max_ratio = max_ratio
#         # self.max_seeding_time = max_seeding_time
#         # self.name = name
#         # self.num_complete = num_complete
#         # self.num_incomplete = num_incomplete
#         # self.num_leechs = num_leechs
#         # self.num_seeds = num_seeds
#         # self.priority = priority
#         # self.progress = progress
#         # self.ratio = ratio
#         # self.ratio_limit = ratio_limit
#         # self.save_path = save_path
#         # self.seeding_time = seeding_time
#         # self.seeding_time_limit = seeding_time_limit
#         # self.seen_complete = seen_complete
#         # self.seq_dl = seq_dl
#         # self.size = size
#         # self.state = state
#         # self.super_seeding = super_seeding
#         # self.tags = tags
#         # self.time_active = time_active
#         # self.total_size = total_size
#         # self.tracker = tracker
#         # self.trackers_count = trackers_count
#         # self.up_limit = up_limit
#         # self.uploaded = uploaded
#         # self.uploaded_session = uploaded_session
#         # self.upspeed = upspeed

from typing import Any
from dataclasses import dataclass
import json


@dataclass
class QBitTorrentFile:
    added_on: int
    amount_left: int
    auto_tmm: bool
    availability: int
    category: str
    completed: int
    completion_on: int
    content_path: str
    dl_limit: int
    dlspeed: int
    download_path: str
    downloaded: int
    downloaded_session: int
    eta: int
    f_l_piece_prio: bool
    force_start: bool
    hash: str
    infohash_v1: str
    infohash_v2: str
    last_activity: int
    magnet_uri: str
    max_ratio: int
    max_seeding_time: int
    name: str
    num_complete: int
    num_incomplete: int
    num_leechs: int
    num_seeds: int
    priority: int
    progress: int
    ratio: int
    ratio_limit: int
    save_path: str
    seeding_time: int
    seeding_time_limit: int
    seen_complete: int
    seq_dl: bool
    size: int
    state: str
    super_seeding: bool
    tags: str
    time_active: int
    total_size: int
    tracker: str
    trackers_count: int
    up_limit: int
    uploaded: int
    uploaded_session: int
    upspeed: int

    @staticmethod
    def from_dict(obj: Any) -> 'QBitTorrentFile':
        _added_on = int(obj.get("added_on"))
        _amount_left = int(obj.get("amount_left"))
        _auto_tmm  = int(obj.get("auto_tmm"))
        _availability = int(obj.get("availability"))
        _category = str(obj.get("category"))
        _completed = int(obj.get("completed"))
        _completion_on = int(obj.get("completion_on"))
        _content_path = str(obj.get("content_path"))
        _dl_limit = int(obj.get("dl_limit"))
        _dlspeed = int(obj.get("dlspeed"))
        _download_path = str(obj.get("download_path"))
        _downloaded = int(obj.get("downloaded"))
        _downloaded_session = int(obj.get("downloaded_session"))
        _eta = int(obj.get("eta"))
        _f_l_piece_prio = str(obj.get("f_l_piece_prio"))
        _force_start  = str(obj.get("force_start"))
        _hash = str(obj.get("hash"))
        _infohash_v1 = str(obj.get("infohash_v1"))
        _infohash_v2 = str(obj.get("infohash_v2"))
        _last_activity = int(obj.get("last_activity"))
        _magnet_uri = str(obj.get("magnet_uri"))
        _max_ratio = int(obj.get("max_ratio"))
        _max_seeding_time = int(obj.get("max_seeding_time"))
        _name = str(obj.get("name"))
        _num_complete = int(obj.get("num_complete"))
        _num_incomplete = int(obj.get("num_incomplete"))
        _num_leechs = int(obj.get("num_leechs"))
        _num_seeds = int(obj.get("num_seeds"))
        _priority = int(obj.get("priority"))
        _progress = int(obj.get("progress"))
        _ratio = int(obj.get("ratio"))
        _ratio_limit = int(obj.get("ratio_limit"))
        _save_path = str(obj.get("save_path"))
        _seeding_time = int(obj.get("seeding_time"))
        _seeding_time_limit = int(obj.get("seeding_time_limit"))
        _seen_complete = int(obj.get("seen_complete"))
        _seq_dl  = str(obj.get("seq_dl"))
        _size = int(obj.get("size"))
        _state = str(obj.get("state"))
        _super_seeding = str(obj.get("super_seeding"))
        _tags = str(obj.get("tags"))
        _time_active = int(obj.get("time_active"))
        _total_size = int(obj.get("total_size"))
        _tracker = str(obj.get("tracker"))
        _trackers_count = int(obj.get("trackers_count"))
        _up_limit = int(obj.get("up_limit"))
        _uploaded = int(obj.get("uploaded"))
        _uploaded_session = int(obj.get("uploaded_session"))
        _upspeed = int(obj.get("upspeed"))
        return QBitTorrentFile(_added_on, _amount_left, _auto_tmm, _availability, _category, _completed, _completion_on, _content_path, _dl_limit, _dlspeed, _download_path, _downloaded, _downloaded_session, _eta, _f_l_piece_prio, _force_start, _hash, _infohash_v1, _infohash_v2, _last_activity, _magnet_uri, _max_ratio, _max_seeding_time, _name, _num_complete, _num_incomplete, _num_leechs, _num_seeds, _priority, _progress, _ratio, _ratio_limit, _save_path, _seeding_time, _seeding_time_limit, _seen_complete, _seq_dl, _size, _state, _super_seeding, _tags, _time_active, _total_size, _tracker, _trackers_count, _up_limit, _uploaded, _uploaded_session, _upspeed)

