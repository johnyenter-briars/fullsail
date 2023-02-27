from typing import List
from bs4 import BeautifulSoup as bs
import pyperclip as pc
import requests
import re

from models import SearchResult


def solidtorrent_search(query) -> List[SearchResult]:
    url = 'https://solidtorrents.net/search?q='+query

    seeders = []

    headers = {
        'User-Agent': 'Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.2840.71 Safari/539.36'}
    source = requests.get(url, headers=headers)
    
    soup = bs(source.text, 'html.parser')

    seeder_imgs = soup.find_all('img', {'alt': 'Seeder'})

    for seeder_img in seeder_imgs:
        parent_div = seeder_img.parent
        font = parent_div.find('font')
        seeders.append(font.text)

    # font_tag = seeder_imgs.find('font')
    # value = font_tag.text

    # result = soup.find_all('h3', class_='subtitle-2 text-truncate')

    # size = soup.find_all('strong')  # find size of torrent
    # seeders = soup.find_all(
    #     'span', class_="green--text darken-4 font-weight-bold")
    # leechers = soup.find_all(
    #     "span", class_="red--text darken-4 font-weight-bold")

    magnet = soup.find_all('a')  # for find href links

    magnet_links = []  # list for storing magnet links

    for link in magnet:
        b: str = link.get('href')
        if b is not None and re.match("^magnet:", b):
            magnet_links.append(b)

    return [SearchResult(magnet_link, seeder) for magnet_link,seeder in zip(magnet_links, seeders)]
