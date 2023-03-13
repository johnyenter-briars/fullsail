import fsconfig
import asyncssh


async def list_files_mediasystem():  # finalDict
    username = fsconfig.CONFIG["media-system-username"]
    password = fsconfig.CONFIG["media-system-password"]
    ip = fsconfig.CONFIG["media-system-localip"]
    media_system_root = fsconfig.CONFIG["media-system-media-root"]

    try:
        async with asyncssh.connect(ip, username=username, password=password, known_hosts=None) as conn:
            async with conn.start_sftp_client() as sftp:
                foo = await sftp.listdir(media_system_root)
                return [{
                    "name": name,
                    "duration": None
                }  for name in foo if name != "." and name != ".."]

    except Exception as e:
        print(e)
