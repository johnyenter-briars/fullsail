from typing import Any, TypeVar, Type, cast


T = TypeVar("T")


def from_str(x: Any) -> str:
    assert isinstance(x, str)
    return x


def to_class(c: Type[T], x: Any) -> dict:
    assert isinstance(x, c)
    return cast(Any, x).to_dict()


class StartTorrentRequest:
    magnet_link: str

    def __init__(self, magnet_link: str) -> None:
        self.magnet_link = magnet_link

    @staticmethod
    def from_dict(obj: Any) -> 'StartTorrentRequest':
        assert isinstance(obj, dict)
        magnet_link = from_str(obj.get("magnet_link"))
        return StartTorrentRequest(magnet_link)

    def to_dict(self) -> dict:
        result: dict = {}
        result["magnet_link"] = from_str(self.magnet_link)
        return result


def start_torrent_request_from_dict(s: Any) -> StartTorrentRequest:
    return StartTorrentRequest.from_dict(s)


def start_torrent_request_to_dict(x: StartTorrentRequest) -> Any:
    return to_class(StartTorrentRequest, x)

