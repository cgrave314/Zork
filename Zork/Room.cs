using Newtonsoft.Json;

namespace Zork
{
    public class Room
    {
        [JsonProperty("NotName")]
        public string Name { get; }

        public string Description { get; set; }
        
        public Room(string name, string description = null)
        {
            Name = name;
            Description = description;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
