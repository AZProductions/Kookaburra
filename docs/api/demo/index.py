# To use this code, make sure you
#
#     import json
#
# and then, to convert JSON from a string, do
#
#     result = newest_version_from_dict(json.loads(json_string))

from typing import Any, List, TypeVar, Callable, Type, cast


T = TypeVar("T")


def from_none(x: Any) -> Any:
    assert x is None
    return x


def from_str(x: Any) -> str:
    assert isinstance(x, str)
    return x


def from_list(f: Callable[[Any], T], x: Any) -> List[T]:
    assert isinstance(x, list)
    return [f(y) for y in x]


def to_class(c: Type[T], x: Any) -> dict:
    assert isinstance(x, c)
    return cast(Any, x).to_dict()


class NewestVersionElement:
    url_win64: None
    url_win86: None
    url_winarm: None
    url_linux: None
    url_linuxarm: None
    version: str
    date: str

    def __init__(self, url_win64: None, url_win86: None, url_winarm: None, url_linux: None, url_linuxarm: None, version: str, date: str) -> None:
        self.url_win64 = url_win64
        self.url_win86 = url_win86
        self.url_winarm = url_winarm
        self.url_linux = url_linux
        self.url_linuxarm = url_linuxarm
        self.version = version
        self.date = date

    @staticmethod
    def from_dict(obj: Any) -> 'NewestVersionElement':
        assert isinstance(obj, dict)
        url_win64 = from_none(obj.get("url_win64"))
        url_win86 = from_none(obj.get("url_win86"))
        url_winarm = from_none(obj.get("url_winarm"))
        url_linux = from_none(obj.get("url_linux"))
        url_linuxarm = from_none(obj.get("url_linuxarm"))
        version = from_str(obj.get("version"))
        date = from_str(obj.get("date"))
        return NewestVersionElement(url_win64, url_win86, url_winarm, url_linux, url_linuxarm, version, date)

    def to_dict(self) -> dict:
        result: dict = {}
        result["url_win64"] = from_none(self.url_win64)
        result["url_win86"] = from_none(self.url_win86)
        result["url_winarm"] = from_none(self.url_winarm)
        result["url_linux"] = from_none(self.url_linux)
        result["url_linuxarm"] = from_none(self.url_linuxarm)
        result["version"] = from_str(self.version)
        result["date"] = from_str(self.date)
        return result


def newest_version_from_dict(s: Any) -> List[NewestVersionElement]:
    return from_list(NewestVersionElement.from_dict, s)


def newest_version_to_dict(x: List[NewestVersionElement]) -> Any:
    return from_list(lambda x: to_class(NewestVersionElement, x), x)
