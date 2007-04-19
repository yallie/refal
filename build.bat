@echo off
rem Generate compiler code
tools\coco compiler\Refal.atg -namespace Refal -frames tools
if errorlevel 1 goto end

rem Compile sources
csc /nologo /out:bin\refal.exe /t:exe /debug compiler\Refal.cs compiler\Scanner.cs compiler\Parser.cs compiler\Syntax.cs compiler\CodeBuilder.cs compiler\CodeVisitor.cs compiler\FormatVisitor.cs compiler\CSharpVisitor.cs
:end