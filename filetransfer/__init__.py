import os
from typing import Dict
import paramiko
from asyncio import events

import fsconfig


def _send_file(file_name: str, future):
    username = fsconfig.CONFIG["media-system-username"]
    password = fsconfig.CONFIG["media-system-password"]
    ip = fsconfig.CONFIG["media-system-localip"]
    media_system_root = fsconfig.CONFIG["media-system-media-root"]

    ssh = paramiko.SSHClient()

    ssh.load_host_keys(os.path.expanduser(
        os.path.join("~", ".ssh", "known_hosts")))

    ssh.connect(ip,
                username=username, password=password)

    sftp = ssh.open_sftp()
    sftp.put("test.txt", f"{media_system_root}/{file_name}")
    sftp.close()
    ssh.close()

    future.set_result("done")


async def send_file(filename):
    loop = events.get_running_loop()
    future = loop.create_future()
    h = loop.call_later(0, _send_file, filename, future)

    try:
        return await future
    finally:
        h.cancel()
