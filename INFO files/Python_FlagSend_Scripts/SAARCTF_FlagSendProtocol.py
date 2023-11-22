import re
import sys
from datetime import datetime
import requests
import sqlite3
from pwn import *

url = sys.argv[1]
flags = sys.argv[2]
token = sys.argv[3]
team_name = sys.argv[4]
exploit_name = sys.argv[5]

m = re.findall(r"SAAR\{[A-Za-z0-9-_]{32}}", flags)

s = remote("submission.ctf.saarland", 31337)
response = {}
for flag in m:
    try:
        s.sendline(flag.encode())
        response[flag] = s.recvline().decode()
    except:
        s = remote("submission.ctf.saarland", 31337)
        pass

path = sys.argv[0]
while path[len(path) - 1] != '\\':
    path = path[:-1]
path = path[:-1]
while path[len(path) - 1] != '\\':
    path = path[:-1]
path = path + "FarmInfo.db"

conn = sqlite3.connect(path)
cur = conn.cursor()

for key, value in response.items():
    mess = (None, exploit_name, team_name, key, str(datetime.now()), value, value)
    cur.execute("INSERT INTO FlagHistorys VALUES (?, ?, ?, ?, ?, ?, ?);", mess)
    conn.commit()
conn.close()
