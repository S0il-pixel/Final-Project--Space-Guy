using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;



namespace Final_Project__Space_Guy 
{
    public class PlayerCharacter : 
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Credits { get; set; }
        public int Scores { get; set; }
        public List<Stuff> Gear { get; set; }
        public Ship PlayerShip { get; set; }
        public List<Criminal> CapturedCriminals { get; set; }
        public List<Helpers> Helpers { get; set; }
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
            Helpers = new List<Helpers>();
            Hunger = 100;
            LastUpdate = DateTime.Now;
        }

        public void UpdateHungerAndFuel()
        {
            TimeSpan elapsedTime = DateTime.Now - LastUpdate;
            int Hours = (int)elapsedTime.TotalSeconds;

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

        public static PlayerCharacter LoadGame(string name)
        {
            if (File.Exists($"{name}.json"))
            {
                // ~ or .../Final-Project!-Space-Guy/Final-Project!-Space-Guy/Final-Project!-Space-Guy.csproj/jsonfile.
                string json = File.ReadAllText($"{name}.json");
                return JsonConvert.DeserializeObject<PlayerCharacter>(json); //If the save is found, the Json file is deserialized (the object is created again that was saved), and is then able to be used again.
            }
            Console.WriteLine("No saved game found."); //If no save is found, this is what is returned, and the player must create a new save. 
            return null;
        }
    }

    public class Stuff
    {
        public abstract class Tools
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int Cost { get; set; }
            public int UsesLeft { get; set; }

            public Tools()
            {
                //This tells the user how this tool helps. 
               //certain amount of uses. Use this in inheritance and polymorphism
            }
        }

        public class LazerGun : Tools
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int Cost { get; set; }
            public int UsesLeft { get; set; }

            public LazerGun()
            {
                Name = "Lazer Gun";
                Description = "This tool will add five seconds to your time."; 
                Cost = 30;
                UsesLeft = 5; 
            }
        }
        public class Crowbar : Tools
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int Cost { get; set; }
            public int UsesLeft { get; set; }

            public Crowbar()
            {
                Name = "Crow Bar";
                Description = "This tool will add ten seconds to your time.";
                Cost = 50;
                UsesLeft = 3;
            }
        }
        public class LazerRiffle : Tools
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int Cost { get; set; }
            public int UsesLeft { get; set; }

            public LazerRiffle()
            {
                Name = "Lazer Riffle";
                Description = "This tool will add twenty five seconds to your time";
                Cost = 500;
                UsesLeft = 5;
            }
        }
        public class ElectricDagger : Tools
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int Cost { get; set; }
            public int UsesLeft { get; set; }

            public ElectricDagger()
            {
                Name = "Electric Charged Dagger";
                Description = "This tool will extend the time limit by 15 seconds";
                Cost = 20;
                UsesLeft = 5;
            }
        }
        public class SaberOfLight : Tools
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int Cost { get; set; }
            public int UsesLeft { get; set; }

            public SaberOfLight()
            {
                Name = "The Saber of Light";
                Description = "This tool will scare the shit out of the criminal, and they will give up and come willingly";
                Cost = 5200;
                UsesLeft = 15;
            }
        }
        public class Club : Tools
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int Cost { get; set; }
            public int UsesLeft { get; set; }

            public Club()
            {
                Name = "A club.";
                Description = "This tool will extend the time limit by 30 seconds";
                Cost = 40;
                UsesLeft = 5;
            }
        }
    }

    public class Criminal //Encapsulation
    {
        private string name;
        private int bounty;
        private int difficulty;

        public Criminal(string name, int bounty, int difficulty)
        {
            Name = name;
            Bounty = bounty;
            Difficulty = difficulty;
        }

        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be null or empty.");
                name = value;
            }
        }

        public int Bounty
        {
            get { return bounty; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Bounty must be greater than zero.");
                bounty = value;
            }
        }

        public int Difficulty
        {
            get { return difficulty; }
            set
            {
                if (value < 1 || value > 3)
                    throw new ArgumentException("Difficulty must be between 1 and 3.");
                difficulty = value;
            }
        }

    }


    public enum WorkerRole //Enums
    {
        Soldier,
        Cook,
        Intern,
        Engineer
    }

    public class Helpers
    {
        public string Name { get; set; }
        public string Skills { get; set; }
        public WorkerRole Role { get; set; }
        public int Cost { get; set; }

        public Helpers(string name, string skills, WorkerRole role, int cost)
        {
            Name = name;
            Skills = skills; //How they help with mini games
            Role = role;
            Cost = cost; //They have to pay this every week (Use date time)
        }

        private void CharacterStats(); //Fix this

        public async Task CompleteMissionAsync(Helpers helper, string Task, int HowLongTaskTakes) //Async, and date time (in case the hunger and fuel functions don't work haha,)
        {
            Console.WriteLine($"{helper.Name}, the {helper.Role}, is starting their task: {Task}.");
            DateTime startTime = DateTime.Now;
            Console.WriteLine($"Mission Start Time: {startTime}");

            // Simulate mission work
            await Task.Delay(HowLongTaskTakes * 1000);

            DateTime endTime = DateTime.Now;
            Console.WriteLine($"Task Completed! End Time: {endTime}");
            Console.WriteLine($"{helper.Name} successfully completed their task: '{Task}' in {(endTime - startTime).TotalSeconds} seconds.");
        }
    }

    public class Ship //OOP
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int Fuel { get; set; }
        public List<string> Upgrades { get; set; } //Lists

        public Ship(string name)
        {
            Name = name;
            Capacity = 5; // Start with space for 5 criminals (This can be changed) I need to change this. Cause rn it doesn't do anything. 
            Fuel = 100; //Start with full fuel (This can be expanded) I need to change this so that you can't get more fuel than you have space for.
            Upgrades = new List<string>();
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)] //Use of Attributes and reflection
    public class PlanetAttribute : Attribute
    {
        public string TerrainType { get; }
        public int DangerLevel { get; }
        public string UniqueResource { get; }

        public PlanetAttribute(string terrainType, int dangerLevel, string uniqueResource)
        {
            TerrainType = terrainType;
            DangerLevel = dangerLevel;
            UniqueResource = uniqueResource;
        }
    }
    [Planet("Rocky", 1, "Rare Minerals")]
    public class TerraPrime
    {
        public string Name => "Gerinnan";
        public string Description => "A rocky planet rich in rare minerals and suitable for exploration.";
    }

    [Planet("Desert", 2, "Alien Fossils")]
    public class Arachon
    {
        public string Name => "Arachon";
        public string Description => "A harsh desert planet where alien fossils can be discovered.";
    }

    [Planet("Forest", 3, "Medical Plants")]
    public class Sylva
    {
        public string Name => "Lyrx";
        public string Description => "A dense forest planet teeming with mystic plants and hidden dangers.";
    }

    [Planet("Frozen", 2, "Large Crystal Ice")]
    public class Cryon
    {
        public string Name => "Cierian";
        public string Description => "A frozen wasteland hiding crystalline ice under its glaciers.";
    }

    [Planet("Volcanic", 4, "Lava Stones")]
    public class Pyronis
    {
        public string Name => "Statillunis";
        public string Description => "A volatile volcanic planet where lava stones can be harvested.";
    }

    public static void DisplayPlanets()
    {
        var planetTypes = Assembly.GetExecutingAssembly().GetTypes() //Why is this brokennn
            .Where(t => t.GetCustomAttributes<PlanetAttribute>().Any()); //uuuuugh

        foreach (var type in planetTypes)
        {
            var attribute = type.GetCustomAttribute<PlanetAttribute>();
            var nameProperty = type.GetProperty("Name");
            var descriptionProperty = type.GetProperty("Description");

            string name = nameProperty?.GetValue(Activator.CreateInstance(type))?.ToString() ?? "Unknown";
            string description = descriptionProperty?.GetValue(Activator.CreateInstance(type))?.ToString() ?? "No description available.";

            Console.WriteLine($"Planet: {name}");
            Console.WriteLine($"Description: {description}");
            Console.WriteLine($"Terrain: {attribute.TerrainType}");
            Console.WriteLine($"Danger Level: {attribute.DangerLevel}");
            Console.WriteLine($"Unique Resource: {attribute.UniqueResource}");
            Console.WriteLine("-------------------------------");
        }
    }
}