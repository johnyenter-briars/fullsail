from typing import List, Any, TypeVar, Callable, Type, cast


T = TypeVar("T")


def from_list(f: Callable[[Any], T], x: Any) -> List[T]:
    assert isinstance(x, list)
    return [f(y) for y in x]


def from_str(x: Any) -> str:
    assert isinstance(x, str)
    return x


def to_class(c: Type[T], x: Any) -> dict:
    assert isinstance(x, c)
    return cast(Any, x).to_dict()


class StartTorrentsRequest:
    magnet_links: List[str]

    def __init__(self, magnet_links: List[str]) -> None:
        self.magnet_links = magnet_links

    @staticmethod
    def from_dict(obj: Any) -> 'StartTorrentsRequest':
        assert isinstance(obj, dict)
        magnet_links = from_list(from_str, obj.get("magnet_links"))
        return StartTorrentsRequest(magnet_links)

    def to_dict(self) -> dict:
        result: dict = {}
        result["magnet_links"] = from_list(from_str, self.magnet_links)
        return result


def start_torrents_request_from_dict(s: Any) -> StartTorrentsRequest:
    return StartTorrentsRequest.from_dict(s)


def start_torrents_request_to_dict(x: StartTorrentsRequest) -> Any:
    return to_class(StartTorrentsRequest, x)

