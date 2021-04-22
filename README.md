![Logo](https://raw.githubusercontent.com/AZProductions/Kookaburra/main/.github/icons/cover_art.png) **KookaburraShell**
=======
###### ***Cross-Platform, Object-Oriented Programming language.***
----
![Build Passing](https://img.shields.io/badge/Build-Passing-green)
![GitHub issues](https://img.shields.io/github/issues/azproductions/kookaburra)
![GitHub all releases](https://img.shields.io/github/downloads/azproductions/kookaburra/total)
![.NET Core Version](https://img.shields.io/badge/.NET%20Core-3.1-yellow)

## Description
Kookaburra is a free, cross-platform programming language. The syntax is efficient and easy to master.
 
###### ***Remember, KookaburraShell is still in Pre-Release. Features may vary in the final release.***

## Prerequisites
- Using the Installer or stand-alone ***.exe*** file, nothing is needed. 
- If you use the unpacked version of Kookaburra ***.Net 3.1*** is automaticly installed.

## Announcements
### [Official Linux support!](https://www.reddit.com/r/Kookaburra/comments/mqolk1/official_linux_support/)
[After a lot of testing, we finally released Kookaburra for Linux.](https://www.reddit.com/r/Kookaburra/comments/mqolk1/official_linux_support/)

### Mac x64 support?

## Installation
1. Head over to the ["**Releases**"](https://github.com/AZProductions/Kookaburra/releases)
2. Open the "Assets" drop-down from the desired version of kookaburra *(If you can't decide, we recommend you to pick the [latest version](https://github.com/AZProductions/Kookaburra/releases/latest))*
3. Select what you want to install.
* **KookaburraShell_x_xxx(.exe)**
* * Stand-alone executable with all necessary prerequisites included.
* **KookaburraInstaller.msi**
* * Windows installer, installs necessary prerequisites and creates a directory in "**Program Files(x86)**"

## Instructions
Open Kookaburra by double clicking the icon on the **Desktop** or **Start menu**.

## Get started
In kookaburra you can choose between using the terminal and scripting with **'.kookaburra'** files.
In this guide we wil only cover the topic of programming you own applications.

1. Create a file with a "**.kookaburra**" extension. Example "***helloworld.kookaburra***".
## ![Creating folder and file,](https://media.giphy.com/media/h8FwBMfUXUiRcwM3CZ/giphy.gif)
2. Open the file with a code/text editor, and start coding!
## ![Creating folder and file,](https://media.giphy.com/media/QLLavb6TzdhgDnv74d/giphy.gif)
3. Slect "**Open with**" and select ***kookaburrashell.exe*** in file explorer.
## ![Setting up "Open with"](https://media.giphy.com/media/LVGzbtygblYO4zPzqo/giphy.gif)
4. Double click the file and Kookaburra wil open up.
### [**Read the full tutorial here.**](https://github.com/404)

## Examples
### File writer
```
# 'app.debug-off' - remove debug text.
import FileIO
print @"Location:"
string location = app.readline()
print "-------------"
print @"value:"
string value = app.readline()
new filewriter(location, value)
print @"finished, value = "
print @value
print @"."
app.read() 
```
## Commands
### CLI commands
- cp
- cd
- mv
- rm
- mkdir
- mkfile
- help
- [read the full list here](github.com/404)

## Security
### **When downloading Kookaburra**
We at AZ Software take security very seriously, that's why on every release page we always show the checksum. You can use online tools to check if the SHA-256 match up.

### **Opening user-created *.kookaburra* or *.kbproject* files**
We recommend you to scan the donwnloaded file with an Antivirus before opening or executing the file.
You can also open the file in a **code/text editor** and read exactly what the file does.


![graph](https://raw.githubusercontent.com/AZProductions/Kookaburra/main/.github/icons/graph.png) 
##### ***AZ Software is not responsible for any harm to your device(s).***
