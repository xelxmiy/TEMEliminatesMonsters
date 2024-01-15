using System.Linq;
using System.Windows.Forms;

namespace TEMEliminatesMonsters.src;

internal class Program
{
	/// <summary>
	/// Entry Point for the program
	/// </summary>
	/// <param name="args">args if ran thru console</param>
	private static void Main (string[] args) // .. this is where it all beings...
	{

		Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
		TEM game = new();
		game.Window.Title = "TEM Eliminates Monsters";
		game.Run();
		if (args.Contains("fullscreen"))
		{
			game.GoFullScreen();
		}
	}
}