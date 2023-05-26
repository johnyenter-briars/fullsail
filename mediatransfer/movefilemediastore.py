import shutil
import os
import subprocess
from typing import List
import fsconfig


async def move_item(item_name, destination):
    media_root = fsconfig.CONFIG["media-root"]

    full_path_item = f"{media_root}{item_name}"
    full_path_destination = f"{media_root}{destination}"

    if not os.path.exists(full_path_destination) or not os.path.exists(full_path_item):
        return False
    
    if not os.path.isdir(full_path_destination):
        return False

    shutil.move(full_path_item, full_path_destination)

    return True



    



