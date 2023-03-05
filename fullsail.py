import json
import fsconfig
from webinterface import start_webinterface


def main():
    with open('config.json') as f:
        config = json.load(f)
        fsconfig.CONFIG = {key: value for key, value in config.items()}

    start_webinterface(config)


if __name__ == "__main__":
    main()
