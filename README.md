# VQTFarm (Work in Progress)
This is Vampiraut's QarabagTeam Farm for Attack-Defense CTF competitions. You can read more about the competitions [here]{https://ctftime.org/ctf-wtf}.

The farm is engaged in regularly launching written exploits for all commands, parsing the result of the work, taking out flags from there and sending them to the checking system. The farm has a convenient GUI for windows. It is capable of working on multiple threads, processing each feature of the system in a separate thread.

The farm is written in the C# programming language, the platform.NET 6.0, applications .NET Windows Form.

# Requirements
nuget pakages:
1. Guna.UI2.WinForms
2. Microsoft.Data.Sqlite

# Writing your exploits
The exploit is written in the Python programming language. file_name.py. The exploit must contain the import of the "sys" library and obtaining the ip address of the attacked command via sys.argv[1]. The result of the program is output via print(\*array_of_flags, sep=',', end=""). After writing the exploit, this file should be added to the Sploits folder.
Example:
```Python
import sys
#your import

ip = sys.argv[1]

#your code is written here

print(*flags_arr, sep=',', end="")
```

# The sequel will be coming soon
