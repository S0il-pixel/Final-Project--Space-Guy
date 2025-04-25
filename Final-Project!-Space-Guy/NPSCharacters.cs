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


public class Criminal
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


public enum WorkerRole
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

    public async Task CompleteMissionAsync(Helpers helper, string Task, int HowLongTaskTakes)
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

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
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