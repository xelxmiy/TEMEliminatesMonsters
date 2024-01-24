using System.Linq;
using System.Windows.Forms;

namespace TEMEliminatesMonsters.Src;

internal class Program
{

	public static readonly string GameName = "TEM Eliminates Monsters";

	/// <summary>
	/// Entry Point for the program
	/// </summary>
	/// <param name="args">args if ran through console</param>
	private static void Main (string[] args) // .. this is where it all beings...
	{
		Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
		TEM game = new();
		game.Window.Title = GameName;
		game.Run();
		if (args.Contains("fullscreen"))
		{
			game.GoFullScreen();
		}
	}
}