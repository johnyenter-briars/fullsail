import os
from typing import Dict
import paramiko
from asyncio import events
import fsconfig
import asyncssh

async def delete_file_mediasystem(file_name):
    username = fsconfig.CONFIG["media-system-username"]
    password = fsconfig.CONFIG["media-system-password"]
    ip = fsconfig.CONFIG["media-system-localip"]
    media_system_root = fsconfig.CONFIG["media-system-media-root"]
    full_file_path = f"{media_system_root}/{file_name}"

    try:
        print(f"Begining to delete file: {file_name}")
        async with asyncssh.connect(ip, username=username, password=password, known_hosts=None) as conn:
            async with conn.start_sftp_client() as sftp:
                await sftp.remove(full_file_path)

        print(f"Successfully deleted file: {file_name}")
    except Exception as e:
        print(e)
