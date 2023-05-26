import fsconfig
import asyncssh


async def send_file_to_mediasystem(file_name_with_dir):  # finalDict
    username = fsconfig.CONFIG["media-system-username"]
    password = fsconfig.CONFIG["media-system-password"]
    ip = fsconfig.CONFIG["media-system-localip"]
    media_root = fsconfig.CONFIG["media-root"]
    full_file_path = f"{media_root}{file_name_with_dir}"
    foo = file_name_with_dir.split("/", 20)
    file_name = foo[-1]
    media_system_root = fsconfig.CONFIG["media-system-media-root"]

    try:
        print(f"Begining to send file: {file_name}")
        async with asyncssh.connect(ip, username=username, password=password, known_hosts=None) as conn:
            async with conn.start_sftp_client() as sftp:
                await sftp.put(full_file_path, f"{media_system_root}/{file_name}")

        print(f"Successfully sent file: {file_name}")
    except Exception as e:
        print(e)


async def send_file_to_laptop(file_name_with_dir):  # finalDict
    username = fsconfig.CONFIG["laptop-username"]
    password = fsconfig.CONFIG["laptop-password"]
    ip = fsconfig.CONFIG["laptop-localip"]
    media_root = fsconfig.CONFIG["media-root"]
    full_file_path = f"{media_root}{file_name_with_dir}"
    foo = file_name_with_dir.split("/", 20)
    file_name = foo[-1]
    laptop_root = fsconfig.CONFIG["laptop-media-root"]

    try:
        print(f"Begining to send file: {file_name}")
        async with asyncssh.connect(ip, username=username, password=password, known_hosts=None) as conn:
            async with conn.start_sftp_client() as sftp:
                await sftp.put(full_file_path, f"{laptop_root}/{file_name}")

        print(f"Successfully sent file: {file_name}")
    except Exception as e:
        print(e)
