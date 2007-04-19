@echo off
rem Compile and run test programs, compare their output to the standard files
rem All tests should produce "No differences encountered" lines in tests.log file

echo Test results:>tests.log
cd tests

..\bin\refal.exe test01.ref >test01.cs
csc /debug /nologo /out:..\temp\test.exe test01.cs ..\runtime\RefalBase.cs ..\runtime\PassiveExpression.cs ..\runtime\Pattern.cs ..\runtime\PatternItems.cs ..\runtime\PatternVariables.cs >nul
..\temp\test.exe >..\temp\test.out
fc /b test01.txt ..\temp\test.out >>..\tests.log
if errorlevel 1 goto end

..\bin\refal.exe test02.ref >test02.cs
csc /debug /nologo /out:..\temp\test.exe test02.cs ..\runtime\RefalBase.cs ..\runtime\PassiveExpression.cs ..\runtime\Pattern.cs ..\runtime\PatternItems.cs ..\runtime\PatternVariables.cs >nul
..\temp\test.exe >..\temp\test.out
fc /b test02.txt ..\temp\test.out >>..\tests.log
if errorlevel 1 goto end

..\bin\refal.exe test03.ref >test03.cs
csc /debug /out:..\temp\test.exe test03.cs ..\runtime\RefalBase.cs ..\runtime\PassiveExpression.cs ..\runtime\Pattern.cs ..\runtime\PatternItems.cs ..\runtime\PatternVariables.cs >nul
..\temp\test.exe >..\temp\test.out
fc /b test03.txt ..\temp\test.out >>..\tests.log
if errorlevel 1 goto end

..\bin\refal.exe test04.ref >test04.cs
csc /debug /out:..\temp\test.exe test04.cs ..\runtime\RefalBase.cs ..\runtime\PassiveExpression.cs ..\runtime\Pattern.cs ..\runtime\PatternItems.cs ..\runtime\PatternVariables.cs >nul
..\temp\test.exe >..\temp\test.out
fc /b test04.txt ..\temp\test.out >>..\tests.log
if errorlevel 1 goto end

..\bin\refal.exe test05.ref >test05.cs
csc /debug /out:..\temp\test.exe test05.cs ..\runtime\RefalBase.cs ..\runtime\PassiveExpression.cs ..\runtime\Pattern.cs ..\runtime\PatternItems.cs ..\runtime\PatternVariables.cs >nul
..\temp\test.exe >..\temp\test.out
fc /b test05.txt ..\temp\test.out >>..\tests.log
if errorlevel 1 goto end

..\bin\refal.exe test06.ref >test06.cs
csc /debug /out:..\temp\test.exe test06.cs ..\runtime\RefalBase.cs ..\runtime\PassiveExpression.cs ..\runtime\Pattern.cs ..\runtime\PatternItems.cs ..\runtime\PatternVariables.cs >nul
..\temp\test.exe >..\temp\test.out
fc /b test06.txt ..\temp\test.out >>..\tests.log
if errorlevel 1 goto end

..\bin\refal.exe test07.ref >test07.cs
csc /debug /out:..\temp\test.exe test07.cs ..\runtime\RefalBase.cs ..\runtime\PassiveExpression.cs ..\runtime\Pattern.cs ..\runtime\PatternItems.cs ..\runtime\PatternVariables.cs >nul
..\temp\test.exe >..\temp\test.out
fc /b test07.txt ..\temp\test.out >>..\tests.log
if errorlevel 1 goto end

..\bin\refal.exe test08.ref >test08.cs
csc /debug /out:..\temp\test.exe test08.cs ..\runtime\RefalBase.cs ..\runtime\PassiveExpression.cs ..\runtime\Pattern.cs ..\runtime\PatternItems.cs ..\runtime\PatternVariables.cs >nul
..\temp\test.exe <test08.txt >..\temp\test.out
fc /b test08.txt ..\temp\test.out >>..\tests.log
if errorlevel 1 goto end

..\bin\refal.exe test09.ref >test09.cs
csc /debug /out:..\temp\test.exe test09.cs ..\runtime\RefalBase.cs ..\runtime\PassiveExpression.cs ..\runtime\Pattern.cs ..\runtime\PatternItems.cs ..\runtime\PatternVariables.cs >nul
..\temp\test.exe >..\temp\test.out
fc /b test09.txt ..\temp\test.out >>..\tests.log
if errorlevel 1 goto end

..\bin\refal.exe test10.ref >test10.cs
csc /debug /out:..\temp\test.exe test10.cs ..\runtime\RefalBase.cs ..\runtime\PassiveExpression.cs ..\runtime\Pattern.cs ..\runtime\PatternItems.cs ..\runtime\PatternVariables.cs >nul
..\temp\test.exe >..\temp\test.out
fc /b test10.txt ..\temp\test.out >>..\tests.log
if errorlevel 1 goto end

..\bin\refal.exe test13.ref >test13.cs
csc /debug /out:..\temp\test.exe test13.cs ..\runtime\RefalBase.cs ..\runtime\PassiveExpression.cs ..\runtime\Pattern.cs ..\runtime\PatternItems.cs ..\runtime\PatternVariables.cs >nul
..\temp\test.exe <test13in.txt >..\temp\test.out
fc /b test13.txt ..\temp\test.out >>..\tests.log
if errorlevel 1 goto end

..\bin\refal.exe test14.ref >test14.cs
csc /debug /out:..\temp\test.exe test14.cs ..\runtime\RefalBase.cs ..\runtime\PassiveExpression.cs ..\runtime\Pattern.cs ..\runtime\PatternItems.cs ..\runtime\PatternVariables.cs >nul
..\temp\test.exe test14in.txt ..\temp\test.out
fc /b test14.txt ..\temp\test.out >>..\tests.log
if errorlevel 1 goto end

..\bin\refal.exe test23.ref >test23.cs
csc /debug /out:..\temp\test.exe test23.cs ..\runtime\RefalBase.cs ..\runtime\PassiveExpression.cs ..\runtime\Pattern.cs ..\runtime\PatternItems.cs ..\runtime\PatternVariables.cs >nul
..\temp\test.exe >..\temp\test.out
fc /b test23.txt ..\temp\test.out >>..\tests.log
if errorlevel 1 goto end

..\bin\refal.exe test24.ref >test24.cs
csc /debug /out:..\temp\test.exe test24.cs ..\runtime\RefalBase.cs ..\runtime\PassiveExpression.cs ..\runtime\Pattern.cs ..\runtime\PatternItems.cs ..\runtime\PatternVariables.cs >nul
..\temp\test.exe >..\temp\test.out
fc /b test24.txt ..\temp\test.out >>..\tests.log
if errorlevel 1 goto end

..\bin\refal.exe test25.ref >test25.cs
csc /debug /out:..\temp\test.exe test25.cs ..\runtime\RefalBase.cs ..\runtime\PassiveExpression.cs ..\runtime\Pattern.cs ..\runtime\PatternItems.cs ..\runtime\PatternVariables.cs >nul
..\temp\test.exe >..\temp\test.out
fc /b test25.txt ..\temp\test.out >>..\tests.log
if errorlevel 1 goto end

..\bin\refal.exe test26.ref >test26.cs
csc /debug /out:..\temp\test.exe test26.cs ..\runtime\RefalBase.cs ..\runtime\PassiveExpression.cs ..\runtime\Pattern.cs ..\runtime\PatternItems.cs ..\runtime\PatternVariables.cs >nul
..\temp\test.exe >..\temp\test.out
fc /b test26.txt ..\temp\test.out >>..\tests.log
if errorlevel 1 goto end

:end
cd ..
start type tests.log
