import sys

import requests

url = sys.argv[1]
isUpdate = True if sys.argv[2] == "1" else False

response = requests.get(url).text

print(response)
