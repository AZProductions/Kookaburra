using Terminal.Gui;
using NStack;
using System;
using System.IO;
using System.Threading;

class KookaburraEditor {
	static string loc;
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
			AllowsTab = true
			//Text = File.ReadAllText(@"D:\Projects\Kookaburra\updates.txt")
		};

		var rect = new Rect(10, 20, 10, 20);


		var menu = new MenuBar(new MenuBarItem[] {
			new MenuBarItem ("_File", new MenuItem [] {
				new MenuItem ("_New", "", () => { textView.Text = ""; /*Resets the text*/ }),
				new MenuItem ("Save _As", "",() => 
				{
					bool run = true;
					while (run)
					{
						OpenDialog selector = new OpenDialog("Save File.", "Save the file currently open in the editor.")
						{
							CanChooseFiles = true,
							AllowsMultipleSelection = false
						};
						Application.Run(selector);
						if (selector.Canceled)
							break;
						else if (selector.FilePath != null)
						{
							File.WriteAllText(loc, textView.Text.ToString());
							run = false;
						}
					}
				}),
				new MenuItem ("_Open file", "",() => 
				{
					bool run = true;
					while (run)
					{
						OpenDialog selector = new OpenDialog("Select file.", "Select a file to edit.")
						{
							CanChooseFiles = true,
							AllowsMultipleSelection = false
						};
						Application.Run(selector);
						if (selector.Canceled)
							break;
						else if (selector.FilePath != null)
						{
							loc = selector.FilePath.ToString();
							textView.Text = File.ReadAllText(selector.FilePath.ToString());
							run = false;
						}
					}
				}),
				new MenuItem ("_Quit", "", () => { if (Quit ()) top.Running = false; })
			}),
			new MenuBarItem ("_Edit", new MenuItem [] {
				new MenuItem ("_Copy", "", () => { textView.Copy(); }),
				new MenuItem ("C_ut", "", () => { textView.Cut(); }/*,null, null, Key.End*/),
				new MenuItem ("_Paste", "", () => { textView.Paste(); })
			})
		});
		top.Add(menu);

		static bool Quit()
		{
			var n = MessageBox.Query(50, 7, "Quit Editor", "Are you sure you want to quit the editor?", "Yes", "No");
			return n == 0;
		}

		/*textView.KeyPress += (k) =>
		{
			if (k.KeyEvent.IsCtrl) 
			{
				if (k.KeyEvent.Key == Key.A)
					textView.SelectAll();
			}
		};*/

		win.Add(
			textView
		);

		Application.Run();
	}
}
