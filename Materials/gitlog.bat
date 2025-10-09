@echo off

cd /d "C:\Users\Usevalad\Study\7sem\course\my\cataca"
git log --pretty=format:"%%ad","%%s" --date=short > Materials\gitlog.txt