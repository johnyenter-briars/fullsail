from bs4 import BeautifulSoup as bs
import pyperclip as pc
import requests
import re


def t1337x_search(query):
    url = 'https://1337x.to/search/'+query+'/1/'
    headers = {
        'User-Agent': 'Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.2840.71 Safari/539.36'}
    source = requests.get(url, headers=headers)

    soup = bs(source.text, 'html.parser')

    if(soup.find_all('td', class_='coll-1 name')):
        results = soup.find_all('td', class_='coll-1 name')
        size = soup.find_all('td', class_="coll-4 size mob-vip")
        search_result(soup, results, size)  # call for getting results


def search_result(soup, results, size):
    for link in soup.find_all('a'):
        b: str = link.get('href')

        if re.match("^/torrent", b):
            get_magnet_link(f"https://1337x.to{b}")


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

    print(magnet_link)


def solidtorrent_search(query):
    '''
        search on solid torrent for long result(upto 20) and print magnet link of torrent and also copy it to clipboard
    '''

    url = 'https://solidtorrents.net/search?q='+query
    # print(url)

    headers = {
        'User-Agent': 'Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.2840.71 Safari/539.36'}
    source = requests.get(url, headers=headers)

    soup = bs(source.text, 'html.parser')

    # name of all torrents (20)
    result = soup.find_all('h3', class_='subtitle-2 text-truncate')

    size = soup.find_all('strong')  # find size of torrent
    seeders = soup.find_all(
        'span', class_="green--text darken-4 font-weight-bold")
    leechers = soup.find_all(
        "span", class_="red--text darken-4 font-weight-bold")

    magnet = soup.find_all('a')  # for find href links

    magnet_links = []  # list for storing magnet links

    for link in magnet:
        b: str = link.get('href')
        print(b)
        if b is not None and re.match("^magnet:", b):
            magnet_links.append(b)

    print(magnet_links[0])

query = "avengers end game"
solidtorrent_search(query)
# t1337x_search(query)
