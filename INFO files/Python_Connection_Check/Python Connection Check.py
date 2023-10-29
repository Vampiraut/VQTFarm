import sys
import requests as r
url = sys.argv[1]
try:
    if str(r.get(url).status_code) != "404":
        print(True)
    else:
        print(False)
except:
    print(False)
