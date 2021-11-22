using Newtonsoft.Json;
using System.ComponentModel;

namespace Zork.Common
{
    public class Player : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public World World { get; }

        [JsonIgnore]
        public Room CurrentRoom{ get; set; }

        [JsonIgnore]
        public Room PreviousRoom { get; set; }


        public Player(World world, string startingLocation)
        {

            World = world;
            CurrentRoom = World.RoomsByName[startingLocation];

        }


        public bool Move(Directions direction)
        {

            bool isValidMove = CurrentRoom.Neighbors.TryGetValue(direction, out Room neighbor);
            if(isValidMove)
            {
                CurrentRoom = neighbor;
            }

            return isValidMove;

        }
    }
}
