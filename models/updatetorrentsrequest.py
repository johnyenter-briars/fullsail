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


class UpdateTorrentsRequest:
    hashes: List[str]

    def __init__(self, hashes: List[str]) -> None:
        self.hashes = hashes

    @staticmethod
    def from_dict(obj: Any) -> 'UpdateTorrentsRequest':
        assert isinstance(obj, dict)
        hashes = from_list(from_str, obj.get("hashes"))
        return UpdateTorrentsRequest(hashes)

    def to_dict(self) -> dict:
        result: dict = {}
        result["hashes"] = from_list(from_str, self.hashes)
        return result


def update_torrents_request_from_dict(s: Any) -> UpdateTorrentsRequest:
    return UpdateTorrentsRequest.from_dict(s)


def update_torrents_request_to_dict(x: UpdateTorrentsRequest) -> Any:
    return to_class(UpdateTorrentsRequest, x)

