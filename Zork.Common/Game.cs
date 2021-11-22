using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Zork.Common
{
    public class Game : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        [JsonIgnore]
        public IInputService Input { get; private set; }

        [JsonIgnore]
        public IOutputService Output { get; private set; }

        [JsonIgnore]
        public static Game Instance { get; private set; }

        public World World { get; set; }

        public string StartingLocation { get; set; }

        public string WelcomeMessage { get; set; }

        public string ExitMessage { get; set; }

        [JsonIgnore]
        public Player Player { get; private set; }

        [JsonIgnore]
        public bool IsRunning { get; private set; }


        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            Player = new Player(World, StartingLocation);
        }

        public static void StartGameFromFile(string gameFileName, IInputService input, IOutputService output)
        {
            if(!File.Exists(gameFileName))
            {
                throw new FileNotFoundException("Expected file.", gameFileName);
            }

            StartGame(File.ReadAllText(gameFileName), input, output);
        }

        public static void StartGame(string jsonString, IInputService input, IOutputService output)
        {
            //game = JsonConvert.DeserializeObject<Game>(jsonString);
            Instance = Load(jsonString);
            Instance.IsRunning = true;
            Instance.Input = input;
            Instance.Output = output;
            Instance.DisplayWelcomeMessage();
            Instance.Input.InputReceived += Instance.InputReceivedHandler;
            //Instance.Run();
        }
        public static Game Load(string jsonString)
        {
            Game game = JsonConvert.DeserializeObject<Game>(jsonString);
            return game;
        }

        private void InputReceivedHandler(object sender, string inputString)
        {

            Commands command = Commands.UNKNOWN;
            Instance.Player.PreviousRoom = Instance.Player.CurrentRoom;

            //Output.WriteLine(Instance.Player.CurrentRoom);

            //Output.Write("\n> ");
            command = ToCommand(inputString.Trim());

            //string outputString;
                switch (command)
                {
                    case Commands.QUIT:
                        //outputString = Instance.ExitMessage;
                        Instance.IsRunning = false;
                        break;

                    case Commands.LOOK:
                    //outputString = Instance.Player.CurrentRoom.Description;
                    Output.WriteLine(Instance.Player.CurrentRoom);
                    Output.WriteLine(Instance.Player.CurrentRoom.Description);
                    break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        Directions direction = (Directions)command;
                    //outputString = Instance.Player.Move(direction) ? $"You moved {command}." : "The way is shut!";
                    Output.WriteLine(Instance.Player.Move(direction) ? $"You moved {command}." : "The way is shut!");
                        break;

                    default:
                    //outputString = "Unknown command.";
                    Output.WriteLine("Unknown command.");
                        break;
                }

                //Output.WriteLine(outputString);
            if (Instance.Player.PreviousRoom != Instance.Player.CurrentRoom)
            {
                Output.WriteLine(Instance.Player.CurrentRoom);
                Output.WriteLine(Instance.Player.CurrentRoom.Description);
                Instance.Player.PreviousRoom = Instance.Player.CurrentRoom;
            }
            
        }

        private void DisplayWelcomeMessage()
        {
            Output.WriteLine(Instance.WelcomeMessage);
        }

        static Commands ToCommand(string commandString)
        {
            return Enum.TryParse<Commands>(commandString, ignoreCase: true, out Commands command) ? command : Commands.UNKNOWN;
        }

    }
}
