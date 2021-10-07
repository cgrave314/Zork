using System;
using System.IO;
using Newtonsoft.Json;


namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            const string defualtGameFilename = "Zork.json";
            string gameFilename = args.Length > 0 ? args[(int)CommandLineArguments.GameFilename] : defualtGameFilename;

            Game game = Game.Load(gameFilename);
            Console.WriteLine("Welcome to Zork!");
            game.Run();
            Console.WriteLine("Thank you for playing!");
        }
        private enum CommandLineArguments
        {
            GameFilename = 0,
        }
    }
}
