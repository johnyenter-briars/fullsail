from typing import List
from bs4 import BeautifulSoup as bs
import requests
import re

from models import MagnetLinkSearchResult


def t1337x_search(query) -> List[MagnetLinkSearchResult]:
    url = 'https://1337x.to/search/'+query+'/1/'
    headers = {
        'User-Agent': 'Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.2840.71 Safari/539.36'}
    source = requests.get(url, headers=headers)

    soup = bs(source.text, 'html.parser')

    magnet_links = []
    seeders = []
    leechers = []
    names = []
    downloads = []
    sizes = []
    date_posteds = []

    for a_tag in soup.find_all('a'):
        h_ref: str = a_tag.get('href')

        if re.match("^/torrent", h_ref):
            magnet_link = get_magnet_link(f"https://1337x.to{h_ref}")
            magnet_links.append(magnet_link)
            names.append(a_tag.text)
            downloads.append(None)

    for td in soup.find_all('td', {'class': 'coll-2 seeds'}):
        seeders.append(td.text)

    for td in soup.find_all('td', {'class': 'coll-3 leeches'}):
        leechers.append(td.text)

    for td in soup.find_all('td', {'class': 'coll-date'}):
        date_posteds.append(td.text)

    for td in soup.find_all('td', {'class': 'coll-4 size mob-user'}):
        sizes.append(td.next)

    return [MagnetLinkSearchResult(magnet_link, number_seeders, number_leechers, name, download, size, date_posted) for
            magnet_link, number_seeders, number_leechers, name, download, size, date_posted in
            zip(magnet_links, seeders, leechers, names, downloads, sizes, date_posteds)]


def get_magnet_link(ch_url):
    headers = {
        'User-Agent': 'Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.2840.71 Safari/539.36'}
    source = requests.get(ch_url, headers=headers)
    soup = bs(source.text, 'html.parser')
    magnet = soup.find_all('a')

    for link in magnet:
        b = link.get('href')
        if re.match("^magnet:", b):
            return b
