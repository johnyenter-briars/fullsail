from typing import List
from bs4 import BeautifulSoup as bs
import requests
import re
from models import SearchResult


def solidtorrent_search(query) -> List[SearchResult]:
    url = 'https://solidtorrents.net/search?q='+query

    headers = {
        'User-Agent': 'Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.2840.71 Safari/539.36'}

    source = requests.get(url, headers=headers)

    seeders = []
    leechers = []
    names = []
    magnet_links = []
    downloads = []
    sizes = []
    date_posteds = []
    soup = bs(source.text, 'html.parser')

    seeder_imgs = soup.find_all('img', {'alt': 'Seeder'})
    for seeder_img in seeder_imgs:
        parent_div = seeder_img.parent
        font = parent_div.find('font')
        seeders.append(font.text)

    seeder_imgs = soup.find_all('img', {'alt': 'Seeder'})
    for seeder_img in seeder_imgs:
        parent_div = seeder_img.parent
        font = parent_div.find('font')
        seeders.append(font.text)

    leecher_imgs = soup.find_all('img', {'alt': 'Leecher'})
    for leecher_img in leecher_imgs:
        parent_div = leecher_img.parent
        font = parent_div.find('font')
        leechers.append(font.text)

    download_images = soup.find_all('img', {'alt': 'Download'})
    for img in download_images:
        parent_div = img.parent
        downloads.append(parent_div.text)

    size_images = soup.find_all('img', {'alt': 'Size'})
    for img in size_images:
        parent_div = img.parent
        sizes.append(parent_div.text)

    date_images = soup.find_all('img', {'alt': 'Date'})
    for img in date_images:
        parent_div = img.parent
        date_posteds.append(parent_div.text)

    a_tags = soup.find_all('a')  # for find href links

    for link in a_tags:
        b: str = link.get('href')
        if b is not None and re.match("^magnet:", b):
            magnet_links.append(b)

        if b is not None and "/torrents/" in b:
            names.append(link.text)

    return [SearchResult(magnet_link, number_seeders, number_leechers, name, download, size, date_posted) for
            magnet_link, number_seeders, number_leechers, name, download, size, date_posted in
            zip(magnet_links, seeders, leechers, names, downloads, sizes, date_posteds)]
