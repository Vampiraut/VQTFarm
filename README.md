# VQTFarm (Work in Progress)
This is Vampiraut's QarabagTeam Farm for Attack-Defense CTF competitions. You can read more about the competitions here: https://ctftime.org/ctf-wtf.

The farm is engaged in regularly launching written exploits for all commands, parsing the result of the work, taking out flags from there and sending them to the checking system. The farm has a convenient GUI for windows. It is capable of working on multiple threads, processing each feature of the system in a separate thread.

The farm is written in the C# programming language, the platform.NET 6.0, applications .NET Windows Form.

# Requirements
nuget pakages:
1. DynamicLanguageRuntime
2. IronPython
3. Microsoft.Data.Sqlite

# Writing your exploits
The exploit is written in the Python programming language. file_name.py it must contain one function with the name "script" and taking one argument "ip" (the address of the string type, which command will be attacked) and returning one variable of the string type (flag)
Example:
1. \# your import
2. def script(ip):
3.  \# your code is written here
4.  return flag

# The sequel will be coming soon
