import re
import sys
from datetime import datetime
import requests
import sqlite3

STATUS = {
    "ACCEPTED": ["Flag accepted!", "Accepted", "ACCEPTED", "points"],
    "DENIED": ["Flag is invalid or too old.", "Denied", "DENIED", "old", "invalid" ]
}

url = sys.argv[1]
flags = sys.argv[2]
token = sys.argv[3]
team_name = sys.argv[4]
exploit_name = sys.argv[5]

m = re.findall(r'[A-Z0-9]{31}=', flags)

r = requests.put(url, headers={'X-Team-Token': token}, json=[item for item in m])

path = sys.argv[0]
while path[len(path) - 1] != '\\':
    path = path[:-1]
path = path[:-1]
while path[len(path) - 1] != '\\':
    path = path[:-1]
path = path + "FarmInfo.db"

conn = sqlite3.connect(path)
cur = conn.cursor()
for item in r.json():
    response = item['msg'].strip()
    response = response.replace('[{}] '.format(item['flag']), '')

    status = ""
    for key in STATUS:
        for i in range(len(STATUS[key])):
            if STATUS[key][i] in response:
                status = key
                break
    if status == "":
        status = "INVALIDE"

    mess = (None, exploit_name, team_name, item['flag'], str(datetime.now()), status, response)
    cur.execute("INSERT INTO FlagHistorys VALUES (?, ?, ?, ?, ?, ?, ?);", mess)
    conn.commit()
conn.close()
