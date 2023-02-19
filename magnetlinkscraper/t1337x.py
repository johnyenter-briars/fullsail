from typing import List
from bs4 import BeautifulSoup as bs
import pyperclip as pc
import requests
import re

from magnetlinkscraper.search_result import SearchResult


def t1337x_search(query) -> List[SearchResult]:
    url = 'https://1337x.to/search/'+query+'/1/'
    headers = {
        'User-Agent': 'Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.2840.71 Safari/539.36'}
    source = requests.get(url, headers=headers)

    soup = bs(source.text, 'html.parser')

    if(soup.find_all('td', class_='coll-1 name')):
        results = soup.find_all('td', class_='coll-1 name')
        size = soup.find_all('td', class_="coll-4 size mob-vip")
        return search_result(soup, results, size)  # call for getting results


def search_result(soup, results, size):
    magnet_links = []
    for link in soup.find_all('a'):
        b: str = link.get('href')

        if re.match("^/torrent", b):
            magnet_link = get_magnet_link(f"https://1337x.to{b}")
            magnet_links.append(magnet_link)

    return [SearchResult(magnet_link, 2) for magnet_link in magnet_links]


def get_magnet_link(ch_url):
    headers = {
        'User-Agent': 'Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.2840.71 Safari/539.36'}
    source = requests.get(ch_url, headers=headers)
    soup = bs(source.text, 'html.parser')
    magnet = soup.find_all('a')

    for link in magnet:
        b = link.get('href')
        if re.match("^magnet:", b):
            magnet_link = b
            break

