using System.Linq;

namespace TEMEliminatesMonsters.src
{
    internal class Program
    {
        /// <summary>
        /// Entry Point for the program
        /// </summary>
        /// <param name="args">args if ran thru console</param>
        private static void Main(string[] args) // .. this is where it all beings...
        {
            TEM game = new();
            game.Window.Title = "TEM Eliminates Monsters";
            game.Run();
            if (args.Contains("fullscreen"))
            {
                game.GoFullScreen();
            }
        }
    }
}