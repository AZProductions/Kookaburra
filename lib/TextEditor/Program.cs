using Terminal.Gui;
using NStack;
using System;
using System.IO;
using System.Threading;

class KookaburraEditor {
	public static void Main(string[] args)
	{


		Application.Init();
		var top = Application.Top;

		var win = new Window("Kookaburra Editor")
		{
			X = 0,
			Y = 1, // Leave one row for the toplevel menu
			Width = Dim.Fill(),
			Height = Dim.Fill()
		};

		top.Add(win);

		var textView = new TextView()
		{
			X = Pos.Center(),
			Y = Pos.Percent(0),
			Width = Dim.Percent(100),
			Height = Dim.Percent(100),
			//Text = File.ReadAllText(@"D:\Projects\Kookaburra\updates.txt")
		};

		var rect = new Rect(10, 20, 10, 20);

		var fileDialog = new SaveDialog(/*"Save as", "Save", "Directory", "File name", "Do you want to save the file?"*/);

		var menu = new MenuBar(new MenuBarItem[] {
			new MenuBarItem ("_File", new MenuItem [] {
				new MenuItem ("_New", "", () => { textView.Text = ""; /*Resets the text*/ }),
				new MenuItem ("Save _As", "",() => { win.Add(fileDialog); /*The program closes for no reason.*/}),
				new MenuItem ("_Open file", "",() => { /*todo: openfiledialog?*/ }),
				new MenuItem ("_Quit", "", () => { if (Quit ()) top.Running = false; })
			}),
			new MenuBarItem ("_Edit", new MenuItem [] {
				new MenuItem ("_Copy", "", null),
				new MenuItem ("C_ut", "", null),
				new MenuItem ("_Paste", "", null)
			})
		});
		top.Add(menu);

		static bool Quit()
		{
			var n = MessageBox.Query(50, 7, "Quit Editor", "Are you sure you want to quit the editor?", "Yes", "No");
			return n == 0;
		}


		win.Add(
			textView
		); ; ;

		Application.Run();
	}
}
