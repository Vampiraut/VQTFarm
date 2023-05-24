# VQTFarm Version 1
This is Vampiraut's QarabagTeam Farm for Attack-Defense CTF competitions. You can read more about the competitions [here](https://ctftime.org/ctf-wtf).

The farm is engaged in regularly launching written sploits for all commands, parsing the result of the work, taking out flags from there and sending them to the checking system. The farm has a convenient GUI for windows. It is capable of working on multiple threads, processing each feature of the system in a separate thread.

The farm is written in the C# programming language, the platform.NET 6.0, applications .NET Windows Form, tests on OS Windows 11 ver.21H2.

# Requirements
nuget pakages:
1. Microsoft.Data.Sqlite

# START FARM
1. Download the solution from GitHub
2. Open VQTFarm.sln and run the project OR open VQTFarm.exe inside the catalog ..\VQTFarm\bin\Debug\net6.0-windows.
3. Enter the first settings in the window that opens
4. Click the "Deploy farm" button.

# Writing your get/flagSend protocols
The protocols is written in the Python programming language. After writing the protocol, this file should be added to the appropriate folder.
Example:
```Python
import sys
from datetime import datetime
import requests
import sqlite3
'''
your import
'''

url = sys.argv[1]
flags = sys.argv[2]
token = sys.argv[3]
team_name = sys.argv[4]
exploit_name = sys.argv[5]

'''
your request
'''

path = sys.argv[0]
while path[len(path) - 1] != '\\':
    path = path[:-1]
path = path[:-1]
while path[len(path) - 1] != '\\':
    path = path[:-1]
path = path + "FarmInfo.db"

conn = sqlite3.connect(path)
cur = conn.cursor()

#example for RuCTF protocol (can be rewrited)
for item in r.json():
    response = item['msg'].strip()
    response = response.replace('[{}] '.format(item['flag']), '')
    status = ""
    for i in response:
        if i != ':':
            status += i
        else:
            break
    mess = (None, exploit_name, team_name, item['flag'], str(datetime.now()), status.upper(), response)
    cur.execute("INSERT INTO FlagHistorys VALUES (?, ?, ?, ?, ?, ?, ?);", mess)
    conn.commit()
conn.close()
```

# Writing your sploits
The sploit is written in the Python programming language. The sploit must contain the import of the "sys" library and obtaining the ip address of the attacked command via sys.argv[1]. The result of the program is output via print(\*array_of_flags, sep=',', end=""). After writing the sploit, this file should be added to the Sploits folder.
Example:
```Python
import sys
#your import

ip = sys.argv[1]

#your code is written here

print(*flags_arr, sep=',', end="")
```

# Feedback
If you have any problems - contact telegram: `@Grime_Reaper`

# First look
1. Preset Farm Settings

![Preset Farm Settings](https://user-images.githubusercontent.com/99714655/235325626-9f4959dd-031b-4f4f-8976-02b8d78d6cfb.png)

2. Main Window

![Main Window](https://user-images.githubusercontent.com/99714655/235325636-fa1bef7c-f8e0-4433-886f-ae19d295f2c9.png)
