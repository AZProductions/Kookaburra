![Logo](https://raw.githubusercontent.com/AZProductions/Kookaburra/main/.github/icons/cover_art.png) **Kookaburra**
=======
###### ***Cross-platform, Object-oriented Programming language.***
----
[![Latest release](https://img.shields.io/github/v/release/azproductions/kookaburra?include_prereleases)](https://github.com/AZProductions/Kookaburra/releases)
[![GitHub issues](https://img.shields.io/github/issues/azproductions/kookaburra)](https://github.com/AZProductions/Kookaburra/issues)
[![GitHub all releases](https://img.shields.io/github/downloads/azproductions/kookaburra/total)](https://github.com/AZProductions/Kookaburra/releases)
[![.NET Core Version](https://img.shields.io/badge/.NET%20Core-5-blue)](https://dotnet.microsoft.com/download/dotnet/5.0)
[![Twitter Follow](https://img.shields.io/twitter/follow/AZ_Software?style=flat?color=blue)](https://twitter.com/intent/follow?screen_name=AZ_Software)

<a href='//www.microsoft.com/store/apps/9pcq0dhdtzpm?cid=storebadge&ocid=badge'><img src='https://developer.microsoft.com/store/badges/images/English_get-it-from-MS.png' alt='MS-Store link badge' style="vertical-align:middle" center width="96" height="35"/></a>
<a href='https://github.com/AZProductions/Kookaburra/releases'><img src='https://raw.githubusercontent.com/AZProductions/Kookaburra/main/docs-img/badge.png' alt='Github link badge' style="vertical-align:middle" center width="96" height="35"/></a>
<a href='https://kookaburrashell.github.io/mirror/index.html'><img src='https://raw.githubusercontent.com/AZProductions/Kookaburra/main/docs-img/badge2.png' alt='Mirror link badge' style="vertical-align:middle" center width="96" height="35"/></a>

----

## Description
Kookaburra is a free, cross-platform programming language. The syntax is efficient and easy to master.
 
###### ***Remember, KookaburraShell is still in Pre-Release. Features may vary in the final release.***

## Prerequisites
- Using the Installer or stand-alone ***.exe*** file, nothing is needed. 

## 📣 Announcements
### [Publishing Kookaburra into an exe file - ***Blog***](https://github.com/AZProductions/Kookaburra/blob/main/blogs/Publishing_Kookaburra.md)
### [Improving Kookaburra's performance - ***Blog***](https://github.com/AZProductions/Kookaburra/blob/main/blogs/0.6.0_Improvments.md)
### [**Kookaburra is on the Windows Store!** - ***Link***](https://www.microsoft.com/store/apps/9pcq0dhdtzpm)

## 💿 Installation
### For Windows:
 - [Using Microsoft Store.](https://www.microsoft.com/store/apps/9pcq0dhdtzpm)
 - Github Releases:
   1. Head over to the ['**Releases**'](https://github.com/AZProductions/Kookaburra/releases)
   2. Open the 'Assets' drop-down from the desired version of kookaburra *(If you can't decide, we recommend you to pick the [latest version](https://github.com/AZProductions/Kookaburra/releases/latest))*
   3. Download the **KookaburraShell_x_xxx.exe** file.
   4. Open Kookaburra by double clicking the icon of the executable.

-----

### For Linux
Use this command to download it with [**wget**](http://www.gnu.org/software/wget/)
```bash
wget -O /kookaburrashell https://github.com/AZProductions/Kookaburra/releases/download/0.5.0/KookaburraShell_linux_0.5.0
```
Or by using [**curl**](https://curl.se/)
```bash
curl -o /kookaburrashell https://github.com/AZProductions/Kookaburra/releases/download/0.5.0/KookaburraShell_linux_0.5.0
```
And run it by typing
```
./KookaburraShell
```
Run Kookaburra and open a .kookaburra file with
```
./KookaburraShell /filelocation/example.kookaburra
```

## ⏱ Get started
In kookaburra you can choose between using the terminal and scripting with **'.kookaburra'** files.
In this guide we wil only cover the topic of programming you own applications.

1. Create a file with a "**.kookaburra**" extension. Example "***helloworld.kookaburra***".
## ![Creating folder and file,](https://media.giphy.com/media/h8FwBMfUXUiRcwM3CZ/giphy.gif)
2. Open the file with a code/text editor, and start coding!
## ![Creating folder and file,](https://media.giphy.com/media/QLLavb6TzdhgDnv74d/giphy.gif)
3. Slect "**Open with**" and select ***kookaburrashell.exe*** in file explorer.
## ![Setting up "Open with"](https://media.giphy.com/media/LVGzbtygblYO4zPzqo/giphy.gif)
4. Double click the file and Kookaburra wil open up.
### [**Watch the full tutorial here.**](https://www.youtube.com/watch?v=ou1rCcN5wEQ)

## [👩‍💻 Syntax](https://azproductions.github.io/Kookaburra/syntax/)
[Availible on the website.](https://azproductions.github.io/Kookaburra/syntax/)

## 📒 CLI Commands
- cp
- cd
- mv
- rm
- edit
- mkdir
- mkfile
- help
- whoami
- ls
- dir
- drives
- browse/explore/explorer
- ipconfig
- download
- send (localtcp)
- receive (localtcp)
- password
- [read the full list here](github.com/404)

## 📐 Examples
### File writer
```
# 'app.debug-off' - remove debug text.
import FileIO
print @"Location:"
string location = app.read()
print "-------------"
print @"value:"
string value = app.readline()
new filewriter(location, value)
print @"finished, value = "
print @value
print @"."
app.read() 
```
### All Colors in Kookaburra
```
app.debug-off
print "- Colors in Kookaburra - "
app.color = blue
print "Blue"
app.color = red
print "Red"
app.color = green
print "Green"
app.color = yellow
print "Yellow"
app.color = white
print "white"
app.read()
```


## Security
### **When downloading Kookaburra**
We at AZ Software take security very seriously, that's why on every release page we always show the checksum. You can use online tools to check if the SHA-256 match up.

### **Opening user-created *.kookaburra* or *.kbproject* files**
We recommend you to scan the donwnloaded file with an Antivirus before opening or executing the file.
You can also open the file in a **code/text editor** and read exactly what the file does.

## About Kookaburra

![graph](https://raw.githubusercontent.com/AZProductions/Kookaburra/main/.github/icons/graph.png) 
##### ***AZ Software is not responsible for any harm to your device(s).***
