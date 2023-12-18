import string
import time

lower = list(string.ascii_lowercase)
lower.append(" ")

li = [5,20,2,10,26,3,8,6,26,1,8,19,2,7]

for num in li:
    print(lower[num], end="")
    time.sleep(0.25)