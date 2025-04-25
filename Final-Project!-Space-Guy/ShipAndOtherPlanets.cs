using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class Ship
{
    public string Name { get; set; }
    public int Capacity { get; set; }
    public int Fuel { get; set; }
    public List<string> Upgrades { get; set; }

    public Ship(string name)
    {
        Name = name;
        Capacity = 5; // Start with space for 5 criminals (This can be changed) I need to change this. Cause rn it doesn't do anything. 
        Fuel = 100; //Start with full fuel (This can be expanded) I need to change this so that you can't get more fuel than you have space for.
        Upgrades = new List<string>();
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