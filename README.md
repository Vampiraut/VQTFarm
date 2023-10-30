# VQTFarm Version 4
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

# Help
In the main window of the program, you can manage and configure the necessary work parameters:
1. Settings - farm settings:
   1. Add team manual - manually adding a team to the list of attacked teams with specifying their IP and name.
   2. Auto teams generation - automatic generation of commands in a given range of IP addresses to the list of attacked commands with the assignment of their IP and default name.
   3. Start/Stop Farm - enabling/disabling program threads. Stops the farm by disabling sending flags and updating tables.
   4. On Auto team parse (OFF) - enabling the function of automatic team parsing from scoreboards, subject to the availability of a working script. Updates team data once a round: their position, name, IP and flagpoints.(the working script should be selected on the initial window, being added to the folder with the corresponding scripts)
   5. On Conection check (OFF) - enables the function of checking the availability of the check system. After clicking, you need to select the python script to check.
   6. Show - enables/disables elements of the working window. In addition to visually disabling them, it stops the work thread associated with this element.
   7. Disable pop-up messages - enables/disables the display of notification messages, warnings and errors.
   8. Fix Tables - fix a bug with tables when they stop displaying information.
3. Help - shows information from this block.
4. Manual Submit - panel for manually sending flags.
5. Sploit test - testing the operability of the sploits before they are added to the main process, with the choice of the attacked team.
6. Teams - a table showing the teams (their position, name, IP, flag points). Pagination is available.
7. Flag status - a table showing the sent flags (by which unit it was received, from which command, which flag, the time of sending, the status and the response from the check system). Also shows the total number of sent flags and accepted flags. Pagination is available.
8. Flag show filter - a panel with filter settings for displaying information on flags in the table from the previous paragraph.
   
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

![Preset Farm Settings](https://github.com/Vampiraut/VQTFarm/assets/99714655/2b47c59f-b87a-4122-a02e-8e142723f7ce)


2. Main Window

![Main Window](https://github.com/Vampiraut/VQTFarm/assets/99714655/5fdce812-7815-4332-91d8-c516bfb84b88)


# Feedback
If you have any problems - contact telegram: `@Grime_Reaper`

# The sequel will be coming soon
