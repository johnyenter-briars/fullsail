from typing import List
from bs4 import BeautifulSoup as bs
import requests
import re

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

    foo = table_rows[1:len(table_rows)]

    results = []

    for row in foo:
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
