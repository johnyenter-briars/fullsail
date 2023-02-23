from typing import Any, List, TypeVar, Callable, Type, cast

from models.qbtfile import QBTFile


T = TypeVar("T")


def from_int(x: Any) -> int:
    assert isinstance(x, int) and not isinstance(x, bool)
    return x


def from_bool(x: Any) -> bool:
    assert isinstance(x, bool)
    return x


def from_float(x: Any) -> float:
    assert isinstance(x, (float, int)) and not isinstance(x, bool)
    return float(x)


def from_str(x: Any) -> str:
    assert isinstance(x, str)
    return x


def to_float(x: Any) -> float:
    assert isinstance(x, float)
    return x


def from_list(f: Callable[[Any], T], x: Any) -> List[T]:
    assert isinstance(x, list)
    return [f(y) for y in x]


def to_class(c: Type[T], x: Any) -> dict:
    assert isinstance(x, c)
    return cast(Any, x).to_dict()


class FileListResponse:
    files: List[QBTFile]

    def __init__(self, files: List[QBTFile] ) -> None:
        self.file = files

    @staticmethod
    def from_dict(obj: Any) -> 'FileListResponse':
        assert isinstance(obj, dict)
        added_on = from_int(obj.get("added_on"))
        amount_left = from_int(obj.get("amount_left"))
        auto_tmm = from_bool(obj.get("auto_tmm"))
        availability = from_float(obj.get("availability"))
        category = from_str(obj.get("category"))
        completed = from_int(obj.get("completed"))
        completion_on = from_int(obj.get("completion_on"))
        content_path = from_str(obj.get("content_path"))
        dl_limit = from_int(obj.get("dl_limit"))
        dlspeed = from_int(obj.get("dlspeed"))
        download_path = from_str(obj.get("download_path"))
        downloaded = from_int(obj.get("downloaded"))
        downloaded_session = from_int(obj.get("downloaded_session"))
        eta = from_int(obj.get("eta"))
        f_l_piece_prio = from_bool(obj.get("f_l_piece_prio"))
        force_start = from_bool(obj.get("force_start"))
        hash = from_str(obj.get("hash"))
        infohash_v1 = from_str(obj.get("infohash_v1"))
        infohash_v2 = from_str(obj.get("infohash_v2"))
        last_activity = from_int(obj.get("last_activity"))
        magnet_uri = from_str(obj.get("magnet_uri"))
        max_ratio = from_int(obj.get("max_ratio"))
        max_seeding_time = from_int(obj.get("max_seeding_time"))
        name = from_str(obj.get("name"))
        num_complete = from_int(obj.get("num_complete"))
        num_incomplete = from_int(obj.get("num_incomplete"))
        num_leechs = from_int(obj.get("num_leechs"))
        num_seeds = from_int(obj.get("num_seeds"))
        priority = from_int(obj.get("priority"))
        progress = from_float(obj.get("progress"))
        ratio = from_float(obj.get("ratio"))
        ratio_limit = from_int(obj.get("ratio_limit"))
        save_path = from_str(obj.get("save_path"))
        seeding_time = from_int(obj.get("seeding_time"))
        seeding_time_limit = from_int(obj.get("seeding_time_limit"))
        seen_complete = from_int(obj.get("seen_complete"))
        seq_dl = from_bool(obj.get("seq_dl"))
        size = from_int(obj.get("size"))
        state = from_str(obj.get("state"))
        super_seeding = from_bool(obj.get("super_seeding"))
        tags = from_str(obj.get("tags"))
        time_active = from_int(obj.get("time_active"))
        total_size = from_int(obj.get("total_size"))
        tracker = from_str(obj.get("tracker"))
        trackers_count = from_int(obj.get("trackers_count"))
        up_limit = from_int(obj.get("up_limit"))
        uploaded = from_int(obj.get("uploaded"))
        uploaded_session = from_int(obj.get("uploaded_session"))
        upspeed = from_int(obj.get("upspeed"))
        return FileListResponse(added_on, amount_left, auto_tmm, availability, category, completed, completion_on, content_path, dl_limit, dlspeed, download_path, downloaded, downloaded_session, eta, f_l_piece_prio, force_start, hash, infohash_v1, infohash_v2, last_activity, magnet_uri, max_ratio, max_seeding_time, name, num_complete, num_incomplete, num_leechs, num_seeds, priority, progress, ratio, ratio_limit, save_path, seeding_time, seeding_time_limit, seen_complete, seq_dl, size, state, super_seeding, tags, time_active, total_size, tracker, trackers_count, up_limit, uploaded, uploaded_session, upspeed)

    def to_dict(self) -> dict:
        result: dict = {}
        result["added_on"] = from_int(self.added_on)
        result["amount_left"] = from_int(self.amount_left)
        result["auto_tmm"] = from_bool(self.auto_tmm)
        result["availability"] = to_float(self.availability)
        result["category"] = from_str(self.category)
        result["completed"] = from_int(self.completed)
        result["completion_on"] = from_int(self.completion_on)
        result["content_path"] = from_str(self.content_path)
        result["dl_limit"] = from_int(self.dl_limit)
        result["dlspeed"] = from_int(self.dlspeed)
        result["download_path"] = from_str(self.download_path)
        result["downloaded"] = from_int(self.downloaded)
        result["downloaded_session"] = from_int(self.downloaded_session)
        result["eta"] = from_int(self.eta)
        result["f_l_piece_prio"] = from_bool(self.f_l_piece_prio)
        result["force_start"] = from_bool(self.force_start)
        result["hash"] = from_str(self.hash)
        result["infohash_v1"] = from_str(self.infohash_v1)
        result["infohash_v2"] = from_str(self.infohash_v2)
        result["last_activity"] = from_int(self.last_activity)
        result["magnet_uri"] = from_str(self.magnet_uri)
        result["max_ratio"] = from_int(self.max_ratio)
        result["max_seeding_time"] = from_int(self.max_seeding_time)
        result["name"] = from_str(self.name)
        result["num_complete"] = from_int(self.num_complete)
        result["num_incomplete"] = from_int(self.num_incomplete)
        result["num_leechs"] = from_int(self.num_leechs)
        result["num_seeds"] = from_int(self.num_seeds)
        result["priority"] = from_int(self.priority)
        result["progress"] = to_float(self.progress)
        result["ratio"] = to_float(self.ratio)
        result["ratio_limit"] = from_int(self.ratio_limit)
        result["save_path"] = from_str(self.save_path)
        result["seeding_time"] = from_int(self.seeding_time)
        result["seeding_time_limit"] = from_int(self.seeding_time_limit)
        result["seen_complete"] = from_int(self.seen_complete)
        result["seq_dl"] = from_bool(self.seq_dl)
        result["size"] = from_int(self.size)
        result["state"] = from_str(self.state)
        result["super_seeding"] = from_bool(self.super_seeding)
        result["tags"] = from_str(self.tags)
        result["time_active"] = from_int(self.time_active)
        result["total_size"] = from_int(self.total_size)
        result["tracker"] = from_str(self.tracker)
        result["trackers_count"] = from_int(self.trackers_count)
        result["up_limit"] = from_int(self.up_limit)
        result["uploaded"] = from_int(self.uploaded)
        result["uploaded_session"] = from_int(self.uploaded_session)
        result["upspeed"] = from_int(self.upspeed)
        return result


def current_torrents_response_from_dict(s: Any) -> List[FileListResponse]:
    return from_list(FileListResponse.from_dict, s)


def current_torrents_response_to_dict(x: List[FileListResponse]) -> Any:
    return from_list(lambda x: to_class(FileListResponse, x), x)

