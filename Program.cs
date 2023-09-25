namespace TEMEliminatesMonsters
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            TEM game = new TEM();
            game.Window.Title = "TEM Eliminates Monsters";
            game.Run();
        }
    }
}