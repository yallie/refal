@echo off
coco Refal.atg -namespace Refal
if errorlevel 1 goto end
csc /nologo /out:refal.exe /t:exe /debug Refal.cs Scanner.cs Parser.cs
::C:\WINDOWS\Microsoft.NET\Framework\v2.0.50110\csc.exe /nologo /out:yalgol.exe /t:exe /r:MsilCodeProvider.dll Yalgol.cs Scanner.cs Parser.cs CodeBuilder.cs
:end