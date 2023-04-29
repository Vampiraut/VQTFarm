import hashlib
import requests
import sys
import time

text = sys.argv[1]

for url in ['http://google.com']:
    for _ in range(3):
        text += requests.get(url).text
        time.sleep(0.1)

flags = []
for _ in range(4):
    text = hashlib.sha256(text.encode()).hexdigest()[:31].upper() + '='
    flags.append(text)
print(*flags, sep=',', end="")
