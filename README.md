# VQTFarm (Work in Progress so may contains bugs!!!)
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
flags = re.findall(r'[A-Z0-9]{31}=', sys.argv[2])
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
STATUS = {
    "ACCEPTED": ["Flag accepted!"],
    "DENIED": ["Flag is invalid or too old."]
}
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
```

# Writing your sploits
The sploit is written in the Python programming language. The sploit must contain the import of the "sys" library and obtaining the ip address of the attacked command via sys.argv[1]. The result of the program is output via print(flags), as you wish. After writing the sploit, this file should be added to the Sploits folder.
Example:
```Python
import sys
#your import

ip = sys.argv[1]

#your code is written here

print(flags) #as you wish
```

# First look
1. Preset Farm Settings

![Preset Farm Settings](https://github.com/Vampiraut/VQTFarm/assets/99714655/e8a58b7e-b85a-45d2-b93d-212cb540c284)


2. Main Window

![Main Window](https://github.com/Vampiraut/VQTFarm/assets/99714655/7ee33035-2a65-45b4-967e-83d2ea2efaf0)


# Feedback
If you have any problems - contact telegram: `@Grime_Reaper`

# The sequel will be coming soon
