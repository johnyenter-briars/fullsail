import os
import os
import subprocess
from typing import List, Tuple
from ffprobe import FFProbe
import fsconfig

VIDEO_EXTENSIONS = {".mkv", ".mp4"}


async def list_files():
    media_root = fsconfig.CONFIG["media-root"]

    return get_media_files_with_duration(media_root)


def get_media_files_with_duration(directory) -> List:
    media_files = []
    for file_name in os.listdir(directory):
        file_path = os.path.join(directory, file_name)
        if os.path.isfile(file_path) and os.path.splitext(file_path)[1].lower() in VIDEO_EXTENSIONS:
            try:
                cmd = 'ffprobe -i {} -show_entries format=duration -v quiet -of csv="p=0"'.format(
                    file_path)

                output = subprocess.check_output(
                    cmd,
                    shell=True,  # Let this run in the shell
                    stderr=subprocess.STDOUT
                )

                minutes = int(float(output) / 60)
                media_files.append({
                    "name": file_name,
                    "duration": minutes,
                })

            except Exception as e:
                print(f"Failed to extract duration for file {file_name}: {e}")
    return media_files
