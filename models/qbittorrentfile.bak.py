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

