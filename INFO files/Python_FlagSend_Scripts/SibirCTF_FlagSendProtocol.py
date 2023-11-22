import re
import sys
from datetime import datetime
import requests as req
import sqlite3

STATUS = {
    200: "ACCEPTED",
    403: "DENIED",
    400: "INVALID"
}

RESPONSE = {
    200: "flag is accepted",
    403: "flag old, already accepted, not found",
    400: "wrong parameters"
}


url = sys.argv[1]
flags = sys.argv[2]
token = sys.argv[3]
team_name = sys.argv[4]
exploit_name = sys.argv[5]

m = re.findall(r"c01d[a-z0-9]{4}-[a-z0-9]{4}-[a-z0-9]{4}-[a-z0-9]{4}-[a-z0-9]{4}[0-9]{8}", flags)

resp = {}
ressspoonssee = ""
for flag in m:
    status = req.get(f"{url}?teamid={token}&flag={flag}")
    resp[flag] = status.status_code
    ressspoonssee = status.content

path = sys.argv[0]
while path[len(path) - 1] != '\\':
    path = path[:-1]
path = path[:-1]
while path[len(path) - 1] != '\\':
    path = path[:-1]
path = path + "FarmInfo.db"

conn = sqlite3.connect(path)
cur = conn.cursor()

for key, value in resp.items():
    status = ""
    if value != 200 and value != 400 and value != 403:
        status = "ERROR"
    else:
        status = STATUS[value]

    mess = (None, exploit_name, team_name, key, str(datetime.now()), status, ressspoonssee)
    cur.execute("INSERT INTO FlagHistorys VALUES (?, ?, ?, ?, ?, ?, ?);", mess)
    conn.commit()
conn.close()
