import shutil
import os
import subprocess
from typing import List
import fsconfig


async def delete_item(item):
    media_root = fsconfig.CONFIG["media-root"]

    full_path = f"{media_root}{item}"

    if os.path.isfile(full_path):
        os.remove(full_path)
    elif os.path.isdir(full_path):
        shutil.rmtree(full_path)
    



