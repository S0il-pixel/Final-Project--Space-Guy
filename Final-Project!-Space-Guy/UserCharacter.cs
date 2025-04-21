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
        public int Hunger { get; set; }
        private DateTime LastUpdate { get; set; }

        public PlayerCharacter(string name, string description) //There's so much stuff :,0
        {
            Name = name;
            Description = description;
            Credits = 500;
            Scores = 0;
            Gear = new List<Stuff>();
            PlayerShip = new Ship("Starter Ship");
            CapturedCriminals = new List<Criminal>();
            LastUpdate = DateTime.Now;
        }

        private void UpdateHungerAndFuel()
        {
            TimeSpan elapsedTime = DateTime.Now - LastUpdate;
            int Hours = (int)elapsedTime.TotalHours;

            if (Hours > 0)
            {
                Hunger -= Hours * 2; //Lost hunger and fuel over time
                PlayerShip.Fuel -= Hours * 3;
                LastUpdate = DateTime.Now; //Reset last updated time
            }

            Hunger = Math.Max(Hunger, 0); //make sure it can't get to negative hunger or fuel.
            PlayerShip.Fuel = Math.Max(PlayerShip.Fuel, 0); 
        }

        public void SaveGame()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented); //Creating the save Json file, serializing the object Player
            File.WriteAllText($"{Name}.json", json);
            Console.WriteLine("Game saved!");
        }

        public static Player LoadGame(string name)
        {
            if (File.Exists($"{name}.json"))
            {
                string json = File.ReadAllText($"{name}.json");  
                return JsonConvert.DeserializeObject<Player>(json); //If the save is found, the Json file is deserialized (the object is created again that was saved), and is then able to be used again.
            }
            Console.WriteLine("No saved game found."); //If no save is found, this is what is returned, and the player must create a new save. 
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
            Capacity = 5; // Start with space for 5 criminals (This can be changed)
            Fuel = 100; //Start with full fuel (This can be expanded)
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

