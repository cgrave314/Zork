using System.IO;
using Newtonsoft.Json;
using Zork.Common;

namespace Zork
{
    class Program
    {

        private enum CommandLineArguments
        {
            GameFileName = 0
        }

        static void Main(string[] args)
        {

            const string defaultGameFileName = "Zork.json";
            string gameFileName = args.Length > 0 ? args[(int)CommandLineArguments.GameFileName] : defaultGameFileName;
            //string jsonString = File.ReadAllText(gameFileName);

            //IOutputService output = new ConsoleOutputService();
            //Game game = JsonConvert.DeserializeObject<Game>(jsonString);
            //game.Run();

            ConsoleInputService input = new ConsoleInputService();
            ConsoleOutputService output = new ConsoleOutputService();

            Game.StartGameFromFile(gameFileName, input, output);

            output.WriteLine(Game.Instance.Player.CurrentRoom);
            output.WriteLine(Game.Instance.Player.CurrentRoom.Description);

            while (Game.Instance.IsRunning)
            {
                //output.WriteLine(Game.Instance.Player.CurrentRoom);
                //output.WriteLine(Game.Instance.Player.CurrentRoom.Description);
                output.Write("\n> ");
                input.GetInput();
            }

            output.WriteLine(Game.Instance.ExitMessage);
        }

    }
}