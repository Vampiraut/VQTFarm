import sys
import requests
import sqlite3

url = sys.argv[1]
isUpdate = True if sys.argv[2] == "1" else False

responseScores = requests.get(url).json()
responseTeams = requests.get("http://10.10.255.3/api/v1/teams").json()

teamsNames = {}
teamsIPs = {}
for team in responseTeams["teams"]:
    teamsNames[team["id"]] = team["name"]
    teamsIPs[team["id"]] = team["ip_address"]

Teams = []
for team in responseScores["scoreboard"]:
    Teams.append({
        "place": responseScores["scoreboard"][team]["place"],
        "name": teamsNames[team],
        "ip": teamsIPs[team],
        "score": responseScores["scoreboard"][team]["points"]
    })

print(Teams)
path = sys.argv[0]
while path[len(path) - 1] != '\\':
    path = path[:-1]
path = path[:-1]
while path[len(path) - 1] != '\\':
    path = path[:-1]
path = path + "FarmInfo.db"

conn = sqlite3.connect(path)
cur = conn.cursor()

for team in Teams:
    if not isUpdate:
        mess = (None, team["place"], team["name"], team["ip"], team["score"])
        cur.execute("INSERT INTO CTFTeams VALUES (?, ?, ?, ?, ?);", mess)
    else:
        cur.execute(f'UPDATE CTFTeams SET teamPlace=\"{team["place"]}\", teamIP=\"{team["ip"]}\", teamScore=\"{team["score"]}\" WHERE teamName=\"{team["name"]}\";')
    conn.commit()
conn.close()

