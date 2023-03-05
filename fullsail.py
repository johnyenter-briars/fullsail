import json
import fsconfig
from webinterface import start_webinterface


def main():
    with open('config.json') as f, \
            open('./mediacache/cache.json') as c:
        config = json.load(f)
        cache = json.load(c)
        fsconfig.CONFIG = {key: value for key, value in config.items()}
        fsconfig.MEDIACACHE = {key: value for key, value in cache.items()}

    start_webinterface(config)


if __name__ == "__main__":
    main()
