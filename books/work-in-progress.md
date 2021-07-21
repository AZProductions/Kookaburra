The following text is a work-in-progress. The text is not finalized.
----

## Table of contents
## [**Chapter 1: Getting started**](#1-getting-started)
### [- Download Kookaburra](#1-download-kookaburra)
### [- Getting the basics right](#2-getting-the-basics-right)
### [- Customizing Kookaburra](#3-customizing-kookaburra)
## [**Chapter 2: Kookaburra scripting**]()
## [**Chapter 3: -**]()
## [**Chapter 4: Customizing Kookaburra**]()
----

# **1. Getting Started**
If you've never coded before, programming can seem difficult. For that reason, we created this E-Book to help new coders. For more experienced developers, this guide may not be necessary.

This book contains multiple chapters, sub-chapters and extra tips/tricks from the team who created Kookaburra. Let's code!



## **1.** Download Kookaburra.
When downloading Kookaburra, you can chose between getting it from the Microsoft Store or downloading it via GitHub. On Linux you can download it via GitHub using [***Curl***](https://curl.se/) or [***Wget***](http://www.gnu.org/software/wget/). In this book we exclusively use Windows examples, but you should be able to follow along as a Linux user. So open your favourite browser and head over to 'https://www.github.com/AZProductions/Kookaburra/releases' or 'https://www.github.com/AZProductions/Kookaburra/releases/latest' to download the latest stable build. Click on the little dropdown icon and download the most suitable version for your computer. 

**If you don't know which version you should download, newer computers support x64. If you have an older computer or don't know if the computer is x64 based, just pick the x86 version.**

Some browsers like Edge will stop you from opening the file. These popups are just warnings to tell the user to not download random files off the internet. The official Kookaburra files are safe to use. Just select **keep** and open the file. Windows *SmartScreen* will popup, and again, this is a false positive. Click on *Read more*, and select run. Kookaburra should start up. If you're not so keen with these popups, we recommend you to download it in the [**Microsoft Store**](https://www.microsoft.com/en-us/p/kookaburra/9pcq0dhdtzpm). *You can also manually build the code using **.Net**, it takes longer but then you can verify that the binaries haven't been tampered with. We won't be covering that in this book.*

## **2.** Getting the basics right.
If you haven't already, start Kookaburra by double clicking the recently downloaded file or using the command line. The Kookaburra CLI *(command-line interface)* will appear. Let's get you through the basics. At the top of the terminal, you can see welcome messages, errors alerts and warnings. The line where your cursor is blinking is called the *input field*. You can start off by typing the command ```help```, this opens the help dialog. In the help dialog you can view a list of possible commands with their corresponding descriptions. Press *enter* to return to the input field. Use the ***arrow key*** pointing **up** to see the last typed command, now press the ***arrow key*** pointing **down** to clear the input field. Type the command **'-dt'** to enter the desktop directory of you computer. You can create a file using the mkfile command. Type ```mkfile test.txt```, a file will appear on the desktop. This example only uses a couple of commands, if you want to see the full list, head over to 'https://github.com/AZProductions/Kookaburra#-cli-commands'.

The second way of using Kookaburra is by coding **'.kookaburra'** files. Start off by creating a new file, by opening Notepad. *(You can use any other program to open it, this is just a simple way of doing it.)* Click on the menu header called **'File'** and select **'Save As'**. A window will appear, give the file the name **'helloworld.kookaburra'**. Change the file type to other and save the file in a folder of your choice. Now start by copy-pasting the following code. 
```
print "Hello World!"
app.read()
```
Save the file *(ctrl+s)*, and open it with Kookaburra by dragging the HelloWorld file on to KookaburraShell. A window will popup with the text **'Hello World!'**. Congratulations, you've created your first Kookaburra program! Head over to the second chapter, to learn what the code does.

## **3.** Customizing Kookaburra.
You can add custom commands by editing the **'custom_commands.txt'** file, which is located in the *AppData* directory. Open the Kookaburra settings directory by pressing <kbd>win</kbd> + <kbd>R</kbd> *(Windows key + R)* and typing **'%appdata%/kookaburra'**. Press *enter*, file explorer will appear with the folder open. Double click on the **'custom_commands.txt'** file. Add a new line and copy-paste this the following ```paint=mspaint```. In the CLI type **'paint'**, and Microsoft Paint will open.

You can also change the default text editer with the **'text_editer.txt'** file. Simply open it up and change it to ```c:\program files\windows nt\accessories\wordpad.exe|%arg%``` if you want to edit it in Wordpad. If you want to change it back to Notepad, replace the text in the file with ```notepad.exe|%arg%```.

----
