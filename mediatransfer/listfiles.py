import os
import os
import subprocess
from typing import List, Tuple
from ffprobe import FFProbe
import fsconfig


async def media_files(get_duration):
    media_root = fsconfig.CONFIG["media-root"]

    media_files = []

    _get_file_metadata(
        media_root,  [".mkv", ".mp4", ".avi"], media_files, get_duration)

    return media_files


async def subtitle_files():
    media_root = fsconfig.CONFIG["media-root"]

    media_files = []

    _get_file_metadata(media_root, [".srt"], media_files, False)

    return media_files


def _get_file_metadata(directory, extensions, media_files=[], get_duration=True) -> List:
    media_root = fsconfig.CONFIG["media-root"]
    for file_name in os.listdir(directory):
        file_path = os.path.join(directory, file_name)
        if os.path.isfile(file_path):
            if os.path.splitext(file_path)[1].lower() in extensions:
                if get_duration:
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
                        })
                    except Exception as e:
                        print(
                            f"Failed to extract duration for file {file_name}: {e}")
                else:
                    media_files.append({
                        "name": file_path.replace(media_root, ""),
                        "duration": None,
                    })
        else:
            _get_file_metadata(file_path, extensions,
                               media_files, get_duration)

    # return media_files
