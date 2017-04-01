# Tail
Tail is a Windows console program that displays new rows as they are added to a file in real-time. Similar to the unix tail command

## Installation
Copy Tail/Tail/bin/Release/Tail.exe into c:\windows\system32 or another directory in your path.

## Requirements
Requires .NET Framework 4
Should work out-of-box with Windows 7 or higher 

## Usage

Help text:
~~~
USAGE: Tail <filename> <last n lines (optional)>
EXAMPLE: To display last 500 lines in somefile.txt =>
         Tail somefile.txt 500
<cntrl-C> to exit
~~~

Display usage help text:
~~~
Tail
~~~

List entire contents of somefile.txt then display lines as the file is updated:
~~~
Tail somefile.txt
~~~

List last 50 lines of somefile.txt then display lines as the file is updated:
~~~
Tail somefile.txt 50
~~~
