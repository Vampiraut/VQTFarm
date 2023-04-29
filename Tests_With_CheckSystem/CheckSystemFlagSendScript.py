import json
import sys
import socket
import sqlite3
from datetime import datetime

url = sys.argv[1]
flags = sys.argv[2]
token = sys.argv[3]
team_name = sys.argv[4]
exploit_name = sys.argv[5]

server_port_glob = 31337
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
sock.connect((url, server_port_glob))

sock.sendall(flags.encode())

resp = json.loads(sock.recv(2048).decode())

sock.close()

path = sys.argv[0]
print(path)
while path[len(path) - 1] != '\\':
    path = path[:-1]
path = path[:-1]
while path[len(path) - 1] != '\\':
    path = path[:-1]
path = path + "FarmInfo.db"

conn = sqlite3.connect(path)
cur = conn.cursor()
for item in resp:
    mess = (None, exploit_name, team_name, item, str(datetime.now()), "ACCEPTED" if resp[item] == "correct flag" else "DENIED", resp[item])
    cur.execute("INSERT INTO FlagHistorys VALUES (?, ?, ?, ?, ?, ?, ?);", mess)
    conn.commit()
conn.close()