import os
import os
from ffprobe import FFProbe

import fsconfig


async def list_files():
    media_root = fsconfig.CONFIG["media-root"]
    get_media_files_with_duration(media_root)

    entries = os.listdir(media_root)

    return 


VIDEO_EXTENSIONS = {".mkv", ".mp4"}

def get_media_files_with_duration(directory):
    media_files = []
    for file_name in os.listdir(directory):
        file_path = os.path.join(directory, file_name)
        if os.path.isfile(file_path) and os.path.splitext(file_path)[1].lower() in VIDEO_EXTENSIONS:
            try:
                metadata = FFProbe(file_path)
                foo = list(filter(lambda s : s.codec_type == "video", metadata.streams))
                duration = metadata.streams.video[0].duration
                media_files.append({"file_name": file_name, "duration": duration})
            except Exception as e:
                print(f"Failed to extract duration for file {file_name}: {e}")
    return media_files
