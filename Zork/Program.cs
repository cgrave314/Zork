using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace Zork
{
    internal class Program
    {
        private static Room CurrentRoom
        {
            get
            {
                return Rooms[Location.row, Location.column];
            }
        }

        static void Main(string[] args)
        {
            const string roomDescriptionsFilename = "Rooms.json";
            InitializeRoomDescriptions(roomDescriptionsFilename);

            Console.WriteLine("Welcome to Zork!");

            Room previousRoom = null;
            Commands command = Commands.UNKNOWN;
            while (command != Commands.QUIT)
            {
                Console.WriteLine(CurrentRoom.Name);

                if (previousRoom != CurrentRoom)
                {
                    Console.WriteLine(CurrentRoom.Description);
                    previousRoom = CurrentRoom;
                }

                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());
                switch (command)
                {
                    case Commands.QUIT:
                        Console.WriteLine("Thank you for playing.");
                        break;

                    case Commands.LOOK:
                        Console.WriteLine(CurrentRoom.Description);
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        if (Move(command) == false)
                        {
                            Console.WriteLine("The way is shut!");
                        }
                        break;

                    default:
                        Console.WriteLine("Unknown command.");
                        break;
                }
            }
        }


        private static bool Move(Commands command)
        {
            Assert.IsTrue(Directions.Contains(command), "Invalid direction.");

            bool isValidMove = true;
            switch (command)
            {
                case Commands.NORTH when Location.row < Rooms.GetLength(0) - 1:
                    Location.row++;
                    break;

                case Commands.SOUTH when Location.row > 0:
                    Location.row--;
                    break;

                case Commands.EAST when Location.column < Rooms.GetLength(1) - 1:
                    Location.column++;
                    break;

                case Commands.WEST when Location.column > 0:
                    Location.column--;
                    break;

                default:
                    isValidMove = false;
                    break;
            }

            return isValidMove;
        }

        public static Commands ToCommand(string commandString) => Enum.TryParse(commandString, ignoreCase: true, out Commands result) ? result : Commands.UNKNOWN;
        private static void InitializeRoomDescriptions(string roomDescriptionsFilename)
        {
           Rooms = JsonConvert.DeserializeObject<Room[,]>(File.ReadAllText(roomDescriptionsFilename));
        }

        private static Room[,] Rooms =
        {
            {new Room("Rocky Trail"), new Room("South of House"), new Room("Canyon View") },
            {new Room("Forest"), new Room("West of House"), new Room("Behind House") },
            {new Room("Dense Woods"), new Room("North of House"), new Room("Clearing") }
        };

        private static readonly Commands[] Directions =
        {
            Commands.NORTH,
            Commands.SOUTH,
            Commands.EAST,
            Commands.WEST
        };

        private enum Fields
        {
            Name = 0,
            Description = 1
        }


        private static (int row, int column) Location = (1, 1);
    }
}
