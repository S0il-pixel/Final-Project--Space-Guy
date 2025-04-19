using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;



namespace Final_Project__Space_Guy
{
    public class PlayerCharacter
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Credits { get; set; } 
        public int Scores { get; set; } 
        public List<Stuff> Gear { get; set; }
        public Ship PlayerShip { get; set; }
        public List<Criminal> CapturedCriminals { get; set; }

        public PlayerCharacter(string name, string description)
        {
            Name = name;
            Description = description;
            Credits = 500;
            Scores = 0;
            Gear = new List<Stuff>();
            PlayerShip = new Ship("Starter Ship");
            CapturedCriminals = new List<Criminal>();
        }
        public void SaveGame()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText($"{Name}.json", json);
            Console.WriteLine("Game saved!");
        }

        public static Player LoadGame(string name)
        {
            if (File.Exists($"{name}.json"))
            {
                string json = File.ReadAllText($"{name}.json");
                return JsonConvert.DeserializeObject<Player>(json);
            }
            Console.WriteLine("No saved game found.");
            return null;
        }
    }

    public class Stuff
    {
        public class Weapons
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public string Description { get; set; }
            public int Cost { get; set; }
            public int Damage { get; set; }

            public Weapons(string name, string type, string description, int cost, int damage)
            {
                Name = name;
                Type = type;
                Description = description;
                Cost = cost;
                Damage = damage;
            }
        }

        public class Tools
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int Cost { get; set; }
            public int UsesLeft { get; set; }

            public Tools(string name, string description, int cost, int usesLeft)
            {
                Name = name;
                Description = description;
                Cost = cost;
                UsesLeft = usesLeft;
            }
        }

        public class Helpers
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Dialogue { get; set; }
            public string Skills { get; set; }
            public int Cost { get; set; }
            public List<object> Gear { get; set; }

            public Helpers(string name, string description, string dialogue, string skills, int cost, List<object> gear)
            {
                Name = name;
                Description = description;
                Dialogue = dialogue;
                Skills = skills;
                Cost = cost;
                Gear = gear;
            }
        }
    }

    public class Ship
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int Fuel { get; set; }
        public List<string> Upgrades { get; set; }

        public Ship(string name)
        {
            Name = name;
            Capacity = 5; // Start with space for 5 criminals
            Fuel = 100;
            Upgrades = new List<string>();
        }
    }

    public class Criminal
    {
        public string Name { get; set; }
        public int Bounty { get; set; }
        public int Difficulty { get; set; }

        public Criminal(string name, int bounty, int difficulty)
        {
            Name = name;
            Bounty = bounty;
            Difficulty = difficulty;
        }
    }

