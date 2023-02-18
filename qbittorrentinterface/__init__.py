from http.server import BaseHTTPRequestHandler, HTTPServer
import time
import requests
from requests import Response
# http://localhost:8080/api/v2/torrents/info

hostName = "localhost"
serverPort = 8082


class QBitTorrentInterface(BaseHTTPRequestHandler):
    def do_GET(self):
        self.send_response(200)
        self.send_header("Content-type", "text/html")
        self.end_headers()

        response: Response = requests.get(
            "http://localhost:8080/api/v2/torrents/info")

        self.wfile.write(
            bytes(response.text, "utf-8"))


def start_qbittorrentinterface():
    webServer = HTTPServer((hostName, serverPort), QBitTorrentInterface)

    print("Server started http://%s:%s" % (hostName, serverPort))

    webServer.serve_forever()
