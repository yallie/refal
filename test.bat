@echo off
echo Test results:>tests.log

refal.exe test02.ref >test02.cs
csc /debug test02.cs RefalBase.cs PassiveExpression.cs Pattern.cs PatternItems.cs PatternVariables.cs >nul
test02.exe >test.out
diff -u test02.txt test.out >>tests.log
if errorlevel 1 goto end

refal.exe test03.ref >test03.cs
csc /debug test03.cs RefalBase.cs PassiveExpression.cs Pattern.cs PatternItems.cs PatternVariables.cs >nul
test03.exe >test.out
fc /b test03.txt test.out >>tests.log
if errorlevel 1 goto end

refal.exe test04.ref >test04.cs
csc /debug test04.cs RefalBase.cs PassiveExpression.cs Pattern.cs PatternItems.cs PatternVariables.cs >nul
test04.exe >test.out
fc /b test04.txt test.out >>tests.log
if errorlevel 1 goto end

refal.exe test05.ref >test05.cs
csc /debug test05.cs RefalBase.cs PassiveExpression.cs Pattern.cs PatternItems.cs PatternVariables.cs >nul
test05.exe >test.out
fc /b test05.txt test.out >>tests.log
if errorlevel 1 goto end

refal.exe test06.ref >test06.cs
csc /debug test06.cs RefalBase.cs PassiveExpression.cs Pattern.cs PatternItems.cs PatternVariables.cs >nul
test06.exe >test.out
fc /b test06.txt test.out >>tests.log
if errorlevel 1 goto end

refal.exe test07.ref >test07.cs
csc /debug test07.cs RefalBase.cs PassiveExpression.cs Pattern.cs PatternItems.cs PatternVariables.cs >nul
test07.exe >test.out
fc /b test07.txt test.out >>tests.log
if errorlevel 1 goto end

refal.exe test08.ref >test08.cs
csc /debug test08.cs RefalBase.cs PassiveExpression.cs Pattern.cs PatternItems.cs PatternVariables.cs >nul
test08.exe <test08.txt >test.out
fc /b test08.txt test.out >>tests.log
if errorlevel 1 goto end

refal.exe test09.ref >test09.cs
csc /debug test09.cs RefalBase.cs PassiveExpression.cs Pattern.cs PatternItems.cs PatternVariables.cs >nul
test09.exe >test.out
fc /b test09.txt test.out >>tests.log
if errorlevel 1 goto end

refal.exe test10.ref >test10.cs
csc /debug test10.cs RefalBase.cs PassiveExpression.cs Pattern.cs PatternItems.cs PatternVariables.cs >nul
test10.exe >test.out
fc /b test10.txt test.out >>tests.log
if errorlevel 1 goto end

refal.exe test13.ref >test13.cs
csc /debug test13.cs RefalBase.cs PassiveExpression.cs Pattern.cs PatternItems.cs PatternVariables.cs >nul
test13.exe <test13in.txt >test.out
fc /b test13.txt test.out >>tests.log
if errorlevel 1 goto end

refal.exe test14.ref >test14.cs
csc /debug test14.cs RefalBase.cs PassiveExpression.cs Pattern.cs PatternItems.cs PatternVariables.cs >nul
test14.exe test14in.txt test.out
fc /b test14.txt test.out >>tests.log
if errorlevel 1 goto end

refal.exe test23.ref >test23.cs
csc /debug test23.cs RefalBase.cs PassiveExpression.cs Pattern.cs PatternItems.cs PatternVariables.cs >nul
test23.exe >test.out
fc /b test23.txt test.out >>tests.log
if errorlevel 1 goto end

refal.exe test24.ref >test24.cs
csc /debug test24.cs RefalBase.cs PassiveExpression.cs Pattern.cs PatternItems.cs PatternVariables.cs >nul
test24.exe >test.out
fc /b test24.txt test.out >>tests.log
if errorlevel 1 goto end

refal.exe test25.ref >test25.cs
csc /debug test25.cs RefalBase.cs PassiveExpression.cs Pattern.cs PatternItems.cs PatternVariables.cs >nul
test25.exe >test.out
fc /b test25.txt test.out >>tests.log
if errorlevel 1 goto end

refal.exe test26.ref >test26.cs
csc /debug test26.cs RefalBase.cs PassiveExpression.cs Pattern.cs PatternItems.cs PatternVariables.cs >nul
test26.exe >test.out
fc /b test26.txt test.out >>tests.log
if errorlevel 1 goto end

goto end

::refal.exe test11.ref >test11.cs
::refal.exe test12.ref >test12.cs
::csc /debug test12.cs RefalBase.cs PassiveExpression.cs Pattern.cs PatternItems.cs PatternVariables.cs >nul
refal.exe test15.ref >test15.cs
csc /debug test15.cs RefalBase.cs PassiveExpression.cs Pattern.cs PatternItems.cs PatternVariables.cs >nul
refal.exe test16.ref >test16.cs
csc /debug test16.cs RefalBase.cs PassiveExpression.cs Pattern.cs PatternItems.cs PatternVariables.cs >nul
refal.exe test17.ref >test17.cs
csc /debug test17.cs RefalBase.cs PassiveExpression.cs Pattern.cs PatternItems.cs PatternVariables.cs >nul
refal.exe test18.ref >test18.cs
csc /debug test18.cs RefalBase.cs PassiveExpression.cs Pattern.cs PatternItems.cs PatternVariables.cs >nul
refal.exe test19.ref >test19.cs
csc /debug test19.cs RefalBase.cs PassiveExpression.cs Pattern.cs PatternItems.cs PatternVariables.cs >nul

:end
start tests.log