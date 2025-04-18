﻿using Final_Project__Space_Guy;

namespace Program
{
    public class program
    {
        public static void MenuChoices(char choice)
        {
            switch (choice)
            {
                case 'O':
                    //If no saves, this returns an error. If there is saves, open save menu, and let player select one.
                    Console.WriteLine("Opperational.");
                    CheckForSaves();
                    break;
                case 'N':

                   
                {
                Console.WriteLine("Welcome to the game, bounty hunter.");
                Console.Write("Please enter your name for our systems, bounty hunter: ");
                string PlayerName = Console.ReadLine();

                PlayerCharacter player = PlayerCharacter.LoadGame(PlayerName) ?? new Player(PlayerName); //Creating new player

                Console.WriteLine($"Hello, {player.Name}. You have {player.Credits} credits to your name. To earn credits, catch criminals and earn their bounties.");

                MainMenu(player);
           
                    break;
                case 'Q':
                    //This will quit the game and close the program.
                    Console.WriteLine("Quite well");
                    break;
                case 'S':
                    //They can change the game to baby mode, normal mode, or expert mode. 
                    Console.WriteLine("Snapping");
                    break;
                case 'T':
                    //Idk, just an easter egg.
                    Console.WriteLine("It's me, Tenna!");
                    break;
            }
        }
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
            Console.WriteLine("1. Increase capacity by 1 (100 credits)");
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
                            new Criminal("Jimmy the Throat Slitter!", 500, 3),
                            new Criminal("Vael the Shadow Walker", 800, 5),
                            new Criminal("Zorak Bloodfang", 1200, 7)
                        };
                        Console.WriteLine("Wanted Board: Choose a criminal to hunt.");
            for (int i = 0; i < availableCriminals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {availableCriminals[i].Name} - Bounty: {availableCriminals[i].Bounty} credits"); //looks at list, and shows to player
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

        private static void CheckForSaves() //Just checking if there are saves available. Saves are in a 
        {

        }

        
    }
}