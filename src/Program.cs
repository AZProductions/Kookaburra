using System;
using Spectre.Console;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace KookaburraShell {
        class Program {
                static void Main(string[] args) {
                                Console.TreatControlCAsInput = false;
                                Isettingsconf.Currentdir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                                Isettingsconf.Envloc = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @ "/Kookaburra/";
                                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
                                        Isettingsconf.Envloc = "/home/" + Environment.UserName + "/Kookaburra/";
                                }
                                script_var.Intcount = 0;
                                Isettingsconf.Quietmode = false;
                                fileoutput();
                                Checkenvironment();
                                Assembly execAssembly = Assembly.GetCallingAssembly();
                                AssemblyName version = execAssembly.GetName();
                                string versionstring = version.Version.ToString();
                                bool network_available = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
                                Isettingsconf.Networkkey = network_available;
                                if (network_available) {
                                        Console.Title = "Kookaburra Shell";
                                } else {
                                        Console.Title = "Kookaburra Shell (ofline mode)";
                                }
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("AZ Software, Kookaburra [" + versionstring + "]");
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("AZ Software is not responsible for any harm to your device(s).");
                                Console.Write("Use the command ");
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("'tos' ");
                                //AnsiConsole.Markup("'[Link=https://github.com/AZProductions/Kookaburra/blob/main/TOS.md]tos[/]' ");
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("to read the Terms of Service.");
                                Console.WriteLine("");
                                try {
                                        string[] vs = File.ReadAllLines(Isettingsconf.Envloc + @ "/conf.txt");
                                        foreach(string Lines in vs) {
                                                if (Lines == "FR=true") {
                                                        Isettingsconf.FR = true;
                                                }
                                        }
                                } catch {
                                        Isettingsconf.FR = true; /*Revert to default, shows FR.*/
                                }
                                if (Isettingsconf.FR) {
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.Write("Type ");
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        Console.Write("'Help' ");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.Write("to see all available commands.");
                                        Console.WriteLine("");
                                        Console.WriteLine("");
                                }
                                //Checkforupdates();
                                Console.ForegroundColor = ConsoleColor.Black;
                                bool run = true;
                                if (Isettingsconf.Quietmode == true) {
                                        run = false; /*Console.Clear();*/
                                } else {
                                        packagecheck();
                                }
                                while (run) {
                                        bool validcommandfound = false;
                                        if (Isettingsconf.Currentdir == "") {
                                                if (Isettingsconf.ShowRainbow == true) {
                                                        AnsiConsole.Markup("[orange1]K[/][yellow1]o[/][greenyellow]o[/][lime]k[/][aqua]a[/][turquoise2]b[/][blue]u[/][hotpink_1]r[/][deeppink2]r[/][red]a[/][blue]@[/][white]>[/]");
                                                } else {
                                                        Console.ForegroundColor = ConsoleColor.Blue;
                                                        Console.Write("Kookaburra");
                                                        Console.ForegroundColor = ConsoleColor.White;
                                                        Console.Write(">");
                                                }
                                        } else {
                                                if (Isettingsconf.ShowRainbow == true) {
                                                        AnsiConsole.Markup("[orange1]K[/][yellow1]o[/][greenyellow]o[/][lime]k[/][aqua]a[/][turquoise2]b[/][blue]u[/][hotpink_1]r[/][deeppink2]r[/][red]a[/][blue]@[/]");
                                                        Console.ForegroundColor = ConsoleColor.Green;
                                                        Console.Write(Isettingsconf.Currentdir);
                                                        Console.ForegroundColor = ConsoleColor.White;
                                                        Console.Write(">");
                                                } else {
                                                        Console.ForegroundColor = ConsoleColor.Blue;
                                                        Console.Write("Kookaburra@");
                                                        Console.ForegroundColor = ConsoleColor.Green;
                                                        Console.Write(Isettingsconf.Currentdir);
                                                        Console.ForegroundColor = ConsoleColor.White;
                                                        Console.Write(">");
                                                }
                                        }
                                        string input = Console.ReadLine().ToLower();

                                        if (input == "help") {
                                                validcommandfound = true;
                                                Console.WriteLine(Environment.NewLine);
                                                AnsiConsole.Render(
                                                        new FigletText("Kookaburra")
                                                        .Centered()
                                                        .Color(Color.Green));

                                                Console.WriteLine(Environment.NewLine);
                                                var rule1 = new Rule("[white bold]All CLI  Commands[/]");
                                                AnsiConsole.Render(rule1);
                                                var table = new Table().Centered();
                                                table.AddColumn("Command");
                                                table.AddColumn(new TableColumn("Description").Centered());
                                                table.AddRow("cp", "[green]Copy files from one place to another.[/]");
                                                table.AddRow("clear [blue]or[/] cls", "[green]Clears the screen.[/]");
                                                table.AddRow("winreset", "[green]Resets the window height and width. (Windows only)[/]");
                                                table.AddRow("rm", "[green]Delete files.[/]");
                                                table.AddRow("rmdir", "[green]Delete folders.[/]");
                                                table.AddRow("mkdir", "[green]Make folders.[/]");
                                                table.AddRow("mkfile", "[green]Make files.[/]");
                                                table.AddRow("cd", "[green]Change to a directory or file.[/]");
                                                table.AddRow("cd ..", "[green]Go back a directory.[/]");
                                                table.AddRow("ls" + "[blue] or [/]" + "dir", "[green]Go back a directory.[/]");
                                                table.AddRow("whoami", "[green]Shows the pc name and username.[/]");
                                                table.AddRow("drives", "[green]List all drives in your computer.[/]");
                                                table.AddRow("browse" + "[blue] or [/]" + "explore" + "[blue] or [/]" + "explorer", "[green]Opens file-explorer in the current directory.[/]");
                                                table.AddRow("ipconfig", "[green]Show all internet settings from your computer.[/]");
                                                table.AddRow("download", "[green]Download files from an internet server.[/]");
                                                table.AddRow("password", "[green]Generate passwords.[/]");
                                                table.AddRow("tree", "[green]Renders a detailed list of all the files and folders in a directory.[/]");
                                                AnsiConsole.Render(table);
                                                var rule4 = new Rule("[white bold]CLI Shortcuts[/]");
                                                var table1 = new Table().Centered();
                                                AnsiConsole.Render(rule4);
                                                table1.AddColumn("Shortcut");
                                                table1.AddColumn(new TableColumn("Description").Centered());
                                                table1.AddRow("-c:/, it works with all drives.", "[green]Automatically gets the root of the set directory.[/]");
                                                table1.AddRow("-env", "[green]The location of the configuration directory of kookaburra.[/]");
                                                table1.AddRow("-r", "[green]Goes to the root of the current directory.[/]");
                                                table1.AddRow("-cookies", "[green]The directory that serves as a common repository for Internet cookies.[/]");
                                                table1.AddRow("-desktop or -dt", "[green]The logical Desktop rather than the physical file system location.[/]");
                                                table1.AddRow("-favorites", "[green]The directory that serves as a common repository for the user's favorite items.[/]");
                                                table1.AddRow("-fonts", "[green]A virtual folder that contains fonts.[/]");
                                                table1.AddRow("-history", "[green]The directory that serves as a common repository for Internet history items.[/]");
                                                table1.AddRow("-personal", "[green]The directory that serves as a common repository for documents. This member is equivalent to MyDocuments.[/]");
                                                table1.AddRow("-programs", "[green]The directory that contains the user's program groups.[/]");
                                                table1.AddRow("-recent", "[green]The directory that contains the user's most recently used documents.[/]");
                                                table1.AddRow("-resources", "[green]The file system directory that contains resource data.[/]");
                                                table1.AddRow("-st", "[green]The directory that contains the Start menu items.[/]");
                                                table1.AddRow("-startup", "[green]The directory that corresponds to the user's Startup program group. The system starts these programs whenever a user logs on or starts Windows.[/]");
                                                table1.AddRow("-system", "[green]The System directory.[/]");
                                                table1.AddRow("-templates", "[green]The directory that serves as a common repository for document templates.[/]");
                                                table1.AddRow("-windows", "[green]The Windows directory or SYSROOT. This corresponds to the %windir% or %SYSTEMROOT% environment variables.[/]");
                                                table1.AddRow("-c", "[green]Clears location.[/]");
                                                AnsiConsole.Render(table1);
                                                var rule5 = new Rule("[white bold]Open-Source Libraries[/]");
                                                AnsiConsole.Render(rule5);
                                                var table2 = new Table().Centered();
                                                table2.AddColumn(new TableColumn("Name").Centered());
                                                table2.AddColumn(new TableColumn("Version").Centered());
                                                table2.AddColumn(new TableColumn("Licence").Centered());
                                                table2.AddColumn(new TableColumn("Link").Centered());
                                                table2.AddColumn(new TableColumn("Notes").Centered());
                                                table2.AddRow("[bold]Spectre.Console[/]", "0.41 [italic](Nuget)[/]", "[bold]MIT[/]", "[blue]https://spectreconsole.net/[/]", "The Spectre system is our favourite markup library, it supports a variety of features. [bold]We recommend using it![/]");
                                                table2.AddRow("[bold]Spectre.Console.ImageSharp[/]", "0.41 [italic](Nuget)[/]", "[bold]MIT[/]", "[blue]https://spectreconsole.net/widgets/canvas-image[/]", "Image Sharp is part of the Spectre.Console system, its a fork from the SixLabors.ImageSharp project.");
                                                AnsiConsole.Render(table2);
                                                Console.WriteLine(Environment.NewLine);
                                        }

                                        if (input == "clear" || input == "cls") {
                                                validcommandfound = true;
                                                Console.Clear();
                                        }

                                        if (input == "tos") {
                                                validcommandfound = true;
                                                if (NetworkInterface.GetIsNetworkAvailable()) {
                                                        var psi = new ProcessStartInfo {
                                                                FileName = "https://github.com/AZProductions/Kookaburra/blob/main/TOS.md",
                                                                        UseShellExecute = true
                                                        };
                                                        Process.Start(psi);
                                                } else {
                                                        AnsiConsole.Markup("[bold]Terms of Service[/]");
                                                        Console.WriteLine(Environment.NewLine);
                                                        AnsiConsole.Markup("When using Kookaburra, AZ Software isn't responsible for any harm or damage done by the end-user. We do not track any data, everything is stored locally on your computer. If users make harmful .kookaburra files, AZ Software isn’t responsible for the damage done. We provide users a tool, and discourage using it for malicious purposes.");
                                                        Console.WriteLine(Environment.NewLine);
                                                }
                                        }

                                        if (input == "winreset") {
                                                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                                                        validcommandfound = true;
                                                        Console.WindowWidth = Isettingsconf.SavedWidth;
                                                        Console.WindowHeight = Isettingsconf.SavedHeight;
                                                }
                                        }

                                        if (input == "exit" || input == "close") {
                                                validcommandfound = true;
                                                Environment.Exit(0);
                                        }

                                        if (input == "env") {
                                                validcommandfound = true;
                                                var table = new Table();
                                                table.AddColumn(new TableColumn("[bold green]ENV Data[/]").Centered()).HeavyHeadBorder();
                                                // Add some columns
                                                table.AddRow(System.Reflection.Assembly.GetExecutingAssembly().Location);
                                                table.AddRow(System.AppContext.BaseDirectory);
                                                table.AddRow(Isettingsconf.Envloc);
                                                table.AddRow(Path.GetFullPath("packages/") + "*" + Isettingsconf.Envloc);
                                                table.AddRow(System.AppContext.TargetFrameworkName);
                                                AnsiConsole.Render(table);
                                        }

                                        if (input == "restart") {
                                                validcommandfound = true;
                                                run = false;
                                                Console.Clear();
                                                Thread.Sleep(123);
                                                System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location.Replace(".dll", ".exe"));
                                                Environment.Exit(0);
                                        }

                                        if (input == "restart -p") {
                                                validcommandfound = true;
                                                run = false;
                                                packagecheck();
                                                run = true;
                                        }

                                        if (input.StartsWith("cd")) {
                                                if (input == "cd" || input == "cd ") {
                                                        validcommandfound = false;
                                                } else if (input == "cd ..") {} else if (input.StartsWith("cd ..")) {
                                                        validcommandfound = false;
                                                } else {
                                                        validcommandfound = true;
                                                        string loc = input.Replace("cd ", "");
                                                        if (loc.StartsWith("/")) {
                                                                if (loc.StartsWith("/")) {
                                                                        loc = Isettingsconf.Currentdir + loc;
                                                                        if (File.Exists(loc)) {
                                                                                // This path is a file
                                                                                Isettingsconf.Filedir = true;
                                                                                Isettingsconf.Currentdir = loc;

                                                                        } else if (Directory.Exists(loc)) {
                                                                                // This path is a directory
                                                                                Isettingsconf.Filedir = false;
                                                                                Isettingsconf.Currentdir = loc;
                                                                        } else {
                                                                                Console.WriteLine("{0} is not a valid file or directory.", loc);
                                                                        }
                                                                } else {
                                                                        loc = Isettingsconf.Currentdir + loc;
                                                                        if (File.Exists(loc)) {
                                                                                // This path is a file
                                                                                Isettingsconf.Filedir = true;
                                                                                Isettingsconf.Currentdir = loc;

                                                                        } else if (Directory.Exists(loc)) {
                                                                                // This path is a directory
                                                                                Isettingsconf.Filedir = false;
                                                                                Isettingsconf.Currentdir = loc;
                                                                        } else {
                                                                                Console.WriteLine("{0} is not a valid file or directory.", loc);
                                                                        }
                                                                }
                                                        } else if (loc.Contains(":/") || loc.Contains(@ ":\")) {
                                                                                if (File.Exists(loc)) {
                                                                                        // This path is a file
                                                                                        Isettingsconf.Filedir = true;
                                                                                        Isettingsconf.Currentdir = loc;

                                                                                } else if (Directory.Exists(loc)) {
                                                                                        // This path is a directory
                                                                                        Isettingsconf.Filedir = false;
                                                                                        Isettingsconf.Currentdir = loc;
                                                                                } else {
                                                                                        Console.WriteLine("{0} is not a valid file or directory.", loc);
                                                                                }
                                                                        } else if (!loc.Contains("/") || !loc.Contains(@ "\")) {
                                                                                                string extendedloc = "/" + loc;
                                                                                                loc = Isettingsconf.Currentdir + extendedloc;
                                                                                                //Console.WriteLine(loc);
                                                                                                if (File.Exists(loc)) {
                                                                                                        // This path is a file
                                                                                                        Isettingsconf.Filedir = true;
                                                                                                        Isettingsconf.Currentdir = loc;

                                                                                                } else if (Directory.Exists(loc)) {
                                                                                                        // This path is a directory
                                                                                                        Isettingsconf.Filedir = false;
                                                                                                        Isettingsconf.Currentdir = loc;
                                                                                                } else {
                                                                                                        Console.WriteLine("{0} is not a valid file or directory.", loc);
                                                                                                }
                                                                                        } else {
                                                                                                if (File.Exists(loc)) {
                                                                                                        // This path is a file
                                                                                                        Isettingsconf.Filedir = true;
                                                                                                        Isettingsconf.Currentdir = loc;

                                                                                                } else if (Directory.Exists(loc)) {
                                                                                                        // This path is a directory
                                                                                                        Isettingsconf.Filedir = false;
                                                                                                        Isettingsconf.Currentdir = loc;
                                                                                                } else {
                                                                                                        Console.WriteLine("{0} is not a valid file or directory.", loc);
                                                                                                }
                                                                                        }
                                                                                }
                                                                        }

                                                                        if (input.StartsWith("mkdir")) {
                                                                                string name = input.Replace("mkdir ", "");
                                                                                if (input == "mkdir") {} else {
                                                                                        try {
                                                                                                validcommandfound = true;
                                                                                                Directory.CreateDirectory(Isettingsconf.Currentdir + "/" + name);
                                                                                        } catch {}
                                                                                }
                                                                        }

                                                                        if (input.StartsWith("mkfile")) {
                                                                                string name = input.Replace("mkfile ", "");
                                                                                if (input == "mkfile") {} else {
                                                                                        try {
                                                                                                validcommandfound = true;
                                                                                                File.Create(Isettingsconf.Currentdir + "/" + name);
                                                                                        } catch {}
                                                                                }
                                                                        }

                                                                        if (input.StartsWith("scan ")) {
                                                                                string loc = input.Replace("scan ", "");
                                                                                List < string > Results = new List < string > ();
                                                                                int Rating = 100;
                                                                                if (File.Exists(loc)) {
                                                                                        validcommandfound = true;
                                                                                        Console.CursorVisible = false;
                                                                                        Scanfile(loc);
                                                                                        Logicfunc();
                                                                                        Console.CursorVisible = true;
                                                                                }

                                                                                void Scanfile(string loc) {
                                                                                        string[] Content = File.ReadAllLines(loc);
                                                                                        int Totalline = Content.Count();
                                                                                        int CurrentLine = 0;
                                                                                        AnsiConsole.Progress()
                                                                                                .Start(ctx => {
                                                                                                        var task = ctx.AddTask("[green]Scanning code base.[/]");
                                                                                                        foreach(string lines in Content) {
                                                                                                                CurrentLine++;
                                                                                                                task.Value(100 * CurrentLine / Totalline);
                                                                                                                if (task.Value > 20) {
                                                                                                                        task.Description = "[yellow]Examining code base.[/]";
                                                                                                                }
                                                                                                                Random random = new Random();
                                                                                                                Thread.Sleep(random.Next(200, 1000));
                                                                                                                Calc(lines);
                                                                                                        }
                                                                                                });
                                                                                }

                                                                                void Calc(string data) {
                                                                                        if (data.StartsWith("new filewriter(")) {
                                                                                                string
                                                                                                var = data.Replace("new filewriter(", "").TrimEnd(')');
                                                                                                string[] valuearray =
                                                                                                        var.Split(", ");
                                                                                                Results.Add("fr=" + valuearray[0] + "|" + valuearray[1]);
                                                                                                Rating = Rating - 40;
                                                                                                if (File.ReadLines(loc).Last() == "exit") {
                                                                                                        Results.Add("!exit");
                                                                                                        Rating = Rating - 5;
                                                                                                }
                                                                                        } else if (data.StartsWith("")) {}
                                                                                }

                                                                                void Logicfunc() {
                                                                                        Console.WriteLine("");
                                                                                        if (Rating <= 30 || Rating == 30) {
                                                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                                        } else if (Rating >= 30 && Rating <= 50) {
                                                                                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                                                                        } else if (Rating >= 50 && Rating <= 70) {
                                                                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                                                        } else if (Rating >= 70) {
                                                                                                Console.ForegroundColor = ConsoleColor.Green;
                                                                                        }
                                                                                        Console.WriteLine("Rating: " + Rating + "/100");
                                                                                        foreach(string lines in Results) {
                                                                                                if (lines.StartsWith("fr=")) {
                                                                                                        string output = lines.Replace("fr=", "");
                                                                                                        string[] valuearray = output.Split("|");
                                                                                                        Console.WriteLine("");
                                                                                                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                                                                                                        Console.Write("Warning ");
                                                                                                        Console.ForegroundColor = ConsoleColor.White;
                                                                                                        Console.Write("| This code is writing the value '" + valuearray[1] + "' to the file '" + valuearray[0] + "'.");
                                                                                                } else if (lines == "!exit") {
                                                                                                        Console.WriteLine("");
                                                                                                        Console.ForegroundColor = ConsoleColor.Blue;
                                                                                                        Console.Write("Tip ");
                                                                                                        Console.ForegroundColor = ConsoleColor.White;
                                                                                                        Console.Write("| This code should end with 'exit' to properly exit the application.");
                                                                                                }
                                                                                        }
                                                                                        Console.WriteLine("");
                                                                                        Console.WriteLine("");
                                                                                }
                                                                        }
                                                                        if (input == "kbconfig" || input == "settings") {
                                                                                validcommandfound = true;

                                                                                var Menu = AnsiConsole.Prompt(
                                                                                        new MultiSelectionPrompt < string > ()
                                                                                        .Title("Settings | [green]Kookaburra[/]")
                                                                                        .NotRequired()
                                                                                        .PageSize(10)
                                                                                        .MoreChoicesText("[grey](Move up and down to select different settings)[/]")
                                                                                        .InstructionsText(
                                                                                                "[grey](Press [blue]<space>[/] to toggle a setting, " +
                                                                                                "[green]<enter>[/] to save and exit)[/]")
                                                                                        .AddChoices(new [] {
                                                                                                "Use Internet.",
                                                                                                "Ask for updates.",
                                                                                                "Use emojis",
                                                                                        }));

                                                                                foreach(string items in Menu) {
                                                                                        AnsiConsole.WriteLine(items);
                                                                                }
                                                                        }

                                                                        if (input == "drives") {
                                                                                validcommandfound = true;
                                                                                DriveInfo[] allDrives = DriveInfo.GetDrives();

                                                                                foreach(DriveInfo d in allDrives) {
                                                                                        try {
                                                                                                Console.WriteLine("Drive {0}", d.Name);
                                                                                                Console.WriteLine("  Drive type: {0}", d.DriveType);
                                                                                                if (d.IsReady == true) {
                                                                                                        Console.WriteLine("  Volume label: {0}", d.VolumeLabel);
                                                                                                        Console.WriteLine("  File system: {0}", d.DriveFormat);
                                                                                                        Console.WriteLine(
                                                                                                                "  Available space to current user:{0, 15} bytes",
                                                                                                                d.AvailableFreeSpace);

                                                                                                        Console.WriteLine(
                                                                                                                "  Total available space:          {0, 15} bytes",
                                                                                                                d.TotalFreeSpace);

                                                                                                        Console.WriteLine(
                                                                                                                "  Total size of drive:            {0, 15} bytes ",
                                                                                                                d.TotalSize);
                                                                                                }
                                                                                        } catch {}
                                                                                }
                                                                        }

                                                                        if (input.StartsWith("password")) {
                                                                                validcommandfound = true;
                                                                                if (input == "password") {
                                                                                        Console.WriteLine("");
                                                                                        Console.ForegroundColor = ConsoleColor.Red;
                                                                                        Console.WriteLine("Disclaimer");
                                                                                        Console.WriteLine("Randomly generated passwords are not saved!");
                                                                                        Console.WriteLine("These passwords are generated using a simple script.");
                                                                                        Console.Write("Have a look at the code: ");
                                                                                        Console.Write("'");
                                                                                        Console.ForegroundColor = ConsoleColor.Green;
                                                                                        Console.Write("https://github.com/AZProductions/Kookaburra/blob/main/packages/password/generator.cs");
                                                                                        Console.ForegroundColor = ConsoleColor.Red;
                                                                                        Console.Write("'");
                                                                                        Console.WriteLine("");
                                                                                        Console.WriteLine("");

                                                                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                                                                        int count = 6;
                                                                                        for (int value = 0; value < count; value++) {
                                                                                                int length = 12;
                                                                                                Random random = new Random();
                                                                                                string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz$$$$!!!!!!!!!!!!";
                                                                                                StringBuilder result = new StringBuilder(length);
                                                                                                for (int i = 0; i < length; i++) {
                                                                                                        result.Append(characters[random.Next(characters.Length)]);
                                                                                                }
                                                                                                Console.WriteLine(result.ToString());
                                                                                        }
                                                                                        Console.WriteLine("");
                                                                                } else {
                                                                                        string value = input.Replace("password ", "");
                                                                                        if (value.Contains(" ")) {
                                                                                                string[] valuearray = value.Split(" ");
                                                                                                Console.WriteLine("");
                                                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                                                Console.WriteLine("Disclaimer");
                                                                                                Console.WriteLine("Randomly generated passwords are not saved!");
                                                                                                Console.WriteLine("These passwords are generated using a simple script.");
                                                                                                Console.Write("Have a look at the code: ");
                                                                                                Console.Write("'");
                                                                                                Console.ForegroundColor = ConsoleColor.Green;
                                                                                                Console.Write("https://github.com/AZProductions/Kookaburra/blob/main/packages/password/generator.cs");
                                                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                                                Console.Write("'");
                                                                                                Console.WriteLine("");
                                                                                                Console.WriteLine("");

                                                                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                                                                try {
                                                                                                        int count = Int32.Parse(valuearray[1]);
                                                                                                        int length = Int32.Parse(valuearray[0]);
                                                                                                        for (int value1 = 0; value1 < count; value1++) {
                                                                                                                Random random = new Random();
                                                                                                                string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz$$$$!!!!!!!!!!!!";
                                                                                                                StringBuilder result = new StringBuilder(length);
                                                                                                                for (int i = 0; i < length; i++) {
                                                                                                                        result.Append(characters[random.Next(characters.Length)]);
                                                                                                                }
                                                                                                                Console.WriteLine(result.ToString());
                                                                                                        }
                                                                                                        Console.WriteLine("");
                                                                                                } catch {
                                                                                                        Console.WriteLine("error");
                                                                                                }
                                                                                        } else {
                                                                                                Console.WriteLine("");
                                                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                                                Console.WriteLine("Disclaimer");
                                                                                                Console.WriteLine("Randomly generated passwords are not saved!");
                                                                                                Console.WriteLine("These passwords are generated using a simple script.");
                                                                                                Console.Write("Have a look at the code: ");
                                                                                                Console.Write("'");
                                                                                                Console.ForegroundColor = ConsoleColor.Green;
                                                                                                Console.Write("https://github.com/AZProductions/Kookaburra/blob/main/packages/password/generator.cs");
                                                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                                                Console.Write("'");
                                                                                                Console.WriteLine("");
                                                                                                Console.WriteLine("");

                                                                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                                                                try {
                                                                                                        int count = 6;
                                                                                                        int length = Int32.Parse(value);
                                                                                                        for (int value1 = 0; value1 < count; value1++) {
                                                                                                                Random random = new Random();
                                                                                                                string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz$$$$!!!!!!!!!!!!";
                                                                                                                StringBuilder result = new StringBuilder(length);
                                                                                                                for (int i = 0; i < length; i++) {
                                                                                                                        result.Append(characters[random.Next(characters.Length)]);
                                                                                                                }
                                                                                                                Console.WriteLine(result.ToString());
                                                                                                        }
                                                                                                        Console.WriteLine("");
                                                                                                } catch {
                                                                                                        Console.WriteLine("error");
                                                                                                }
                                                                                        }
                                                                                }
                                                                        }

                                                                        if (input == "explorer" || input == "explore" || input == "browse" || input == "view") {
                                                                                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                                                                                        validcommandfound = true;
                                                                                        if (Isettingsconf.Currentdir.Contains(":")) {
                                                                                                try {
                                                                                                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo() {
                                                                                                                FileName = Isettingsconf.Currentdir,
                                                                                                                        UseShellExecute = true,
                                                                                                                        Verb = "open"
                                                                                                        });
                                                                                                } catch {
                                                                                                        //Console.WriteLine("error");
                                                                                                }
                                                                                        } else {
                                                                                                //open explorer
                                                                                                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo() {
                                                                                                        FileName = "explorer.exe",
                                                                                                                UseShellExecute = true,
                                                                                                                Verb = "open"
                                                                                                });
                                                                                        }
                                                                                }
                                                                        }

                                                                        if (input.StartsWith("rm")) {
                                                                                if (input == "rm" || input == "rm ") {
                                                                                        validcommandfound = false;
                                                                                } else {
                                                                                        validcommandfound = true;
                                                                                        string loc = input.Replace("rm ", "");

                                                                                        // get the file attributes for file or directory
                                                                                        FileAttributes attr = File.GetAttributes(loc);

                                                                                        if (attr.HasFlag(FileAttributes.Directory)) {
                                                                                                try {
                                                                                                        Directory.Delete(loc);
                                                                                                } catch {}
                                                                                        } else {
                                                                                                try {
                                                                                                        File.Delete(loc);
                                                                                                } catch {}
                                                                                        }
                                                                                }
                                                                        }

                                                                        if (input.StartsWith("cp")) {
                                                                                validcommandfound = true;
                                                                                string locs = input.Replace("cp ", "");
                                                                                Console.WriteLine("File to copy:");
                                                                                string firstloc = Console.ReadLine();
                                                                                Console.WriteLine("Location to copy to:");
                                                                                string secondloc = Console.ReadLine();
                                                                                // -o to overwrite
                                                                                if (locs.EndsWith("-o")) {
                                                                                        try {
                                                                                                File.Copy(firstloc, secondloc, true);
                                                                                        } catch {
                                                                                                Console.WriteLine("");
                                                                                                Console.WriteLine("Error, file not found!");
                                                                                                Console.WriteLine("");
                                                                                        }
                                                                                } else {
                                                                                        if (File.Exists(secondloc)) {
                                                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                                                Console.WriteLine("Destination location is already occupied.");
                                                                                                Console.WriteLine("");
                                                                                                Console.Write("Use ");
                                                                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                                                                Console.Write("-o ");
                                                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                                                Console.Write("at the end of the command to overwrite.");
                                                                                                Console.WriteLine("");
                                                                                                Console.WriteLine("Example: copy -o");
                                                                                                Console.WriteLine("");
                                                                                        } else {
                                                                                                try {
                                                                                                        File.Copy(firstloc, secondloc, false);
                                                                                                } catch {
                                                                                                        Console.WriteLine("");
                                                                                                        Console.WriteLine("Error, file not found!");
                                                                                                        Console.WriteLine("");
                                                                                                }
                                                                                        }
                                                                                }
                                                                        }

                                                                        if (input.StartsWith("mv")) {
                                                                                validcommandfound = true;
                                                                                string locs = input.Replace("mv ", "");
                                                                                Console.WriteLine("File to move:");
                                                                                string firstloc = Console.ReadLine();
                                                                                Console.WriteLine("Location to move to:");
                                                                                string secondloc = Console.ReadLine();
                                                                                // -o to overwrite
                                                                                if (locs.EndsWith("-o")) {
                                                                                        try {
                                                                                                File.Move(firstloc, secondloc, true);
                                                                                        } catch {
                                                                                                Console.WriteLine("");
                                                                                                Console.WriteLine("Error, file not found!");
                                                                                                Console.WriteLine("");
                                                                                        }
                                                                                } else {
                                                                                        if (File.Exists(secondloc)) {
                                                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                                                Console.WriteLine("Destination location is already occupied.");
                                                                                                Console.WriteLine("");
                                                                                                Console.Write("Use ");
                                                                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                                                                Console.Write("-o ");
                                                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                                                Console.Write("at the end of the command to overwrite.");
                                                                                                Console.WriteLine("");
                                                                                                Console.WriteLine("Example: mv -o");
                                                                                                Console.WriteLine("");
                                                                                        } else {
                                                                                                try {
                                                                                                        File.Move(firstloc, secondloc, false);
                                                                                                } catch {
                                                                                                        Console.WriteLine("");
                                                                                                        Console.WriteLine("Error, file not found!");
                                                                                                        Console.WriteLine("");
                                                                                                }
                                                                                        }
                                                                                }
                                                                        }

                                                                        if (input == "dir" || input == "ls" || input == "directory") {
                                                                                validcommandfound = true;
                                                                                try {
                                                                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                                                                        string[] dirs = Directory.GetDirectories(Isettingsconf.Currentdir);
                                                                                        //Console.WriteLine("Directories:");
                                                                                        var newArray = dirs.OrderBy(x => x.ToLower()).ToArray();
                                                                                        foreach(var newarray in newArray) {
                                                                                                Console.WriteLine(newarray + "");
                                                                                        }

                                                                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                                                                        string[] allfiles = Directory.GetFiles(Isettingsconf.Currentdir, "*.*", SearchOption.TopDirectoryOnly);
                                                                                        //Console.WriteLine("Files:");
                                                                                        foreach(var file in allfiles) {
                                                                                                FileInfo info = new FileInfo(file);
                                                                                                if (info.Extension == ".kookaburra" || info.Extension == ".kbproject") {
                                                                                                        Console.ForegroundColor = ConsoleColor.Magenta;
                                                                                                } else {
                                                                                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                                                                                }
                                                                                                Console.WriteLine(info);
                                                                                        }
                                                                                        Console.ForegroundColor = ConsoleColor.White;
                                                                                } catch {
                                                                                        /*Console.WriteLine("Error, no directory found.");*/ }
                                                                        }

                                                                        if (input == "cd ..") {
                                                                                validcommandfound = true;
                                                                                if (Isettingsconf.Currentdir.EndsWith("/") || Isettingsconf.Currentdir.EndsWith(@ "\")) { }
                                                                                                else {
                                                                                                        Isettingsconf.Currentdir = Isettingsconf.Currentdir + "/";
                                                                                                }

                                                                                                try {
                                                                                                        DirectoryInfo pd = Directory.GetParent(Isettingsconf.Currentdir);
                                                                                                        if (pd == null) {
                                                                                                                Isettingsconf.Currentdir = "";
                                                                                                        } else {
                                                                                                                string res = pd.Parent.FullName;
                                                                                                                Isettingsconf.Currentdir = res;
                                                                                                        }
                                                                                                        /*if (pd == null) { }
                                                                                                        string res = pd.Parent.FullName;
                                                                                                        Isettingsconf.Currentdir = res;*/
                                                                                                } catch {}
                                                                                        }

                                                                                        if (input == "ipconfig") {
                                                                                                validcommandfound = true;
                                                                                                NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
                                                                                                foreach(NetworkInterface adapter in adapters) {
                                                                                                        IPInterfaceProperties properties = adapter.GetIPProperties();
                                                                                                        Console.WriteLine(adapter.Description);
                                                                                                        Console.WriteLine("  DNS suffix .............................. : {0}",
                                                                                                                properties.DnsSuffix);
                                                                                                        Console.WriteLine("  DNS enabled ............................. : {0}",
                                                                                                                properties.IsDnsEnabled);
                                                                                                        Console.WriteLine("  Dynamically configured DNS .............. : {0}",
                                                                                                                properties.IsDynamicDnsEnabled);
                                                                                                }
                                                                                        }

                                                                                        if (input.StartsWith("start")) {
                                                                                                validcommandfound = true;
                                                                                                string loc = input.Replace("start ", "");
                                                                                                if (loc == "cmd.exe" || loc == "cmd") {
                                                                                                        Console.WriteLine("");
                                                                                                        Console.WriteLine("Error, CMD not supported.");
                                                                                                        Console.WriteLine("");
                                                                                                } else if (loc.EndsWith(".kookaburra")) {

                                                                                                } else {
                                                                                                        try {
                                                                                                                ProcessStartInfo startInfo = new ProcessStartInfo();
                                                                                                                startInfo.FileName = loc;
                                                                                                                startInfo.Arguments = "";
                                                                                                                Process.Start(startInfo);
                                                                                                        } catch {
                                                                                                                Console.WriteLine("");
                                                                                                                Console.WriteLine("Error, file not found!");
                                                                                                                Console.WriteLine("");
                                                                                                        }
                                                                                                }
                                                                                        }

                                                                                        if (input.StartsWith(".")) {
                                                                                                if (input == "." || input == "./" || input.StartsWith("..")) {

                                                                                                } else {
                                                                                                        validcommandfound = true;
                                                                                                        try {
                                                                                                                // fix
                                                                                                                string loc = input.TrimStart('.');
                                                                                                                ProcessStartInfo startInfo = new ProcessStartInfo();
                                                                                                                //Console.WriteLine(Process.GetCurrentProcess().MainModule.FileName);
                                                                                                                startInfo.FileName = Process.GetCurrentProcess().MainModule.FileName;
                                                                                                                startInfo.Arguments = loc;
                                                                                                                Process.Start(startInfo);
                                                                                                        } catch {}
                                                                                                }
                                                                                        }

                                                                                        if (input.StartsWith("sound.play ")) {
                                                                                                validcommandfound = true;
                                                                                                string res = input.Replace("sound.play", "");
                                                                                                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                                                                                                        Process.Start(@ "powershell", $ @ "-c (New-Object Media.SoundPlayer '{res}').PlaySync();");
                                                                                                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
                                                                                                        try {
                                                                                                                string program = "cvlc";
                                                                                                                var pi = new ProcessStartInfo(res) {
                                                                                                                        Arguments = Path.GetFileName(res) + " --play-and-exit",
                                                                                                                                UseShellExecute = true,
                                                                                                                                WorkingDirectory = Path.GetDirectoryName(res),
                                                                                                                                FileName = program,
                                                                                                                                Verb = "OPEN",
                                                                                                                                WindowStyle = ProcessWindowStyle.Hidden
                                                                                                                };
                                                                                                                Process p = new Process();
                                                                                                                p.StartInfo = pi;
                                                                                                                p.Start();
                                                                                                                p.WaitForExit();
                                                                                                        } catch {
                                                                                                                Console.WriteLine("Please install VLC Media Player to work.");
                                                                                                        }
                                                                                                }
                                                                                        }

                                                                                        if (input.StartsWith("download ")) {
                                                                                                validcommandfound = true;
                                                                                                try {
                                                                                                        string loc1 = input.Replace("download ", "");
                                                                                                        string[] loc2 = loc1.Split('"');
                                                                                                        string internetloc = loc2[1];
                                                                                                        string localloc = loc2[3];
                                                                                                        try {
                                                                                                                Thread.Sleep(1249);
                                                                                                                WebClient webClient = new WebClient();
                                                                                                                webClient.DownloadFile(internetloc, localloc);
                                                                                                                Thread.Sleep(1432);
                                                                                                                Console.WriteLine("");
                                                                                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                                                                                Console.WriteLine("File successfully downloaded!");
                                                                                                                Console.ForegroundColor = ConsoleColor.White;
                                                                                                                Console.WriteLine("");
                                                                                                        } catch {
                                                                                                                Console.WriteLine("Error downloading file!");
                                                                                                        }
                                                                                                } catch {
                                                                                                        Console.WriteLine("Error!");
                                                                                                }
                                                                                        }

                                                                                        if (input == "tip") {
                                                                                                validcommandfound = true;
                                                                                                var table = new Table();
                                                                                                table.AddColumn(new TableColumn("Tip").Centered());
                                                                                                Random random = new Random();
                                                                                                int rand = random.Next(1, 10);

                                                                                                //New tips coming soon..
                                                                                                switch (rand) {
                                                                                                case 1:
                                                                                                        table.AddRow("[green bold]You can customzie the startup location by editing the startup_loc.txt file.[/]");
                                                                                                        break;
                                                                                                case 2:
                                                                                                        table.AddRow("[green bold]Deleting KookaburraShell.exe and the folder {0} will completely delete Kookaburra.[/]", Isettingsconf.Envloc);
                                                                                                        break;
                                                                                                case 3:
                                                                                                        table.AddRow("[green bold]You can customzie the startup location by editing the startup_loc.txt file[/]");
                                                                                                        break;
                                                                                                case 4:
                                                                                                        table.AddRow("[green bold]You can customzie the startup location by editing the startup_loc.txt file[/]");
                                                                                                        break;
                                                                                                case 5:
                                                                                                        table.AddRow("[green bold]You can customzie the startup location by editing the startup_loc.txt file[/]");
                                                                                                        break;
                                                                                                case 6:
                                                                                                        table.AddRow("[green bold]You can customzie the startup location by editing the startup_loc.txt file[/]");
                                                                                                        break;
                                                                                                case 7:
                                                                                                        table.AddRow("[green bold]You can customzie the startup location by editing the startup_loc.txt file[/]");
                                                                                                        break;
                                                                                                case 8:
                                                                                                        table.AddRow("[green bold]You can customzie the startup location by editing the startup_loc.txt file[/]");
                                                                                                        break;
                                                                                                case 9:
                                                                                                        table.AddRow("[green bold]You can customzie the startup location by editing the startup_loc.txt file[/]");
                                                                                                        break;
                                                                                                case 10:
                                                                                                        table.AddRow("[green bold]You can customzie the startup location by editing the startup_loc.txt file[/]");
                                                                                                        break;
                                                                                                }

                                                                                                table.Border(TableBorder.Heavy);
                                                                                                AnsiConsole.Render(table);
                                                                                        }

                                                                                        if (input == "whoami") {
                                                                                                validcommandfound = true;
                                                                                                Console.WriteLine(Environment.MachineName + @ "/" + Environment.UserName);
                                                                                        }

                                                                                        if (input.StartsWith("edit")) {
                                                                                                validcommandfound = true;
                                                                                                string file = input.Replace("edit ", "");
                                                                                                string loc = Isettingsconf.Envloc + @ "/text_editor.txt";
                                                                                                try {
                                                                                                        string result = File.ReadAllText(loc);
                                                                                                        string[] valuearray = result.Split("|");
                                                                                                        string result3;
                                                                                                        string[] result2 = valuearray[1].Split("%arg%");
                                                                                                        if (valuearray[1] == "%arg%") {
                                                                                                                result3 = /*result2[0] + */ file /* + result2[1]*/ ;
                                                                                                        } else {
                                                                                                                result3 = result2[0] + file + result2[1];
                                                                                                        }
                                                                                                        ProcessStartInfo startInfo = new ProcessStartInfo();
                                                                                                        startInfo.FileName = valuearray[0];
                                                                                                        startInfo.Arguments = result3.Split(new [] {
                                                                                                                '\r',
                                                                                                                '\n'
                                                                                                        }).FirstOrDefault();
                                                                                                        Process.Start(startInfo);
                                                                                                } catch {
                                                                                                        Console.WriteLine("Error.");
                                                                                                }
                                                                                        }

                                                                                        //Easter egg....
                                                                                        /*if (input == "matrix")
                                                                                        {
                                                                                            validcommandfound = true;
                                                                                            Console.Clear();
                                                                                            Console.CursorVisible = false;
                                                                                            Console.ForegroundColor = ConsoleColor.Green;
                                                                                            int Color = 0;
                                                                                            Random rand = new Random();
                                                                                            Thread t = new Thread(SpeedTyper);
                                                                                            t.Start();
                                                                                            Thread t2 = new Thread(SpeedClear);
                                                                                            t2.Start();
                                                                                            while (true) 
                                                                                            {
                                                                                                switch (Color)
                                                                                                {
                                                                                                    case 1:
                                                                                                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                                                                                                        break;
                                                                                                    case 2:
                                                                                                        Console.ForegroundColor = ConsoleColor.Blue;
                                                                                                        break;
                                                                                                    default:
                                                                                                        Console.ForegroundColor = ConsoleColor.Green;
                                                                                                        break;
                                                                                                }
                                                                                                Console.Write(rand.Next(0, 9999)); 
                                                                                            }

                                                                                            void SpeedTyper() 
                                                                                            {
                                                                                                while (true)
                                                                                                {
                                                                                                    int changed = Color - 1;
                                                                                                    switch (Color)
                                                                                                    {
                                                                                                        case 1:
                                                                                                            Console.ForegroundColor = ConsoleColor.Gray;
                                                                                                            break;
                                                                                                        case 2:
                                                                                                            Console.ForegroundColor = ConsoleColor.Magenta;
                                                                                                            break;
                                                                                                        default:
                                                                                                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                                                                                                            break;
                                                                                                    }
                                                                                                    Console.Write(rand.Next(0, 9999));
                                                                                                }
                                                                                            }

                                                                                            void SpeedClear()
                                                                                            {
                                                                                                while (true)
                                                                                                {
                                                                                                    Thread.Sleep(1000);
                                                                                                    Console.Clear();
                                                                                                    Color = 2;
                                                                                                    Thread.Sleep(1000);
                                                                                                    Console.Clear();
                                                                                                    Color = 1;
                                                                                                    Thread.Sleep(1000);
                                                                                                    Console.Clear();
                                                                                                    Color = 3;
                                                                                                }
                                                                                            }
                                                                                        }*/

                                                                                        if (input.StartsWith("wait")) {
                                                                                                validcommandfound = true;
                                                                                                string time = input.Replace("wait ", "");
                                                                                                int timeint = Int16.Parse(time);
                                                                                                Thread.Sleep(TimeSpan.FromSeconds(timeint));
                                                                                        }

                                                                                        if (input.StartsWith("app.title = ")) {
                                                                                                validcommandfound = true;
                                                                                                string name = input.Replace("app.title = ", "");
                                                                                                Console.Title = name;
                                                                                        }

                                                                                        if (input.StartsWith("app.size = ")) {
                                                                                                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                                                                                                        validcommandfound = true;
                                                                                                        string widthheight = input.Replace("app.size = ", "");
                                                                                                        string width;
                                                                                                        string height;
                                                                                                        widthheight.Replace(" ", "/n");
                                                                                                        string[] valuearray = widthheight.Split(' ');
                                                                                                        width = valuearray[0];
                                                                                                        height = valuearray[1];
                                                                                                        int widthint = Int16.Parse(width);
                                                                                                        int heightint = Int16.Parse(height);
                                                                                                        Console.SetWindowSize(widthint, heightint);
                                                                                                }
                                                                                        }

                                                                                        if (input == "app.beep()") {
                                                                                                validcommandfound = true;
                                                                                                Console.Beep();
                                                                                        }

                                                                                        if (input == "app.reset()") {
                                                                                                validcommandfound = true;
                                                                                                Console.ResetColor();
                                                                                        }

                                                                                        if (input == "pwd") {
                                                                                                validcommandfound = true;
                                                                                                if (Isettingsconf.Currentdir == "") {} else {
                                                                                                        Console.ForegroundColor = ConsoleColor.White;
                                                                                                        Console.WriteLine(Isettingsconf.Currentdir);
                                                                                                }
                                                                                        }

                                                                                        if (input.StartsWith("rand")) {
                                                                                                validcommandfound = true;
                                                                                                try {
                                                                                                        string value = input.Replace("rand ", "");
                                                                                                        value.Replace(" ", "/n");
                                                                                                        string[] valuearray = value.Split(' ');
                                                                                                        string firstchars = valuearray[0];
                                                                                                        string secondchars = valuearray[1];
                                                                                                        try {
                                                                                                                int first = Int16.Parse(firstchars);
                                                                                                                int second = Int16.Parse(secondchars);
                                                                                                                Random rd = new Random();
                                                                                                                int result = rd.Next(first, second);
                                                                                                                Console.ForegroundColor = ConsoleColor.Blue;
                                                                                                                Console.WriteLine(result);
                                                                                                                Console.ForegroundColor = ConsoleColor.White;
                                                                                                        } catch {
                                                                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                                                                Console.WriteLine("Input value too great.");
                                                                                                        }
                                                                                                } catch {}
                                                                                        }

                                                                                        if (input == custom_commands.name1) {
                                                                                                validcommandfound = true;
                                                                                                try {
                                                                                                        System.Diagnostics.Process.Start(custom_commands.loc1);
                                                                                                } catch {}
                                                                                        }

                                                                                        if (input == custom_commands.name2) {
                                                                                                validcommandfound = true;
                                                                                                try {
                                                                                                        System.Diagnostics.Process.Start(custom_commands.loc2);
                                                                                                } catch {}
                                                                                        }

                                                                                        if (input == custom_commands.name3) {
                                                                                                validcommandfound = true;
                                                                                                try {
                                                                                                        System.Diagnostics.Process.Start(custom_commands.loc3);
                                                                                                } catch {}
                                                                                        }

                                                                                        if (input == custom_commands.name4) {
                                                                                                validcommandfound = true;
                                                                                                try {
                                                                                                        System.Diagnostics.Process.Start(custom_commands.loc4);
                                                                                                } catch {}
                                                                                        }

                                                                                        if (input == custom_commands.name5) {
                                                                                                validcommandfound = true;
                                                                                                try {
                                                                                                        System.Diagnostics.Process.Start(custom_commands.loc5);
                                                                                                } catch {}
                                                                                        }

                                                                                        if (input == custom_commands.name6) {
                                                                                                validcommandfound = true;
                                                                                                try {
                                                                                                        System.Diagnostics.Process.Start(custom_commands.loc6);
                                                                                                } catch {}
                                                                                        }

                                                                                        if (input == custom_commands.name7) {
                                                                                                validcommandfound = true;
                                                                                                try {
                                                                                                        System.Diagnostics.Process.Start(custom_commands.loc7);
                                                                                                } catch {}
                                                                                        }

                                                                                        if (input == custom_commands.name8) {
                                                                                                validcommandfound = true;
                                                                                                try {
                                                                                                        System.Diagnostics.Process.Start(custom_commands.loc8);
                                                                                                } catch {}
                                                                                        }

                                                                                        if (input == "tree" && !string.IsNullOrEmpty(Isettingsconf.Currentdir) || input == "list" && !string.IsNullOrEmpty(Isettingsconf.Currentdir)) {
                                                                                                validcommandfound = true;
                                                                                                string basedir = Isettingsconf.Currentdir;

                                                                                                var root = new Tree("[yellow]" + basedir + "[/]");

                                                                                                //AddNode(basedir);
                                                                                                //bool firsttime

                                                                                                try {

                                                                                                        string[] FilePath = Directory.GetFiles(basedir);
                                                                                                        string[] SubPath = Directory.GetDirectories(basedir);

                                                                                                        foreach(string SubDir in SubPath) {
                                                                                                                var node = root.AddNode("[yellow]" + SubDir + "[/]");
                                                                                                                string[] fp = Directory.GetFiles(SubDir);
                                                                                                                foreach(string sub in fp) node.AddNode("[blue]" + sub + "[/]");
                                                                                                                string[] SubPath2 = Directory.GetDirectories(SubDir);
                                                                                                                foreach(string SubDir2 in SubPath2) {
                                                                                                                        var node1 = node.AddNode("[yellow]" + SubDir2 + "[/]");
                                                                                                                        string[] fp1 = Directory.GetFiles(SubDir2);
                                                                                                                        foreach(string sub2 in fp1) node1.AddNode("[blue]" + sub2 + "[/]");
                                                                                                                        string[] SubPath3 = Directory.GetDirectories(SubDir2);
                                                                                                                        foreach(string SubDir3 in SubPath3) {
                                                                                                                                //node.AddNode(subdirectory2);
                                                                                                                                var node2 = node.AddNode("[yellow]" + SubDir3 + "[/]");
                                                                                                                                string[] fp2 = Directory.GetFiles(SubDir3);
                                                                                                                                foreach(string sub2 in fp2) node1.AddNode("[blue]" + sub2 + "[/]");
                                                                                                                                //string[] SubPath3 = Directory.GetDirectories(SubDir2);
                                                                                                                        }
                                                                                                                }
                                                                                                        }

                                                                                                        AnsiConsole.Render(root);
                                                                                                } catch (Exception e) {
                                                                                                        Console.ForegroundColor = ConsoleColor.Red;
                                                                                                        Console.WriteLine(e.ToString());
                                                                                                }
                                                                                        }

                                                                                        if (!validcommandfound) {
                                                                                                if (input == "") {} else if (string.IsNullOrWhiteSpace(input)) {} else {
                                                                                                        if (input == "abaut" || input == "aboutr" || input == "abourt" || input == "aboutf" || input == "abouft" || input == "aboutg" || input == "abougt" || input == "abouty") {
                                                                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                                                                Console.WriteLine("Command '{0}' not found. Here are similar commands.", input);
                                                                                                                Console.WriteLine("");
                                                                                                                Console.ForegroundColor = ConsoleColor.White;
                                                                                                                AnsiConsole.MarkupLine("[rapidblink yellow bold]Command 'about'[/]");
                                                                                                                Console.WriteLine("");
                                                                                                        }
                                                                                                        if (input == "delete" || input == "del" || input == "remove") {
                                                                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                                                                Console.WriteLine("Command '{0}' not found. Here are similar commands.", input);
                                                                                                                Console.WriteLine("");
                                                                                                                Console.ForegroundColor = ConsoleColor.White;
                                                                                                                AnsiConsole.MarkupLine("[rapidblink yellow bold]Command 'rm'[/]");
                                                                                                                Console.WriteLine("");
                                                                                                        } else if (input == "elp" || input == "hlp" || input == "hep" || input == "hepl" || input == "hhelp" || input == "heelp" || input == "hellp" || input == "helpp") {
                                                                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                                                                Console.WriteLine("Command '{0}' not found. Here are similar commands.", input);
                                                                                                                Console.WriteLine("");
                                                                                                                Console.ForegroundColor = ConsoleColor.White;
                                                                                                                AnsiConsole.MarkupLine("[rapidblink yellow bold]Command 'help'[/]");
                                                                                                                Console.WriteLine("");
                                                                                                        } else if (input == "ip" || input == "zp" || input == "zi" || input == "zzip" || input == "ziip" || input == "zipp" || input == "izp" || input == "zpi" || input == "aip" || input == "sip") {
                                                                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                                                                Console.WriteLine("Command '{0}' not found. Here are similar commands.", input);
                                                                                                                Console.WriteLine("");
                                                                                                                Console.ForegroundColor = ConsoleColor.White;
                                                                                                                AnsiConsole.MarkupLine("[rapidblink yellow bold]Command 'zip'[/]");
                                                                                                                AnsiConsole.MarkupLine("[yellow strikethrough]Command 'unzip'[/]");
                                                                                                                Console.WriteLine("");
                                                                                                        } else if (input == "-env") {
                                                                                                                if (Directory.Exists(Isettingsconf.Envloc))
                                                                                                                        Isettingsconf.Currentdir = Isettingsconf.Envloc;
                                                                                                        } else if (input == "-dt") {
                                                                                                                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)))
                                                                                                                        Isettingsconf.Currentdir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                                                                                                        } else if (input == "-st") {
                                                                                                                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Startup)))
                                                                                                                        Isettingsconf.Currentdir = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
                                                                                                        } else if (input == "-exe" && RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                                                                                                                Isettingsconf.Currentdir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                                                                                                        } else if (input == "-r") {
                                                                                                                if (Directory.Exists(Directory.GetDirectoryRoot(Isettingsconf.Currentdir)))
                                                                                                                        Isettingsconf.Currentdir = Directory.GetDirectoryRoot(Isettingsconf.Currentdir);
                                                                                                        } else if (input == "-c") {
                                                                                                                Isettingsconf.Currentdir = "";
                                                                                                        } else if (input == "-o") {
                                                                                                                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                                                                                                                        validcommandfound = true;
                                                                                                                        if (Isettingsconf.Currentdir.Contains(":")) {
                                                                                                                                try {
                                                                                                                                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo() {
                                                                                                                                                FileName = Isettingsconf.Currentdir,
                                                                                                                                                        UseShellExecute = true,
                                                                                                                                                        Verb = "open"
                                                                                                                                        });
                                                                                                                                } catch {
                                                                                                                                        //Console.WriteLine("error");
                                                                                                                                }
                                                                                                                        } else {
                                                                                                                                //open explorer
                                                                                                                                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo() {
                                                                                                                                        FileName = "explorer.exe",
                                                                                                                                                UseShellExecute = true,
                                                                                                                                                Verb = "open"
                                                                                                                                });
                                                                                                                        }
                                                                                                                }
                                                                                                        } else if (input.StartsWith("-")) {
                                                                                                                string Replace = input.Replace("-", "");
                                                                                                                if (Replace.Length == 3) {
                                                                                                                        //Assumes that its a drive label.
                                                                                                                        if (Directory.Exists(Replace)) {
                                                                                                                                Isettingsconf.Currentdir = Replace;
                                                                                                                        } else {
                                                                                                                                Console.WriteLine("");
                                                                                                                                Console.WriteLine("Command '{0}' not found.", input);
                                                                                                                                Console.WriteLine("");
                                                                                                                        }
                                                                                                                } else {
                                                                                                                        try {
                                                                                                                                //Reverts back to original if the location doesn't exist.
                                                                                                                                string Temp = Isettingsconf.Currentdir;
                                                                                                                                string ReplaceFinal = char.ToUpper(Replace[0]) + Replace.Substring(1);
                                                                                                                                Isettingsconf.Currentdir = Environment.GetFolderPath((Environment.SpecialFolder) Enum.Parse(typeof (Environment.SpecialFolder), ReplaceFinal));
                                                                                                                                //If the directory doesn't exist revert back to 'Temp'.
                                                                                                                                if (!Directory.Exists(Isettingsconf.Currentdir))
                                                                                                                                        Isettingsconf.Currentdir = Temp;
                                                                                                                        } catch {
                                                                                                                                Console.WriteLine("");
                                                                                                                                Console.WriteLine("Command '{0}' not found.", input);
                                                                                                                                Console.WriteLine("");
                                                                                                                        }
                                                                                                                }
                                                                                                        } else {
                                                                                                                Console.WriteLine("");
                                                                                                                Console.WriteLine("Command '{0}' not found.", input);
                                                                                                                Console.WriteLine("");
                                                                                                        }
                                                                                                }
                                                                                        }
                                                                                }

                                                                                void packagecheck() {
                                                                                        bool Errormsg = false;
                                                                                        if (Directory.Exists(Isettingsconf.Envloc)) {} else {
                                                                                                try {
                                                                                                        Directory.CreateDirectory(Isettingsconf.Envloc);
                                                                                                } catch {
                                                                                                        Console.ForegroundColor = ConsoleColor.Red;
                                                                                                        Console.WriteLine("Restart with admin priviliges.");
                                                                                                }
                                                                                        }

                                                                                        string path4 = Isettingsconf.Envloc + @ "/conf.txt";
                                                                                        if (!File.Exists(path4)) {
                                                                                                try {
                                                                                                        using(StreamWriter sw = File.CreateText(path4)) {
                                                                                                                sw.WriteLine("# KookaburraShell - Conf.txt");
                                                                                                                sw.WriteLine("show_startup=true");
                                                                                                                sw.WriteLine("startup_location=default");
                                                                                                                //sw.WriteLine("startup_message=null");
                                                                                                                sw.WriteLine("FR=true");
                                                                                                                sw.WriteLine("force_rainbow=false");
                                                                                                        }
                                                                                                } catch {
                                                                                                        if (!Errormsg) {
                                                                                                                Console.ForegroundColor = ConsoleColor.White;
                                                                                                                Console.Write("[");
                                                                                                                Console.ForegroundColor = ConsoleColor.Blue;
                                                                                                                Console.Write("Note");
                                                                                                                Console.ForegroundColor = ConsoleColor.White;
                                                                                                                Console.Write("] ");
                                                                                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                                                                                Console.Write("To finish setting up Kookaburra, Please restart it as administrator.");
                                                                                                                Console.ForegroundColor = ConsoleColor.White;
                                                                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                                                                Console.WriteLine("Failed to Create/Write text_editor.txt");
                                                                                                                // make white-space between input and info
                                                                                                                Console.WriteLine("");
                                                                                                                Errormsg = true;
                                                                                                        }
                                                                                                }
                                                                                        } //Todo, add conf.txt

                                                                                        try {
                                                                                                Console.ForegroundColor = ConsoleColor.White;
                                                                                                string[] vs = File.ReadAllLines(path4);
                                                                                                foreach(string Lines in vs) {
                                                                                                        if (Lines.StartsWith("#")) {} else if (Lines.StartsWith("show_startup=")) {
                                                                                                                string result = Lines.Replace("show_startup=", "");
                                                                                                                if (result == "true") {
                                                                                                                        /*TODO*/ } else if (result == "false") {
                                                                                                                        /*TODO*/ } else {
                                                                                                                        Console.WriteLine("error");
                                                                                                                }
                                                                                                        } else if (Lines.StartsWith("force_rainbow=")) {
                                                                                                                string result = Lines.Replace("force_rainbow=", "");
                                                                                                                if (result == "true") {
                                                                                                                        Isettingsconf.ShowRainbow = true;
                                                                                                                } else if (result == "false") {
                                                                                                                        /*Default function*/ }
                                                                                                        } else if (Lines.StartsWith("startup_location=")) {
                                                                                                                string result = Lines.Replace("startup_location=", "");
                                                                                                                if (File.Exists(result)) {
                                                                                                                        Isettingsconf.Envloc = result;
                                                                                                                } else if (result == "default") {} else {
                                                                                                                        Console.WriteLine("error");
                                                                                                                }
                                                                                                        } else if (Lines.StartsWith("FR=")) {
                                                                                                                string result = Lines.Replace("FR=", "");
                                                                                                                if (result == "true") {
                                                                                                                        string text = File.ReadAllText(path4);
                                                                                                                        text = text.Replace("FR=true", "FR=false");
                                                                                                                        File.WriteAllText(path4, text);
                                                                                                                        Isettingsconf.FR = true;
                                                                                                                } else {
                                                                                                                        Isettingsconf.FR = false;
                                                                                                                }
                                                                                                        } else {
                                                                                                                Console.WriteLine("error");
                                                                                                        }
                                                                                                }
                                                                                        } catch {
                                                                                                if (!Errormsg) {
                                                                                                        Console.ForegroundColor = ConsoleColor.White;
                                                                                                        Console.Write("[");
                                                                                                        Console.ForegroundColor = ConsoleColor.Blue;
                                                                                                        Console.Write("Note");
                                                                                                        Console.ForegroundColor = ConsoleColor.White;
                                                                                                        Console.Write("] ");
                                                                                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                                                                                        Console.Write("To finish setting up Kookaburra, Please restart it as administrator.");
                                                                                                        Console.ForegroundColor = ConsoleColor.White;
                                                                                                        Console.ForegroundColor = ConsoleColor.Red;
                                                                                                        Console.WriteLine("Failed to Create/Write text_editor.txt");
                                                                                                        // make white-space between input and info
                                                                                                        Console.WriteLine("");
                                                                                                        Errormsg = true;
                                                                                                }
                                                                                        }

                                                                                        string path2 = Isettingsconf.Envloc + @ "/text_editor.txt";
                                                                                        if (!File.Exists(path2)) {
                                                                                                try {
                                                                                                        using(StreamWriter sw = File.CreateText(path2)) {
                                                                                                                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                                                                                                                        sw.WriteLine("notepad.exe|%arg%");
                                                                                                                }
                                                                                                                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
                                                                                                                        sw.WriteLine("nano|%arg%");
                                                                                                                }
                                                                                                        }
                                                                                                } catch {
                                                                                                        if (!Errormsg) {
                                                                                                                Console.ForegroundColor = ConsoleColor.White;
                                                                                                                Console.Write("[");
                                                                                                                Console.ForegroundColor = ConsoleColor.Blue;
                                                                                                                Console.Write("Note");
                                                                                                                Console.ForegroundColor = ConsoleColor.White;
                                                                                                                Console.Write("] ");
                                                                                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                                                                                Console.Write("To finish setting up Kookaburra, Please restart it as administrator.");
                                                                                                                Console.ForegroundColor = ConsoleColor.White;
                                                                                                                /*Console.ForegroundColor = ConsoleColor.Red;
                                                                                                                Console.WriteLine("Failed to Create/Write text_editor.txt");*/
                                                                                                                // make white-space between input and info
                                                                                                                Console.WriteLine("");
                                                                                                                Errormsg = true;
                                                                                                        }
                                                                                                }
                                                                                        }

                                                                                        string path = Isettingsconf.Envloc + @ "/custom_commands.txt";
                                                                                        if (!File.Exists(path)) {
                                                                                                try {
                                                                                                        using(StreamWriter sw = File.CreateText(path)) {
                                                                                                                sw.WriteLine("# KookaburraShell - Custom Commands 2.0");
                                                                                                                sw.WriteLine("#");
                                                                                                                sw.WriteLine("# example:");
                                                                                                                sw.WriteLine("# testcommand=C:/exampledir/test.exe");
                                                                                                                sw.WriteLine("#");
                                                                                                                sw.WriteLine("#");
                                                                                                                sw.WriteLine("# Lines with '#' infront of them are comments. You can remove them. (Max 10 lines)");
                                                                                                        }
                                                                                                } catch {
                                                                                                        if (!Errormsg) {
                                                                                                                Console.ForegroundColor = ConsoleColor.White;
                                                                                                                Console.Write("[");
                                                                                                                Console.ForegroundColor = ConsoleColor.Blue;
                                                                                                                Console.Write("Note");
                                                                                                                Console.ForegroundColor = ConsoleColor.White;
                                                                                                                Console.Write("] ");
                                                                                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                                                                                Console.Write("To finish setting up Kookaburra, Please restart it as administrator.");
                                                                                                                Console.ForegroundColor = ConsoleColor.White;
                                                                                                                /*Console.ForegroundColor = ConsoleColor.Red;
                                                                                                                Console.WriteLine("Failed to Create/Write text_editor.txt");*/
                                                                                                                // make white-space between input and info
                                                                                                                Console.WriteLine("");
                                                                                                                Errormsg = true;
                                                                                                        }
                                                                                                }
                                                                                        } else {
                                                                                                try {
                                                                                                        int filelines = 0;
                                                                                                        int num = 0;
                                                                                                        int iterations = 1;
                                                                                                        //int locnum = 1;
                                                                                                        //int namenum = 1;
                                                                                                        string[] l = System.IO.File.ReadAllLines(path);

                                                                                                        foreach(string line in l) {
                                                                                                                if (!l[filelines].StartsWith("#") && l[filelines].Contains("=")) {
                                                                                                                        string[] results = l[filelines].Split('=');
                                                                                                                        //Console.WriteLine(results[0] +"->" + results[1]);
                                                                                                                        num++;
                                                                                                                        num = custom_commands.num;
                                                                                                                        //Console.WriteLine(iterations);
                                                                                                                        switch (iterations) {
                                                                                                                        case 1:
                                                                                                                                custom_commands.name1 = results[0];
                                                                                                                                custom_commands.loc1 = results[1];
                                                                                                                                break;
                                                                                                                        case 2:
                                                                                                                                custom_commands.name2 = results[0];
                                                                                                                                custom_commands.loc2 = results[1];
                                                                                                                                break;
                                                                                                                        case 3:
                                                                                                                                custom_commands.name3 = results[0];
                                                                                                                                custom_commands.loc3 = results[1];
                                                                                                                                break;
                                                                                                                        case 4:
                                                                                                                                custom_commands.name4 = results[0];
                                                                                                                                custom_commands.loc4 = results[1];
                                                                                                                                break;
                                                                                                                        case 5:
                                                                                                                                custom_commands.name5 = results[0];
                                                                                                                                custom_commands.loc5 = results[1];
                                                                                                                                break;
                                                                                                                        case 6:
                                                                                                                                custom_commands.name6 = results[0];
                                                                                                                                custom_commands.loc6 = results[1];
                                                                                                                                break;
                                                                                                                        case 7:
                                                                                                                                custom_commands.name7 = results[0];
                                                                                                                                custom_commands.loc7 = results[1];
                                                                                                                                break;
                                                                                                                        case 8:
                                                                                                                                custom_commands.name8 = results[0];
                                                                                                                                custom_commands.loc8 = results[1];
                                                                                                                                break;
                                                                                                                        default:
                                                                                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                                                                                Console.WriteLine("Limit reached, remove other commands to add more.");
                                                                                                                                break;
                                                                                                                        }
                                                                                                                        iterations++;
                                                                                                                }
                                                                                                                filelines++;
                                                                                                        }
                                                                                                } catch {
                                                                                                        if (!Errormsg) {
                                                                                                                Console.ForegroundColor = ConsoleColor.White;
                                                                                                                Console.Write("[");
                                                                                                                Console.ForegroundColor = ConsoleColor.Blue;
                                                                                                                Console.Write("Note");
                                                                                                                Console.ForegroundColor = ConsoleColor.White;
                                                                                                                Console.Write("] ");
                                                                                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                                                                                Console.Write("To finish setting up Kookaburra, Please restart it as administrator.");
                                                                                                                Console.ForegroundColor = ConsoleColor.White;
                                                                                                                /*Console.ForegroundColor = ConsoleColor.Red;
                                                                                                                Console.WriteLine("Failed to Create/Write text_editor.txt");*/
                                                                                                                // make white-space between input and info
                                                                                                                Console.WriteLine("");
                                                                                                                Errormsg = true;
                                                                                                        }

                                                                                                        //Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Failed to Read custom_commands.txt");
                                                                                                }
                                                                                        }

                                                                                        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                                                                                                Isettingsconf.SavedHeight = Console.WindowHeight;
                                                                                                Isettingsconf.SavedWidth = Console.WindowWidth;
                                                                                        }
                                                                                }

                                                                                void fileoutput() {
                                                                                        if (args.Length == 1) {
                                                                                                FileInfo file = new FileInfo(args[0]);
                                                                                                int line = 1;
                                                                                                Console.ForegroundColor = ConsoleColor.White;
                                                                                                var Intname = new [] {
                                                                                                        ""
                                                                                                };
                                                                                                List < string > Stringname = new List < string > ();
                                                                                                List < string > Stringvalue = new List < string > ();
                                                                                                BarChart bc = new BarChart();
                                                                                                Table tb = new Table();
                                                                                                bool GridSelect = false;
                                                                                                string debugoff = File.ReadLines(file.ToString()).First();
                                                                                                if (debugoff == "app.debug-off") {} else {
                                                                                                        Debuger();
                                                                                                }
                                                                                                Isettingsconf.Quietmode = true;
                                                                                                if (file.Extension == ".kookaburra") {
                                                                                                        Console.Title = "(" + file + ") Kookaburra Shell";
                                                                                                        if (file.Exists) {
                                                                                                                try {
                                                                                                                        string[] readText = File.ReadAllLines(file.ToString());
                                                                                                                        foreach(string s in readText) {
                                                                                                                                if (s.StartsWith("start")) {
                                                                                                                                        string loc = s.Replace("start ", "");
                                                                                                                                        //Console.WriteLine(loc);
                                                                                                                                        ProcessStartInfo startInfo = new ProcessStartInfo();
                                                                                                                                        startInfo.FileName = Format(loc);
                                                                                                                                        startInfo.Arguments = "";
                                                                                                                                        Process.Start(startInfo);
                                                                                                                                } else if (s.StartsWith("exit")) {
                                                                                                                                        Environment.Exit(0);
                                                                                                                                        script_var.Ifcount = 1;
                                                                                                                                } else if (s.StartsWith("wait")) {
                                                                                                                                        Wait(s);
                                                                                                                                        script_var.Ifcount = 1;
                                                                                                                                } else if (s.StartsWith("app.title = ")) {
                                                                                                                                        Apptitle(s);
                                                                                                                                        script_var.Ifcount = 1;
                                                                                                                                } else if (s == "app.clear") {
                                                                                                                                        Appclear();
                                                                                                                                        script_var.Ifcount = 1;
                                                                                                                                } else if (s.StartsWith("app.size = ")) {
                                                                                                                                        Appsize(s);
                                                                                                                                        script_var.Ifcount = 1;
                                                                                                                                } else if (s.StartsWith("import ")) {
                                                                                                                                        string name = s.Replace("import ", "");
                                                                                                                                        if (name == "FileIO") {
                                                                                                                                                Packages.FileIO = true;
                                                                                                                                        } else if (name == "Net") {
                                                                                                                                                Packages.Net = true;
                                                                                                                                        } else {
                                                                                                                                                throw new ArgumentException("Synax error at line '" + line + "'." + " C3211");
                                                                                                                                        }
                                                                                                                                } else if (s.StartsWith("print")) {
                                                                                                                                        Print(s);
                                                                                                                                        script_var.Ifcount = 1;
                                                                                                                                } else if (s.StartsWith("new filewriter(") && s.EndsWith(")")) {
                                                                                                                                        if (Packages.FileIO) {
                                                                                                                                                script_var.Ifcount = 1;
                                                                                                                                                string res1 = s.Replace("new filewriter", "");
                                                                                                                                                string res2 = res1.TrimStart('(');
                                                                                                                                                string res3 = res2.TrimEnd(')');
                                                                                                                                                List < string > Result = new List < string > ();
                                                                                                                                                try {
                                                                                                                                                        string res4 = res3.Replace(", ", "/n");
                                                                                                                                                        string[] valuearray = res4.Split("/n");
                                                                                                                                                        if (valuearray[0].StartsWith('"') && valuearray[0].EndsWith('"')) {
                                                                                                                                                                //loc
                                                                                                                                                                string loc1 = valuearray[0].TrimStart('"');
                                                                                                                                                                string loc2 = loc1.TrimEnd('"');
                                                                                                                                                                Result.Add(loc2);
                                                                                                                                                        } else {
                                                                                                                                                                int num = 0;
                                                                                                                                                                int num2 = 0;
                                                                                                                                                                foreach(string lines in Stringname) {
                                                                                                                                                                        num++;
                                                                                                                                                                        if (lines == valuearray[0]) {
                                                                                                                                                                                foreach(string lines2 in Stringvalue) {
                                                                                                                                                                                        num2++;
                                                                                                                                                                                        //Console.WriteLine("num=" + num + " num2=" + num2);
                                                                                                                                                                                        if (num == num2) {
                                                                                                                                                                                                Result.Add(lines2);
                                                                                                                                                                                        }
                                                                                                                                                                                }
                                                                                                                                                                                //Console.WriteLine("match");
                                                                                                                                                                        }
                                                                                                                                                                }
                                                                                                                                                        }

                                                                                                                                                        if (valuearray[1].StartsWith('"') && valuearray[1].EndsWith('"')) {
                                                                                                                                                                //value
                                                                                                                                                                string loc1 = valuearray[1].TrimStart('"');
                                                                                                                                                                string loc2 = loc1.TrimEnd('"');
                                                                                                                                                                Result.Add(loc2);
                                                                                                                                                        } else {
                                                                                                                                                                int num = 0;
                                                                                                                                                                int num2 = 0;
                                                                                                                                                                foreach(string lines in Stringname) {
                                                                                                                                                                        num++;
                                                                                                                                                                        if (lines == valuearray[1]) {
                                                                                                                                                                                foreach(string lines2 in Stringvalue) {
                                                                                                                                                                                        num2++;
                                                                                                                                                                                        //Console.WriteLine("num=" + num + " num2=" + num2);
                                                                                                                                                                                        if (num == num2) {
                                                                                                                                                                                                Result.Add(lines2);
                                                                                                                                                                                        }
                                                                                                                                                                                }
                                                                                                                                                                                //Console.WriteLine("match");
                                                                                                                                                                        }
                                                                                                                                                                }
                                                                                                                                                        }

                                                                                                                                                        Filewriter(Result[0], Result[1]);

                                                                                                                                                        void Filewriter(string path, string value) {
                                                                                                                                                                if (!File.Exists(path)) {
                                                                                                                                                                        // Create a file to write to.
                                                                                                                                                                        using(StreamWriter sw = File.CreateText(path)) {
                                                                                                                                                                                sw.WriteLine(value);
                                                                                                                                                                        }
                                                                                                                                                                } else {
                                                                                                                                                                        File.AppendAllText(path, Environment.NewLine + value);
                                                                                                                                                                }
                                                                                                                                                        }
                                                                                                                                                } catch {
                                                                                                                                                        throw new ArgumentException("Synax error at line '" + line + "'." + " C1793");
                                                                                                                                                }

                                                                                                                                        } else {
                                                                                                                                                throw new ArgumentException("Synax error at line '" + line + "'." + " C3965");
                                                                                                                                        }
                                                                                                                                } else if (s.StartsWith("new filereader(") && s.EndsWith("]") && s.Contains(")[")) {
                                                                                                                                        if (Packages.FileIO) {
                                                                                                                                                script_var.Ifcount = 1;
                                                                                                                                                string res1 = s.Replace("new filereader", "");
                                                                                                                                                string res2 = res1.TrimStart('(');
                                                                                                                                                string res3 = res2.TrimEnd(']');
                                                                                                                                                string[] valuearray = res3.Split(")[");

                                                                                                                                                string loc = "";
                                                                                                                                                if (valuearray[0].StartsWith('"') && valuearray[1].EndsWith('"')) {
                                                                                                                                                        string loc1 = valuearray[0].TrimStart('"');
                                                                                                                                                        string loc2 = loc1.TrimEnd('"');
                                                                                                                                                        loc = loc2;
                                                                                                                                                } else {
                                                                                                                                                        int num = 0;
                                                                                                                                                        int num2 = 0;
                                                                                                                                                        foreach(string lines in Stringname) {
                                                                                                                                                                num++;
                                                                                                                                                                if (lines == valuearray[0]) {
                                                                                                                                                                        foreach(string lines2 in Stringvalue) {
                                                                                                                                                                                num2++;
                                                                                                                                                                                //Console.WriteLine("num=" + num + " num2=" + num2);
                                                                                                                                                                                if (num == num2) {
                                                                                                                                                                                        loc = lines2;
                                                                                                                                                                                }
                                                                                                                                                                        }
                                                                                                                                                                        //Console.WriteLine("match");
                                                                                                                                                                }
                                                                                                                                                        }
                                                                                                                                                }

                                                                                                                                                string counter = "";
                                                                                                                                                if (valuearray[1].StartsWith('"') && valuearray[1].EndsWith('"')) {
                                                                                                                                                        string loc1 = valuearray[1].TrimStart('"');
                                                                                                                                                        string loc2 = loc1.TrimEnd('"');
                                                                                                                                                        counter = loc2;
                                                                                                                                                } else {
                                                                                                                                                        int num = 0;
                                                                                                                                                        int num2 = 0;
                                                                                                                                                        foreach(string lines in Stringname) {
                                                                                                                                                                num++;
                                                                                                                                                                if (lines == valuearray[1]) {
                                                                                                                                                                        foreach(string lines2 in Stringvalue) {
                                                                                                                                                                                num2++;
                                                                                                                                                                                //Console.WriteLine("num=" + num + " num2=" + num2);
                                                                                                                                                                                if (num == num2) {
                                                                                                                                                                                        counter = lines2;
                                                                                                                                                                                }
                                                                                                                                                                        }
                                                                                                                                                                        //Console.WriteLine("match");
                                                                                                                                                                }
                                                                                                                                                        }
                                                                                                                                                }

                                                                                                                                                int eachcount2 = Int32.Parse(counter);
                                                                                                                                                int eachcounter = 0;
                                                                                                                                                foreach(string lines in File.ReadLines(loc)) {
                                                                                                                                                        eachcounter++;
                                                                                                                                                        if (eachcounter == eachcount2) {
                                                                                                                                                                Console.WriteLine(lines);
                                                                                                                                                        }
                                                                                                                                                }

                                                                                                                                                /*int test = 0;
                                                                                                                                                foreach (string lines in valuearray) 
                                                                                                                                                {
                                                                                                                                                    test++;
                                                                                                                                                    Console.WriteLine(lines + " |" + test + "|");
                                                                                                                                                }*/
                                                                                                                                        } else {
                                                                                                                                                throw new ArgumentException("Synax error at line '" + line + "'." + " C3965");
                                                                                                                                        }
                                                                                                                                } else if (s.StartsWith("app.color = ")) {
                                                                                                                                        Appcolor(s);
                                                                                                                                        script_var.Ifcount = 1;
                                                                                                                                } else if (s.StartsWith("app.background = ")) {
                                                                                                                                        string color = s.Replace("app.background = ", "");
                                                                                                                                        color = char.ToUpper(color[0]) + color.Substring(1);
                                                                                                                                        Console.BackgroundColor = (ConsoleColor) Enum.Parse(typeof (ConsoleColor), color);
                                                                                                                                        script_var.Ifcount = 1;
                                                                                                                                } else if (s == "sound.beep()") {
                                                                                                                                        Console.Beep();
                                                                                                                                } else if (s.StartsWith("new image(") && s.EndsWith(")")) {
                                                                                                                                        string result = s.Replace("new image(", "").TrimEnd(')');
                                                                                                                                        string[] result2 = result.Split(", ");
                                                                                                                                        var image = new CanvasImage(Format(result2[0]));
                                                                                                                                        image.MaxWidth(int.Parse(Format(result2[1])));
                                                                                                                                        AnsiConsole.Render(image);
                                                                                                                                } else if (s.StartsWith("sound.play = ")) {
                                                                                                                                        string res = s.Replace("sound.play = ", "");
                                                                                                                                        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                                                                                                                                                Process.Start(@ "powershell", $ @ "-c (New-Object Media.SoundPlayer '{Format(res)}').PlaySync();");
                                                                                                                                        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
                                                                                                                                                try {
                                                                                                                                                        string program = "cvlc";
                                                                                                                                                        var pi = new ProcessStartInfo(res) {
                                                                                                                                                                Arguments = Path.GetFileName(res) + " --play-and-exit",
                                                                                                                                                                        UseShellExecute = true,
                                                                                                                                                                        WorkingDirectory = Format(res),
                                                                                                                                                                        FileName = program,
                                                                                                                                                                        Verb = "OPEN",
                                                                                                                                                                        WindowStyle = ProcessWindowStyle.Hidden
                                                                                                                                                        };
                                                                                                                                                        Process p = new Process();
                                                                                                                                                        p.StartInfo = pi;
                                                                                                                                                        p.Start();
                                                                                                                                                        p.WaitForExit();
                                                                                                                                                } catch {
                                                                                                                                                        Console.WriteLine("Please install VLC Media Player to work.");
                                                                                                                                                }
                                                                                                                                        }
                                                                                                                                } else if (s.StartsWith("app.debug-off")) {
                                                                                                                                        //script_var.Ifcount = 1;
                                                                                                                                } else if (s.StartsWith("app.foreground")) {
                                                                                                                                        string color = s.Replace("app.foreground = ", "");
                                                                                                                                        color = char.ToUpper(color[0]) + color.Substring(1);
                                                                                                                                        Console.ForegroundColor = (ConsoleColor) Enum.Parse(typeof (ConsoleColor), color);
                                                                                                                                        script_var.Ifcount = 1;
                                                                                                                                } else if (s.StartsWith("cp ")) {
                                                                                                                                        string result = s.Replace("cp ", "");
                                                                                                                                        string[] valuearray = result.Split(", ");
                                                                                                                                        File.Copy(Format(valuearray[0]), Format(valuearray[1]), true);
                                                                                                                                        script_var.Ifcount = 1;
                                                                                                                                } else if (s.StartsWith("mv ")) {
                                                                                                                                        string result = s.Replace("cp ", "");
                                                                                                                                        string[] valuearray = result.Split(", ");
                                                                                                                                        File.Move(Format(valuearray[0]), Format(valuearray[1]), true);
                                                                                                                                        script_var.Ifcount = 1;
                                                                                                                                } else if (s.StartsWith("rm ")) {
                                                                                                                                        string result = s.Replace("rm ", "");
                                                                                                                                        File.Delete(Format(result));
                                                                                                                                        script_var.Ifcount = 1;
                                                                                                                                } else if (s.StartsWith("Grid.AddColumn(")) {
                                                                                                                                        if (GridSelect) {
                                                                                                                                                string result1 = s.Replace("Grid.AddColumn", "");
                                                                                                                                                string result2 = result1.Replace(")", "");
                                                                                                                                                result2 = result2.Replace("(", "");
                                                                                                                                                tb.AddColumn("[" + Console.ForegroundColor + "]" + Format(result2) + "[/]");
                                                                                                                                        }
                                                                                                                                } else if (s.StartsWith("Grid.AddRow(")) {
                                                                                                                                        if (GridSelect) {
                                                                                                                                                string result1 = s.Replace("Grid.AddRow", "");
                                                                                                                                                string result2 = result1.Replace(")", "");
                                                                                                                                                result2 = result2.Replace("(", "");
                                                                                                                                                string[] valuearray = result2.Split(", ");
                                                                                                                                                tb.AddRow("[" + Console.ForegroundColor + "]" + Format(valuearray[0]) + "[/]", "[" + Console.ForegroundColor + "]" + Format(valuearray[1]) + "[/]");
                                                                                                                                        }
                                                                                                                                } else if (s.StartsWith("Grid.Display(")) {
                                                                                                                                        if (GridSelect)
                                                                                                                                                AnsiConsole.Render(tb);
                                                                                                                                } else if (s == "app.breakpoint()") {
                                                                                                                                        var table = new Table();
                                                                                                                                        table.Border = TableBorder.Rounded;
                                                                                                                                        table.AddColumn("[white]Breakpoint[/]");
                                                                                                                                        table.AddRow("[white]Status=[/][green]running[/]");

                                                                                                                                        table.AddRow("[white]-- Strings --[/]");

                                                                                                                                        foreach(string Finalvalue in Stringname) {
                                                                                                                                                table.AddRow("[blue]" + Finalvalue + "[/]=[red]" + Format(Finalvalue) + "[/]");
                                                                                                                                        }
                                                                                                                                        AnsiConsole.Render(table);
                                                                                                                                } else if (s.StartsWith("new Grid(")) {
                                                                                                                                        GridSelect = true;
                                                                                                                                } else if (s.StartsWith("new Barchart")) {
                                                                                                                                        string result1 = s.Replace("new Barchart", "");
                                                                                                                                        string result2 = result1.Replace(")", "");
                                                                                                                                        result2 = result2.Replace("(", "");

                                                                                                                                        bc.Width = 100;
                                                                                                                                        bc.Label("[" + Console.ForegroundColor + "]" + Format(result2) + "[/]");
                                                                                                                                        bc.CenterLabel();
                                                                                                                                        script_var.Ifcount = 1;
                                                                                                                                } else if (s.StartsWith("new Rule(")) {
                                                                                                                                        string result = s.Replace("new Rule(", "");
                                                                                                                                        result = result.Replace(")", "");
                                                                                                                                        if (string.IsNullOrWhiteSpace(result)) {
                                                                                                                                                var rule = new Rule("[" + Console.ForegroundColor + "][/]");
                                                                                                                                                AnsiConsole.Render(rule);
                                                                                                                                        } else {
                                                                                                                                                var rule = new Rule("[" + Console.ForegroundColor + "]" + Format(result) + "[/]");
                                                                                                                                                AnsiConsole.Render(rule);
                                                                                                                                        }
                                                                                                                                        script_var.Ifcount = 1;
                                                                                                                                } else if (s.StartsWith("new Calendar(")) {
                                                                                                                                        string result = s.Replace("new Calendar(", "");
                                                                                                                                        result = result.Replace(")", "");
                                                                                                                                        string[] valuearray = result.Split(", ");
                                                                                                                                        var calendar = new Calendar(int.Parse(valuearray[0]), int.Parse(valuearray[1]));
                                                                                                                                        calendar.HeaderStyle(Style.Parse(Console.ForegroundColor.ToString()));
                                                                                                                                        AnsiConsole.Render(calendar);

                                                                                                                                } else if (s.StartsWith("Barchart.Add(") && s.EndsWith(")")) {
                                                                                                                                        string result = s.Replace("Barchart.Add(", "");
                                                                                                                                        result = result.Replace(")", "");
                                                                                                                                        string[] valuearray = result.Split(", ");
                                                                                                                                        bc.AddItem("[" + Console.ForegroundColor + "]" + Format(valuearray[0]) + "[/]" /*+ Format(valuearray[0])*/ , Double.Parse(valuearray[1]), Console.ForegroundColor);
                                                                                                                                        script_var.Ifcount = 1;
                                                                                                                                } else if (s.StartsWith("Barchart.Display(") && s.EndsWith(")")) {
                                                                                                                                        AnsiConsole.Render(bc);
                                                                                                                                } else if (s.StartsWith("zip ")) {
                                                                                                                                        if (Packages.FileIO) {
                                                                                                                                                string result = s.Replace("zip ", "");
                                                                                                                                                string[] valuearray = result.Split(", ");
                                                                                                                                                ZipFile.CreateFromDirectory(Format(valuearray[0]), Format(valuearray[1]));
                                                                                                                                                script_var.Ifcount = 1;
                                                                                                                                        } else {
                                                                                                                                                throw new ArgumentException("Synax error at line '" + line + "'." + " C3965");
                                                                                                                                        }
                                                                                                                                } else if (s.StartsWith("unzip ")) {
                                                                                                                                        if (Packages.FileIO) {
                                                                                                                                                string result = s.Replace("unzip ", "");
                                                                                                                                                string[] valuearray = result.Split(", ");
                                                                                                                                                ZipFile.ExtractToDirectory(Format(valuearray[0]), Format(valuearray[1]));
                                                                                                                                                script_var.Ifcount = 1;
                                                                                                                                        } else {
                                                                                                                                                throw new ArgumentException("Synax error at line '" + line + "'." + " C3965");
                                                                                                                                        }
                                                                                                                                } else if (s.StartsWith("if") && s.EndsWith(":")) {
                                                                                                                                        string result = s.Replace(":", "");
                                                                                                                                        Newif(result);
                                                                                                                                } else if (s.StartsWith("else") && s.EndsWith(":")) {
                                                                                                                                        Else();
                                                                                                                                } else if (s.StartsWith(" ")) {
                                                                                                                                        string input = s.Replace("  ", "");
                                                                                                                                        //Console.WriteLine(input + "/" + script_var.Ifcount);
                                                                                                                                        if (input.StartsWith("print")) {
                                                                                                                                                if (script_var.Ifcount == 2) {
                                                                                                                                                        Print(input);
                                                                                                                                                }
                                                                                                                                        } else if (input.StartsWith("wait")) {
                                                                                                                                                if (script_var.Ifcount == 2) {
                                                                                                                                                        Wait(input);
                                                                                                                                                }
                                                                                                                                        } else if (input.StartsWith("set ")) {
                                                                                                                                                if (script_var.Ifcount == 2) {
                                                                                                                                                        Set(input);
                                                                                                                                                }
                                                                                                                                        } else if (input.StartsWith("exit")) {
                                                                                                                                                if (script_var.Ifcount == 2) {
                                                                                                                                                        Environment.Exit(0);
                                                                                                                                                }
                                                                                                                                        } else if (input.StartsWith("app.title = ")) {
                                                                                                                                                if (script_var.Ifcount == 2) {
                                                                                                                                                        Apptitle(input);
                                                                                                                                                }
                                                                                                                                        } else if (input.StartsWith("app.color = ")) {
                                                                                                                                                if (script_var.Ifcount == 2) {
                                                                                                                                                        Appcolor(input);
                                                                                                                                                }
                                                                                                                                        } else if (input.StartsWith("app.size = ")) {
                                                                                                                                                if (script_var.Ifcount == 2) {
                                                                                                                                                        Appsize(input);
                                                                                                                                                }
                                                                                                                                        } else if (input.StartsWith("start")) {
                                                                                                                                                if (script_var.Ifcount == 2) {
                                                                                                                                                        string loc = input.Replace("start ", "");
                                                                                                                                                        //Console.WriteLine(loc);
                                                                                                                                                        ProcessStartInfo startInfo = new ProcessStartInfo();
                                                                                                                                                        startInfo.FileName = loc;
                                                                                                                                                        startInfo.Arguments = "";
                                                                                                                                                        Process.Start(startInfo);
                                                                                                                                                }
                                                                                                                                        } else {
                                                                                                                                                throw new ArgumentException("7 Internal error at line '" + line + "'");
                                                                                                                                        }
                                                                                                                                } else if (s.StartsWith("int ")) {
                                                                                                                                        Int(s);
                                                                                                                                        script_var.Ifcount = 1;
                                                                                                                                } else if (s.StartsWith("figlet ")) {
                                                                                                                                        string result = s.Replace("figlet ", "");
                                                                                                                                        AnsiConsole.Render(
                                                                                                                                                new FigletText(Format(result))
                                                                                                                                                .LeftAligned()
                                                                                                                                                .Color(Console.ForegroundColor));
                                                                                                                                        script_var.Ifcount = 1;
                                                                                                                                } else if (s.StartsWith("set ")) {
                                                                                                                                        Set(s);
                                                                                                                                        script_var.Ifcount = 1;
                                                                                                                                } else if (s.StartsWith("string ")) {
                                                                                                                                        String(s);
                                                                                                                                        script_var.Ifcount = 1;
                                                                                                                                } else if (s == "") {
                                                                                                                                        //empty line 
                                                                                                                                } else if (s == "app.read()") {
                                                                                                                                        Appread();
                                                                                                                                        script_var.Ifcount = 1;
                                                                                                                                } else if (s.StartsWith("#")) {
                                                                                                                                        /*ignore #*/ } else {
                                                                                                                                        throw new ArgumentException("Synax error at line '" + line + "'.");
                                                                                                                                }

                                                                                                                                line++;

                                                                                                                                void Int(string input) {
                                                                                                                                        if (input.Contains(" = ")) {
                                                                                                                                                try {
                                                                                                                                                        script_var.Intcount++;
                                                                                                                                                        string fullname1 = input.Replace("int ", "");
                                                                                                                                                        string fullname2 = fullname1.Replace(" = ", " ");
                                                                                                                                                        string[] valuearray = fullname2.Split(" ");
                                                                                                                                                        int intvalue = Int16.Parse(valuearray[1]);
                                                                                                                                                        string intname = valuearray[0];
                                                                                                                                                        switch (script_var.Intcount) {
                                                                                                                                                        case 1:
                                                                                                                                                                script_var.Int1 = intvalue;
                                                                                                                                                                script_var.Intname1 = intname;
                                                                                                                                                                break;
                                                                                                                                                        case 2:
                                                                                                                                                                script_var.Int2 = intvalue;
                                                                                                                                                                script_var.Intname2 = intname;
                                                                                                                                                                break;
                                                                                                                                                        default:
                                                                                                                                                                string storedcolor = Console.ForegroundColor.ToString();
                                                                                                                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                                                                                                                Console.WriteLine("int limit reached");
                                                                                                                                                                // set the color after warning to the color before
                                                                                                                                                                Console.ForegroundColor = (ConsoleColor) Enum.Parse(typeof (ConsoleColor), storedcolor);
                                                                                                                                                                break;
                                                                                                                                                                //throw new ArgumentException("Synax error at line '" + line + "'.");
                                                                                                                                                                // max int's reached
                                                                                                                                                        }

                                                                                                                                                        //new system
                                                                                                                                                        Intname = Intname.Append(intname + "=" + intvalue).ToArray();
                                                                                                                                                        /*foreach (string lines in Intname) 
                                                                                                                                                        {
                                                                                                                                                            Console.WriteLine(lines);
                                                                                                                                                        }*/
                                                                                                                                                } catch {
                                                                                                                                                        throw new ArgumentException("Synax error at line '" + line + "'." + " C8575");
                                                                                                                                                }
                                                                                                                                        } else {
                                                                                                                                                throw new ArgumentException("Synax error at line '" + line + "'." + " C3108");
                                                                                                                                        }
                                                                                                                                }
                                                                                                                                void Newif(string input) {
                                                                                                                                        string results = input.Replace("if ", "");
                                                                                                                                        string trimmed = results.Replace(" = ", "/n");
                                                                                                                                        string[] valuearray = trimmed.Split("/n");

                                                                                                                                        bool Locked = false;

                                                                                                                                        if (valuearray[1].StartsWith('"') && valuearray[1].StartsWith('"')) {
                                                                                                                                                valuearray[1] = valuearray[1].Trim('"');
                                                                                                                                        }

                                                                                                                                        int Num = 0;
                                                                                                                                        int Num2 = 0;

                                                                                                                                        foreach(string lines in Stringname) {
                                                                                                                                                Num++;
                                                                                                                                                if (!Locked) {
                                                                                                                                                        if (lines == valuearray[0]) {
                                                                                                                                                                foreach(string lines2 in Stringvalue) {
                                                                                                                                                                        Num2++;
                                                                                                                                                                        if (Num == Num2) {
                                                                                                                                                                                if (lines2 == valuearray[1]) {
                                                                                                                                                                                        Read();
                                                                                                                                                                                } else if (lines2 != valuearray[1]) {
                                                                                                                                                                                        Ignore();
                                                                                                                                                                                }
                                                                                                                                                                        }
                                                                                                                                                                }
                                                                                                                                                        }
                                                                                                                                                }
                                                                                                                                        }

                                                                                                                                        void Read() {
                                                                                                                                                script_var.Ifcount = 2;
                                                                                                                                        }

                                                                                                                                        void Ignore() {
                                                                                                                                                script_var.Ifcount = 1;
                                                                                                                                        }
                                                                                                                                }
                                                                                                                                void Else() {
                                                                                                                                        if (script_var.Ifcount == 1) {
                                                                                                                                                script_var.Ifcount = 2;
                                                                                                                                                // inverse ifcount
                                                                                                                                        } else if (script_var.Ifcount == 2) {
                                                                                                                                                script_var.Ifcount = 1;
                                                                                                                                                //ignore becouse normal if is executed
                                                                                                                                        }
                                                                                                                                }
                                                                                                                                void Appread() {
                                                                                                                                        Console.Read();
                                                                                                                                }
                                                                                                                                void Appcolor(string input) {
                                                                                                                                        string color = input.Replace("app.color = ", "");
                                                                                                                                        color = char.ToUpper(color[0]) + color.Substring(1);
                                                                                                                                        Console.ForegroundColor = (ConsoleColor) Enum.Parse(typeof (ConsoleColor), color);
                                                                                                                                        //Console.ForegroundColor = 
                                                                                                                                        /*string color = input.Replace("app.color = ", "");
                                                                                                                                        switch (color)
                                                                                                                                        {
                                                                                                                                            case "":
                                                                                                                                                break;
                                                                                                                                            case "blue":
                                                                                                                                                Console.ForegroundColor = ConsoleColor.Blue;
                                                                                                                                                break;
                                                                                                                                            case "yellow":
                                                                                                                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                                                                                                                break;
                                                                                                                                            case "green":
                                                                                                                                                Console.ForegroundColor = ConsoleColor.Green;
                                                                                                                                                break;
                                                                                                                                            case "white":
                                                                                                                                                Console.ForegroundColor = ConsoleColor.White;
                                                                                                                                                break;
                                                                                                                                            case "red":
                                                                                                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                                                                                                break;
                                                                                                                                            default:
                                                                                                                                                throw new ArgumentException("Synax error at line '" + line + "'." + " C1793");
                                                                                                                                        }*/
                                                                                                                                }
                                                                                                                                void Print(string input) {
                                                                                                                                        string rawdata = input.Replace("print ", "");
                                                                                                                                        if (rawdata.StartsWith('@')) {
                                                                                                                                                string rawdata2 = rawdata.Remove(0, 1);
                                                                                                                                                Console.Write(Format(rawdata2));
                                                                                                                                        } else {
                                                                                                                                                Console.WriteLine(Format(rawdata));
                                                                                                                                        }
                                                                                                                                }
                                                                                                                                void Appsize(string input) {
                                                                                                                                        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                                                                                                                                                string widthheight = input.Replace("app.size = ", "");
                                                                                                                                                string width;
                                                                                                                                                string height;
                                                                                                                                                widthheight.Replace(" ", "/n");
                                                                                                                                                string[] valuearray = widthheight.Split(' ');
                                                                                                                                                width = valuearray[0];
                                                                                                                                                height = valuearray[1];
                                                                                                                                                int widthint = Int16.Parse(width);
                                                                                                                                                int heightint = Int16.Parse(height);
                                                                                                                                                Console.SetWindowSize(widthint, heightint);
                                                                                                                                        } else {
                                                                                                                                                throw new ArgumentException("Not supported on current platform! Line '" + line + "'." + " C1793");
                                                                                                                                        }
                                                                                                                                }
                                                                                                                                void Appclear() {
                                                                                                                                        Console.Clear();
                                                                                                                                }
                                                                                                                                void Apptitle(string input) {
                                                                                                                                        string rawdata = input.Replace("app.title = ", "");
                                                                                                                                        //Old code.
                                                                                                                                        /*if (rawdata.StartsWith('"') && rawdata.EndsWith('"'))
                                                                                                                                        {
                                                                                                                                            string rawdata2 = rawdata.Trim('"');
                                                                                                                                            Console.Title = rawdata2;
                                                                                                                                        }
                                                                                                                                        else
                                                                                                                                        {
                                                                                                                                            int num = 0;
                                                                                                                                            int num2 = 0;
                                                                                                                                            foreach (string lines in Stringname)
                                                                                                                                            {
                                                                                                                                                num++;
                                                                                                                                                if (lines == rawdata)
                                                                                                                                                {
                                                                                                                                                    foreach (string lines2 in Stringvalue)
                                                                                                                                                    {
                                                                                                                                                        num2++;
                                                                                                                                                        //Console.WriteLine("num=" + num + " num2=" + num2);
                                                                                                                                                        if (num == num2) { Console.Title = lines2; }
                                                                                                                                                    }
                                                                                                                                                }
                                                                                                                                            }
                                                                                                                                        }*/
                                                                                                                                        Console.Title = Format(rawdata);
                                                                                                                                }
                                                                                                                                void Wait(string input) {
                                                                                                                                        string time = input.Replace("wait ", "");
                                                                                                                                        int timeint = Int16.Parse(time);
                                                                                                                                        Thread.Sleep(TimeSpan.FromSeconds(timeint));
                                                                                                                                }
                                                                                                                                string Format(string value) {
                                                                                                                                        string finalvalue = "";
                                                                                                                                        if (value.StartsWith('"') && value.EndsWith('"')) {
                                                                                                                                                string rawdata2 = value.Trim('"');
                                                                                                                                                finalvalue = rawdata2;
                                                                                                                                        } else {
                                                                                                                                                int num = 0;
                                                                                                                                                int num2 = 0;
                                                                                                                                                foreach(string lines in Stringname) {
                                                                                                                                                        num++;
                                                                                                                                                        if (lines == value) {
                                                                                                                                                                foreach(string lines2 in Stringvalue) {
                                                                                                                                                                        num2++;
                                                                                                                                                                        if (num == num2) {
                                                                                                                                                                                finalvalue = lines2; /*checkreplace();*/
                                                                                                                                                                        }
                                                                                                                                                                }
                                                                                                                                                        }
                                                                                                                                                }
                                                                                                                                        }

                                                                                                                                        if (finalvalue.Contains("{environment.username}")) {
                                                                                                                                                string replace = finalvalue.Replace("{environment.username}", Environment.UserName.ToString());
                                                                                                                                                finalvalue = replace;
                                                                                                                                        }

                                                                                                                                        if (finalvalue.Contains("{environment.machinename}")) {
                                                                                                                                                string replace = finalvalue.Replace("{environment.machinename}", Environment.MachineName.ToString());
                                                                                                                                                finalvalue = replace;
                                                                                                                                        }

                                                                                                                                        if (finalvalue.Contains("<string.")) {

                                                                                                                                                int pFrom = finalvalue.IndexOf("<string.") + "<string.".Length;
                                                                                                                                                int pTo = finalvalue.LastIndexOf(">");
                                                                                                                                                String result = finalvalue.Substring(pFrom, pTo - pFrom);
                                                                                                                                                result.Replace("<string.", "");
                                                                                                                                                result.Replace(">", "");
                                                                                                                                                string replace = finalvalue.Replace("<string." + result + ">", Format(result));
                                                                                                                                                finalvalue = replace;
                                                                                                                                        }

                                                                                                                                        if (finalvalue.Contains("{Net.IP}")) {
                                                                                                                                                if (Packages.Net) {
                                                                                                                                                        string GetLocalIPAddress() {
                                                                                                                                                                var host = Dns.GetHostEntry(Dns.GetHostName());
                                                                                                                                                                foreach(var ip in host.AddressList) {
                                                                                                                                                                        if (ip.AddressFamily == AddressFamily.InterNetwork) {
                                                                                                                                                                                return ip.ToString();
                                                                                                                                                                        }
                                                                                                                                                                }
                                                                                                                                                                throw new Exception("No network adapters with an IPv4 address in the system.");
                                                                                                                                                        }

                                                                                                                                                        string replace = finalvalue.Replace("{Net.IP}", GetLocalIPAddress().ToString());
                                                                                                                                                        finalvalue = replace;
                                                                                                                                                }
                                                                                                                                        }

                                                                                                                                        return finalvalue;

                                                                                                                                }
                                                                                                                                void Set(string input) {
                                                                                                                                        string input2 = input.Replace("set ", "");
                                                                                                                                        string[] valuearray = input2.Split(" = ");
                                                                                                                                        int num = 0;
                                                                                                                                        int num2 = 0;
                                                                                                                                        string finalvalue = "";
                                                                                                                                        string value = valuearray[0];
                                                                                                                                        foreach(string lines in Stringname.ToList()) {
                                                                                                                                                num++;
                                                                                                                                                if (lines == valuearray[0]) {
                                                                                                                                                        foreach(string lines2 in Stringvalue.ToList()) {
                                                                                                                                                                num2++;
                                                                                                                                                                if (num == num2) {
                                                                                                                                                                        finalvalue = lines2;
                                                                                                                                                                        Stringname.Remove(value);
                                                                                                                                                                        Stringvalue.Remove(finalvalue);
                                                                                                                                                                        Stringname.Add(value);
                                                                                                                                                                        Stringvalue.Add(Format(valuearray[1]));
                                                                                                                                                                }
                                                                                                                                                        }
                                                                                                                                                }
                                                                                                                                        }
                                                                                                                                }
                                                                                                                                void String(string input) {
                                                                                                                                        try {
                                                                                                                                                string fullname1 = input.Replace("string ", "");
                                                                                                                                                string fullname2 = fullname1.Replace(" = ", "/n");
                                                                                                                                                string[] valuearray = fullname2.Split("/n");
                                                                                                                                                if (valuearray[1] == "app.read()") {
                                                                                                                                                        string result = Console.ReadLine().ToLower();
                                                                                                                                                        Stringname.Add(valuearray[0]);
                                                                                                                                                        Stringvalue.Add(result);
                                                                                                                                                } else {
                                                                                                                                                        Stringname.Add(valuearray[0]);
                                                                                                                                                        Stringvalue.Add(Format(valuearray[1]));
                                                                                                                                                }
                                                                                                                                        } catch {
                                                                                                                                                Console.WriteLine("error");
                                                                                                                                        }
                                                                                                                                }
                                                                                                                        }
                                                                                                                } catch (Exception e) {
                                                                                                                        if (e.Source != null)
                                                                                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                                                                        Console.WriteLine("Exception: {0}" + " at line " + line, e);
                                                                                                                        Thread.Sleep(10000);
                                                                                                                        throw;
                                                                                                                }
                                                                                                        }
                                                                                                }
                                                                                        }
                                                                                }

                                                                                void Checkenvironment() {
                                                                                        check_environment.Is64x = Environment.Is64BitProcess;
                                                                                        check_environment.Username = Environment.UserName;
                                                                                        check_environment.OSversion = Environment.OSVersion.ToString();
                                                                                        DateTime d1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                                                                                        DateTime d2 = new DateTime(DateTime.Now.Year, 6, DateTime.Now.Day);
                                                                                        if (d1 == d2) {
                                                                                                Isettingsconf.ShowRainbow = true;
                                                                                        } else {
                                                                                                Isettingsconf.ShowRainbow = false;
                                                                                        }
                                                                                }

                                                                                void Debuger() {
                                                                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                                                                        Console.WriteLine("===============");
                                                                                        Console.WriteLine("  Kookaburra Debuger - Sysinfo: is64x=" + check_environment.Is64x + " OSversion=" + check_environment.OSversion + " ProcessorCount=" + Environment.ProcessorCount + " CurrentManagedThreadId=" + Environment.CurrentManagedThreadId);
                                                                                        Console.WriteLine("  Type 'app.debug-off' in the first line of your program to disable this message.");
                                                                                        Console.WriteLine("===============");
                                                                                        // default color
                                                                                        Console.ForegroundColor = ConsoleColor.White;
                                                                                }
                                                                        }
                                                                }
                                                        }
