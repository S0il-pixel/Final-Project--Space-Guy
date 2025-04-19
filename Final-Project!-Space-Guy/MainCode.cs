using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project__Space_Guy
{
    public class MainCode : Program.program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Space Bounty Hunter RPG game! You are at the main menu. To select an option, type in the letter in the brackets() beside the option.");
            Console.WriteLine(
                "(O)Open Save" +
                "(N)New Game" +
                "(Q)Quit" +
                "(S)Settings");
            string PlayerChoice = Console.ReadLine();
            char choice = char.Parse(PlayerChoice);
            MenuChoices(choice);

            MainMenu(player);
        }

        static void MainMenu(PlayerCharacter player)
        {
            Dictionary<string, Action<Player>> options = new Dictionary<string, Action<Player>>
                {
                { "shop", Shop },
                { "mechanic", Mechanic },
                { "wanted board", WantedBoard },
                { "save", p => p.SaveGame() }
                };

            while (true)
            {
                player.UpdateHungerAndFuel();
                Console.WriteLine($"Hunger: {player.Hunger}  |  Fuel: {player.PlayerShip.Fuel}");
                Console.WriteLine("Where would you like to go? (shop, mechanic, wanted board, save)");
                string choice = Console.ReadLine()?.ToLower();

                if (options.ContainsKey(choice))
                {
                    options[choice](player);
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                }
            }
        }
    }
    static void Shop(Player player)
    {
        Dictionary<string, int> shopItems = new Dictionary<string, int>
                        {
                            { "fuel", 50 },
                            { "food", 30 },
                            { "weapon upgrade", 200 }
                        };

        Console.WriteLine("Welcome to the shop! Items available:");
        foreach (var item in shopItems)
        {
            Console.WriteLine($"{item.Key}: {item.Value} credits");
        }

        Console.Write("What would you like to buy? ");
        string itemChoice = Console.ReadLine()?.ToLower();

        if (shopItems.ContainsKey(itemChoice) && player.Credits >= shopItems[itemChoice])
        {
            player.Credits -= shopItems[itemChoice];
            if (itemChoice == "fuel") player.PlayerShip.Fuel += 50;
            if (itemChoice == "food") player.Hunger += 30;
            Console.WriteLine($"{itemChoice} purchased! Remaining credits: {player.Credits}");
        }
        else
        {
            Console.WriteLine("Not enough credits or invalid choice.");
        }
    }

    static void Mechanic(Player player)
    {
        Console.WriteLine("Welcome to the mechanic! Ship upgrades available:");
        Console.WriteLine("1. Increase capacity (100 credits)");
        Console.WriteLine("2. Improve fuel efficiency (150 credits)");

        Console.Write("Choose an upgrade: ");
        string upgradeChoice = Console.ReadLine();

        if (upgradeChoice == "1" && player.Credits >= 100)
        {
            player.PlayerShip.Capacity += 2;
            player.Credits -= 100;
            Console.WriteLine("Capacity upgraded! Ship now holds more criminals.");
        }
        else if (upgradeChoice == "2" && player.Credits >= 150)
        {
            player.PlayerShip.Fuel += 50;
            player.Credits -= 150;
            Console.WriteLine("Fuel efficiency improved!");
        }
        else
        {
            Console.WriteLine("Invalid choice or insufficient credits.");
        }
    }

    static void WantedBoard(Player player)
    {
        List<Criminal> availableCriminals = new List<Criminal>
                        {
                            new Criminal("Xaroth the Silent", 500, 3),
                            new Criminal("Vael Stormrider", 800, 5),
                            new Criminal("Zorak Bloodfang", 1200, 7)
                        };

        Console.WriteLine("Wanted Board: Choose a criminal to hunt.");
        for (int i = 0; i < availableCriminals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {availableCriminals[i].Name} - Bounty: {availableCriminals[i].Bounty} credits");
        }

        Console.Write("Enter the number of the criminal you want to hunt: ");
        int choice = int.Parse(Console.ReadLine());

        if (choice > 0 && choice <= availableCriminals.Count)
        {
            Criminal target = availableCriminals[choice - 1];
            Console.WriteLine($"You are now hunting {target.Name}.");

            // Placeholder: Minigame implementation will go here

            Console.WriteLine($"Captured {target.Name}! You earn {target.Bounty} credits.");
            player.CapturedCriminals.Add(target);
            player.Credits += target.Bounty;
        }
        else
        {
            Console.WriteLine("Invalid choice.");
        }
    }

    //MyObject myObject = new MyObject
    //{
    //    Id = 1
    //    Name = "Sample Object",
    //    CreatedDate = DateTime.Now
    //};

    //string filePath = "myObject.json";
    //JsonFileHandler.SaveObjectToFile(myObject, filePath);
}

    }
}
