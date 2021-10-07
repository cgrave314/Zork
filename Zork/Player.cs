using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Zork
{
    public class Player
    {
        public World World { get; }

        [JsonIgnore]
        public Room Location { get; set; }

        public Player(World world, string startingLocation)
        {
            World = world;
        }

        public bool Move(Directions direction)
        {
            bool isValidMove = Location.Neighbors.TryGetValue(direction, out Room destination);
            if (isValidMove)
            {
                Location = destination;
            }

            return isValidMove;
        }
    }
}
