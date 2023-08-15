import os
import shutil
from typing import List
from zipfile import ZipFile
from bs4 import BeautifulSoup as bs
import requests
import re
import fsconfig

from models import SubtitleSearchResult


def open_subtitles_search(query) -> List[SubtitleSearchResult]:
    query = query.replace(" ", "+")
    url = f'https://www.opensubtitles.org/en/search2/sublanguageid-eng/moviename-{query}'
    headers = {
        'User-Agent': 'Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.2840.71 Safari/539.36'}

    source = requests.get(url, headers=headers)

    soup = bs(source.text, 'html.parser')

    table = soup.find(id='search_results')
    table_rows = table.find_all('tr')

    rows_first_skipped = table_rows[1:len(table_rows)]

    results = []

    for row in rows_first_skipped:
        id = row.get('id')
        if id is None:
            continue

        a_tag = row.find('a', {'class': 'bnone'})
        name = a_tag.get_text().replace('\n', ' ')
        link = a_tag.get('href')
        abbr_tag = row.find('abbr', {'class': 'timeago'})
        date_posted = abbr_tag.get_text()
        full_link = f'https://www.opensubtitles.org{link}'

        results.append(
            SubtitleSearchResult(
                name,
                full_link,
                date_posted
            )
        )

    return results

def open_subtitles_download(link):
    headers = {
        'User-Agent': 'Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.2840.71 Safari/539.36'}

    source = requests.get(link, headers=headers)

    soup = bs(source.text, 'html.parser')

    table = soup.find(id='search_results')
    table_rows = table.find_all('tr')

    rows_first_skipped = table_rows[1:len(table_rows)]

    first_row = rows_first_skipped[0]

    columns = first_row.find_all('td')

    fifth_column = columns[4]

    a_tag = fifth_column.find_all('a')[0]

    href = a_tag.get('href')

    download_url = f'https://www.opensubtitles.org{href}'

    r = requests.get(download_url, allow_redirects=True)

    raw_zipfile_path = f'subs/test.zip'

    with open(raw_zipfile_path, 'wb') as raw_zipfile:
        raw_zipfile.write(r.content)

    with ZipFile(raw_zipfile_path, 'r') as zObject:
        zObject.extractall(path="subs/")


    media_root = fsconfig.CONFIG["media-root"]
    default_srt_folder = fsconfig.CONFIG["default-srt-folder"]

    location_for_srt_files = f"{media_root}{default_srt_folder}"

    for srt_file in os.listdir('subs/'):
        if not '.srt' in srt_file:
            continue
        shutil.move(f"subs/{srt_file}", f"{location_for_srt_files}/{srt_file}")