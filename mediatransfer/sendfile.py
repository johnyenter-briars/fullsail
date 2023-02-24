import os
from typing import Dict
import paramiko
from asyncio import events
import fsconfig
import asyncssh


async def send_file(file_name_with_dir):  # finalDict
    username = fsconfig.CONFIG["media-system-username"]
    password = fsconfig.CONFIG["media-system-password"]
    ip = fsconfig.CONFIG["media-system-localip"]
    media_root = fsconfig.CONFIG["media-root"]
    full_file_path = f"{media_root}{file_name_with_dir}"
    foo = file_name_with_dir.split("/", 2)
    file_name = foo[-1]
    media_system_root = fsconfig.CONFIG["media-system-media-root"]

    try:
        print("Performing SSH Connection to the device")
        async with asyncssh.connect(ip, username=username, password=password, known_hosts=None) as conn:
            async with conn.start_sftp_client() as sftp:
                await sftp.put(full_file_path, f"{media_system_root}/{file_name}")

        print("Channel established")
    except Exception as e:
        print(e)
