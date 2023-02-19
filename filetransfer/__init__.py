import os
from typing import Dict
import paramiko
from asyncio import events


def _send_file(username: str, password: str, config: Dict, file_name: str, future):
    ssh = paramiko.SSHClient()

    ssh.load_host_keys(os.path.expanduser(
        os.path.join("~", ".ssh", "known_hosts")))

    ssh.connect(config['media-system-localip'],
                username=username, password=password)

    sftp = ssh.open_sftp()
    sftp.put("test.txt", f"{config['media-system-media-root']}/{file_name}")
    sftp.close()
    ssh.close()

    future.set_result("done")


async def send_file(username, password, config, filename, result=None):
    loop = events.get_running_loop()
    future = loop.create_future()
    h = loop.call_later(0,
                        _send_file,
                        username, password, config, filename,
                        future)

    try:
        return await future
    finally:
        h.cancel()
