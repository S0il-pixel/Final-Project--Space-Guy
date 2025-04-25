using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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

