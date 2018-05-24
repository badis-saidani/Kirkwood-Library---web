
echo off

rem batch file to run a script to create a db
rem 11/12/2017

rem sqlcmd -S localhost -E -i kirkwoodLibrary.sql


sqlcmd -S localhost  -E -i kirkwoodLibrary.sql
rem sqlcmd -S localhost  -E -i kirkwoodLibrary.sql

ECHO .
ECHO if no error messages appear DB was created
PAUSE

