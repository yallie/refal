@echo off
echo This section should be empty: >tests.log
echo ---------------------------------------- >>tests.log
refal.exe test01.ref >>tests.log
refal.exe test02.ref >>tests.log
refal.exe test03.ref >>tests.log
refal.exe test04.ref >>tests.log
refal.exe test05.ref >>tests.log
refal.exe test06.ref >>tests.log
refal.exe test07.ref >>tests.log
refal.exe test08.ref >>tests.log
refal.exe test09.ref >>tests.log
refal.exe test10.ref >>tests.log
refal.exe test11.ref >>tests.log
refal.exe test12.ref >>tests.log
refal.exe test13.ref >>tests.log
refal.exe test14.ref >>tests.log
refal.exe test15.ref >>tests.log
refal.exe test16.ref >>tests.log
refal.exe test17.ref >>tests.log
refal.exe test18.ref >>tests.log
refal.exe test19.ref >>tests.log
echo This section should contain errors: >>tests.log
echo ---------------------------------------- >>tests.log
echo *** Duplicate definition of Test1 >>tests.log
refal.exe test20.ref >>tests.log
::refal.exe test21.ref >>tests.log
start .\tests.log