﻿using System.IO;
using Newtonsoft.Json;


namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            const string defualtGameFilename = "Zork.json";
            string gameFilename = args.Length > 0 ? args[(int)CommandLineArguments.GameFilename] : defualtGameFilename;

            Game game = JsonConvert.DeserializeObject<Game>(File.ReadAllText(gameFilename));
            game.Run();
        }
        private enum CommandLineArguments
        {
            GameFilename = 0,
        }
    }
}
