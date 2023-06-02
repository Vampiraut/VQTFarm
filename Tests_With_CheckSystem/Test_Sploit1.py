import sys
import random

ip = sys.argv[1]

alf = ""

for i in range(65, 91):
    alf += chr(i)
for i in range(48, 58):
    alf += chr(i)

def gen_rand_str():
    strr = ""
    for i in range(31):
        strr += random.choice(alf)
    return strr

mass = []
for i in range(4):
    mass.append(gen_rand_str() + "=")

print(mass)
