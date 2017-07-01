echo off

sqlcmd -S localhost -E -i SignInFormDB.sql

ECHO if no error message appears DB was created
PAUSE