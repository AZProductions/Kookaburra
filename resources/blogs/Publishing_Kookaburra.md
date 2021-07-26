# Publishing Kookaburra into an exe file.
-----
##### **In this blog, we're going to discuss different methods to publish Kookaburra.**
###### ***26 June 2021 - Blog 2 - Kookaburra 0.6.0***
-----

Currently, Kookaburra doesn't support publishing to a single exe file. That has a big impact when distributing scripts. Luckily, there are a few methods to bypass this inconvenience. In this blog we're only going to use the programming language [**C#**](https://docs.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/), but you can try to follow along with different frameworks. 


## **1. Idea**
To work around the issue of publishing Kookaburra, we can bundle both the file and the **Kookaburra** framework. When running the program, it will extract both Kookaburra and the file into a folder. Which is included in the **exe**, and run Kookaburra with the script.

----
<img src="https://github.com/AZProductions/Kookaburra/blob/main/docs-img/graph2.png" class="center">

----

## **2. Coding**
### In this tutorial we are going to use [**Visual Studio 2019**](https://visualstudio.microsoft.com/vs/) and [**.Net 5**](https://dotnet.microsoft.com/download/dotnet/5.0).

Start off by creating a new Console Application. Then add the KookaburraShell.exe and script in resources. Remember to set ***Copy to output directory*** to **Copy Always**. Then copy-paste the following code.

----

``` c#
string path = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("TestApp.dll", "");
try
{
    Directory.CreateDirectory(path + "TestApp/");
    File.Copy(@"Resources\KookaburraShell.exe", path + "TestApp/src.exe");
    File.Copy(@"Resources\TestApp.kookaburra", path + "TestApp/TestApp.kookaburra");
}
catch { }
ProcessStartInfo startInfo = new ProcessStartInfo();
startInfo.FileName = path + "TestApp/src.exe";
startInfo.Arguments = path + "TestApp/TestApp.kookaburra";
Process.Start(startInfo);
```
----
You can replace **TestApp** with the name of your program. Using this method you can manipulate Kookaburra and add Icons and more. Publish the app by clicking Publish in the ***Solution Explorer***. Now you can distribute your favourite program to whoever you want. **Happy coding!**
