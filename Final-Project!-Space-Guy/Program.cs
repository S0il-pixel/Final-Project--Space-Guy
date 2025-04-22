using Final_Project__Space_Guy;

namespace Program
{
    public class program
    {
        public static void MenuChoices(char choice)
        {
            switch (choice)
            {
                case 'N':


                    {
                        Console.WriteLine("Welcome to the game, bounty hunter.");
                        Console.Write("Please enter your name for our systems, bounty hunter. Or, enter the name of your bounty hunter from a previous save to load that game: ");
                        string PlayerName = Console.ReadLine();

                        PlayerCharacter player = PlayerCharacter.LoadGame(PlayerName) ?? new PlayerCharacter(PlayerName, "New character"); //Either Loading a saved game character, or creating a new character with the new name given. ?? means it's nullable or optional

                        Console.WriteLine($"Hello, {player.Name}. You have {player.Credits} credits to your name. To earn credits, catch criminals and earn their bounties.");

                        MainMenu(player);
                        break;
                    }
           
                case 'Q':
                    //This will quit the game and close the program.
                    Console.WriteLine("Closing program...");
                    break;
                case 'S':
                    Console.WriteLine("You think there's settings? This is a text rpg, what in the world are you going to change?");
                    break;
                case 'T':
                    //Idk, just an easter egg.
                    Console.WriteLine("It's me, Tenna!");
                    break;
            }
        }
            

            static void MainMenu(PlayerCharacter player)
            {
                Dictionary<string, Action<PlayerCharacter>> options = new Dictionary<string, Action<PlayerCharacter>>
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
                    string choice = Console.ReadLine()?.ToLower(); //Reads the text, and converts all characters to lowercase just to not confuse my code. ? means nullable/seeing if it works 

                    if (options.ContainsKey(choice)) //Takes the choice the player made, and uses the dictionary options to then move to the methods that coinside with their choices. 
                    {
                        options[choice](player);
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice.");
                    }
                }
            }
        
        static void Shop(PlayerCharacter player)
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
                Console.WriteLine($"{itemChoice} purchased! Remaining credits: {player.Credits}");  //I'm going to make this larger, such as adding tools they can buy, and instead of weapon upgrades it's weapons that make the minigames easier.
            }
            else
            {
                Console.WriteLine("Not enough credits or invalid choice.");
            }
        }

        static void Mechanic(PlayerCharacter player)
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

        static void WantedBoard(PlayerCharacter player)
        {
            List<Criminal> availableCriminals = new List<Criminal>
                        {
                            new Criminal("Jimmy the Throat Slitter!", 500, 1),
                            new Criminal("Vael the Shadow Walker", 800, 2),
                            new Criminal("Zorak Bloodfang", 1200, 3),
                            new Criminal("Mafie guy: Al Capone!", 2000, 3)
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

                if (target.Difficulty == 1)
                {
                    Random random = new Random();
                    int Number = random.Next(1, 3);
                    RandomMiniGameEasy(Number);
                }
                else if (target.Difficulty == 2)
                {
                    Random random = new Random();
                    int Number = random.Next(1, 3);
                    RandomMiniGameNormal(Number);
                }
                else if (target.Difficulty == 3)
                {
                    Random random = new Random();
                    int Number = random.Next(1, 3);
                    RandomMiniGameDifficult(Number);
                }
                else Console.WriteLine("Error!");

                Console.WriteLine($"Captured {target.Name}! You earn {target.Bounty} credits.");
                player.CapturedCriminals.Add(target);
                player.Credits += target.Bounty;
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }

        public static void RandomMiniGameEasy(int number)
        {
            switch (number)
            {

                case 1:
                    {
                        //mini game 1
                        break;
                    }
                case 2:
                    {
                        //mini game 2
                        break;
                    }
                case 3:
                    {
                        //mini game 3
                        break;
                    }
            }
        }

        public static void RandomMiniGameNormal(int number)
        {
            switch (number)
            {

                case 1:
                    {
                        //mini game 1
                        break;
                    }
                case 2:
                    {
                        //mini game 2
                        break;
                    }
                case 3:
                    {
                        //mini game 3
                        break;
                    }
            }
        }

        public static void RandomMiniGameDifficult(int number)
        {
            switch (number)
            {

                case 1:
                    {
                        //mini game 1
                        break;
                    }
                case 2:
                    {
                        //mini game 2
                        break;
                    }
                case 3:
                    {
                        //mini game 3
                        break;
                    }
            }
        }

       
    }
}