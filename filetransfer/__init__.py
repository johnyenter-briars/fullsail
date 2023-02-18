import os
import paramiko


def send_file(username: str, password: str):
    ssh = paramiko.SSHClient()

    ssh.load_host_keys(os.path.expanduser(
        os.path.join("~", ".ssh", "known_hosts")))

    ssh.connect("192.168.0.50", username=username, password=password)

    sftp = ssh.open_sftp()
    sftp.put("test.txt", "/storage/videos/test.txt")
    sftp.close()
    ssh.close()
