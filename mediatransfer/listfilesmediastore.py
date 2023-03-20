import os
import subprocess
from typing import List
import fsconfig


async def list_media_files_in_folder(folder):
    media_root = fsconfig.CONFIG["media-root"]
    media_files = []
    if folder == "media-root":
        media_files = _get_files_in_folder(
            media_root,  [".mkv", ".mp4", ".avi", ".srt"])
    else:
        folder = folder.replace("media-root", media_root)
        media_files = _get_files_in_folder(
            folder,  [".mkv", ".mp4", ".avi", ".srt"])
    return media_files


def file_name_in_list(file_name, l):
    for f in l:
        if file_name in f["name"] and f["duration"] is not None:
            return True

    return False


def _get_files_in_folder(directory, extensions) -> List:
    media_files = []
    media_root = fsconfig.CONFIG["media-root"]
    for file_name in os.listdir(directory):
        file_path = os.path.join(directory, file_name)
        if os.path.isfile(file_path):
            if os.path.splitext(file_path)[1].lower() in extensions:
                try:
                    cmd = f'ffprobe -i "{file_path}" -show_entries format=duration -v quiet -of csv="p=0"'

                    output = subprocess.check_output(
                        cmd,
                        shell=True,  # Let this run in the shell
                        stderr=subprocess.STDOUT
                    )

                    minutes = int(float(output) / 60)
                    media_files.append({
                        "name": file_path.replace(media_root, ""),
                        "duration": minutes,
                        "isFile": True,
                    })
                except Exception as e:
                    media_files.append({
                        "name": file_path.replace(media_root, ""),
                        "duration": None,
                        "isFile": True,
                    })
        else:
            media_files.append({
                "name": file_path.replace(media_root, ""),
                "duration": None,
                "isFile": False,
            })

    return media_files
