# Tail
Tail is a Windows console program that displays new rows as they are added to a file in real-time. Similar to the unix tail command

## Usage

Help text:
~~~
USAGE: Tail <filename> <last n lines (optional)>
EXAMPLE: To display last 500 lines in somefile =>
         Tail somefile.txt 500
<cntrl-C> to exit
~~~

Display usage help text:
~~~
Tail
~~~

List entire contents of file then display lines as the file is updated:
~~~
Tail somefile.txt
~~~

List last 50 lines of somefile.txt then display lines as the file is updated:
~~~
Tail somefile.txt 500
~~~
