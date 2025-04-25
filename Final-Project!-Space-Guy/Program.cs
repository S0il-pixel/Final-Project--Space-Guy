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
                    Console.WriteLine("You think there's settings? This is a text rpg, what in the world are you going to change?"); //Hehe
                    break;
                case 'T':
                    //Idk, just an easter egg.
                    Console.WriteLine("It's me, Tenna!");
                    break;
            }
        }
            

            static void MainMenu(PlayerCharacter player)
            {
                Dictionary<string, Action<PlayerCharacter>> options = new Dictionary<string, Action<PlayerCharacter>> //Creates a new dictionary, where when the key is called by the user, it calls the method that it is connected to in the dictionary. I thought this was a cool use of the dictionary :)
                {
                { "shop", Shop },
                { "mechanic", Mechanic },
                { "wanted board", WantedBoard },
                { "guilds for hire", Guilds },
                { "save", p => p.SaveGame() } //Saves the Json file containing the object. Idk where the Json file is rn, but hey, it works :P
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
            player.UpdateHungerAndFuel();
            Dictionary<string, int> shopItems = new Dictionary<string, int>
                        {
                            { "fuel refill", 50 },
                            { "food refill", 30 },
                            { "weapon upgrade", 200 }
                            { "Lazer Gun", 30 }
                            { "Crowbar", 50 }
                            { "Lazer Riffle", 500 }
                            { "Electric Charged Dagger", 20 }
                            { "Saber of Light", 5200 }
                            { "Club", 40 }
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
                if (itemChoice == "fuel") player.PlayerShip.Fuel = player.PlayerShip.Fuel; //I need to make sure this actually costs something, and also the amount doesn't exceed how much the player can contain. 
                if (itemChoice == "food") player.Hunger = player.Hunger;
                if (itemChoice == "Lazer Gun") player.Gear.Add(LazerGun());
                if (itemChoice) == "Crowbar") player.Gear.Add(Crowbar());
                if (itemChoice) == "Lazer Riffle") player.Gear.Add(LazerRiffle());
                if (itemChoice) == "Electric Charged Dagger") player.Gear.Add(ElectricDagger());
                if (itemChoice) == "Saber of Light") player.Gear.Add(SaberOfLight());
                if (itemChoice) == "Club") player.Gear.Add(Club());
                Console.WriteLine($"{itemChoice} purchased! Remaining credits: {player.Credits}");  //I'm going to make this larger, such as adding tools they can buy, and instead of weapon upgrades it's weapons that make the minigames easier.
            }
            else
            {
                Console.WriteLine("Not enough credits or invalid choice.");
            }
        }

        static void Mechanic(PlayerCharacter player)
        {
            player.UpdateHungerAndFuel();
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
            player.UpdateHungerAndFuel();
            List<Criminal> availableCriminals = new List<Criminal>
                        {
                            new Criminal("Jimmy the Throat Slitter!", 500, 1),
                            new Criminal("Vael the Shadow Walker", 800, 2),
                            new Criminal("Zorak Bloodfang", 1200, 3),
                            new Criminal("Mafie guy: Al Capone!", 2000, 3)
                            new Criminal("Carmen the Swift", 700, 1),
                            new Criminal("Lynx Dark", 900, 2),
                            new Criminal("Grimgor the Crusher", 1500, 3),
                            new Criminal("Scarlet Dagger", 400, 1),
                            new Criminal("Ragnar Ironclad", 1000, 2),
                            new Criminal("Morgath Firebrand", 1800, 3),
                            new Criminal("Silas Blackhand", 600, 1),
                            new Criminal("Thorne the Silent", 750, 2),
                            new Criminal("Xara Venomstrike", 1100, 3),
                            new Criminal("Erebus the Phantom", 900, 2),
                            new Criminal("Brutus Bloodfang", 1400, 3)
                        };

            Console.WriteLine("Wanted Board: ");

            availableCriminals.ForEach(c => Console.WriteLine($"Name: {Criminal.Name}, Bounty: {Criminal.Bounty}, Difficulty: {Criminal.Difficulty}"));

            Console.WriteLine("Choose the difficulty you want to hunt. 1, 2, 3 :");
            string difficultyLevel = Console.ReadLine();
            int difficulty = int.Parse(difficultyLevel)

            var filteredCriminals = criminals.Where(Criminals => Criminals.Difficulty == difficulty).ToList();

            Console.WriteLine($"Criminals with difficulty {Criminal.Difficulty}:");
            filteredCriminals.ForEach(c => Console.WriteLine($"Name: {Criminal.Name}, Bounty: {Criminal.Bounty}, Difficulty: {Criminal.Difficulty}"));

            Console.Write("Enter the name of the criminal you want to hunt: ");
            string choice = Console.ReadLine(); 

            if (choice == availableCriminals)
            {
                Criminal target = availableCriminals[choice - 1];
                Console.WriteLine($"You are now hunting {target.Name}.");

                Random random = new Random();
                int Number = random.Next(1, 3);

                if (target.Difficulty == 1)
                {              
            
                    RandomMiniGameEasy(Number);
                }
                else if (target.Difficulty == 2)
                {
           
                    RandomMiniGameNormal(Number);
                }
                else if (target.Difficulty == 3)
                {
                
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
                        Game("The criminal runs, and jumps over a barel.")
                        break;
                    }
                case 2:
                    {
                        //mini game 2
                        Game("The criminal tries to shoot you!")
                        break;
                    }
                case 3:
                    {
                        //mini game 3
                        Game("You find your target, and throw a rock.")
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
                        Game("The criminal has attacked you, you dodge but the criminal persists!");
                        break;
                    }
                case 2:
                    {
                        //mini game 2
                        Game("Where did he go? Lookout! Your target is behind you!");
                        break;
                    }
                case 3:
                    {
                        //mini game 3
                        Game("The criminal was sleeping when you found them, but is it safe?");
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
                        Game("The criminal attacks your ship with their band of criminals, you have to fight them off, quickly!");
                        break;
                    }
                case 2:
                    {
                        //mini game 2
                        Game("They have a secret base, that is very hard to find. And when you find it, they wer waiting for you! It's a trap!");
                        break;
                    }
                case 3:
                    {
                        //mini game 3
                        Game("You shoot at the criminal with your blaster, but they dodge with skill. How will you ever catch the evil doer?");
                        break;
                    }
            }
        }

        public static void Game(PlayerCharacter player, string sentence)
        {
            Console.WriteLine("Your sentence to type is:");
            Console.WriteLine(sentence);
            Console.WriteLine("You have 10 seconds to type the sentence exactly as shown. Ready? Go!");

            DateTime startTime = DateTime.Now;
            string userInput = Console.ReadLine();
            DateTime endTime = DateTime.Now;

            TimeSpan timeTaken = endTime - startTime;

            if (timeTaken.TotalSeconds <= 10)
            {
                if (userInput == sentenceToType)
                {
                    Console.WriteLine($"Great job! You typed the sentence correctly in {timeTaken.TotalSeconds:F2} seconds. You have caught the criminal!");
                }
                else
                {
                    Console.WriteLine($"Oops! You didn't type the sentence correctly, and the criminal escaped. Try again!");
                }
            }
            else if (player.Gear.Contains(LazerGun()) && userInput <= 15)
            {//if they have tools, it changes. 
                if (userInput == sentenceToType)
                {
                    Console.WriteLine($"Great job! You typed the sentence correctly in {timeTaken.TotalSeconds:F2} seconds. You have caught the criminal!");
                }
                else
                {
                    Console.WriteLine($"Oops! You didn't type the sentence correctly, and the criminal escaped. Try again!");
                }
            }
            else if (player.Gear.Contains(CrowBar()) && userInput <= 20)
            {
                if (userInput == sentenceToType)
                {
                    Console.WriteLine($"Great job! You typed the sentence correctly in {timeTaken.TotalSeconds:F2} seconds. You have caught the criminal!"); //F2 means a float point number. So it has two decimal places only.
                }
                else
                {
                    Console.WriteLine($"Oops! You didn't type the sentence correctly, and the criminal escaped. Try again!");
                }
            }
            else if (player.Gear.Contains(LazerRiffle()) && userInput <= 25)
            {
                if (userInput == sentenceToType)
                {
                    Console.WriteLine($"Great job! You typed the sentence correctly in {timeTaken.TotalSeconds:F2} seconds. You have caught the criminal!");
                }
                else
                {
                    Console.WriteLine($"Oops! You didn't type the sentence correctly, and the criminal escaped. Try again!");
                }
            }
            else if (player.Gear.Contains(ElectricDagger()) && userInput <= 35)
            {
                if (userInput == sentenceToType)
                {
                    Console.WriteLine($"Great job! You typed the sentence correctly in {timeTaken.TotalSeconds:F2} seconds. You have caught the criminal!");
                }
                else
                {
                    Console.WriteLine($"Oops! You didn't type the sentence correctly, and the criminal escaped. Try again!");
                }
            }
            else if (player.Gear.Contains(Club()) && userInput <= 40)
            {
                if (userInput == sentenceToType)
                {
                    Console.WriteLine($"Great job! You typed the sentence correctly in {timeTaken.TotalSeconds:F2} seconds. You have caught the criminal!");
                }
                else
                {
                    Console.WriteLine($"Oops! You didn't type the sentence correctly, and the criminal escaped. Try again!");
                }
            }
            else if (player.Gear.Contains(SaberOfLight())
            {
                Console.WriteLine("The criminal shit themselves out of fear, and you caught them! Good work!");
            }
            else
            {
                Console.WriteLine($"Time's up! The criminal has gotten away. Better luck next time!");
            }
        }

        public async void Guilds(PlayerCharacter player)
        {
            player.UpdateHungerAndFuel();
            List<Helpers> availableHelpers = new List<Helpers>
                        {
                            new Helpers("Shane","Will go off and collect criminals. Will give you 60% of the credits they earn.", Soldier, 3000),
                            new Helpers("Amanda", "Will make you food, so that you don't go hungry while out in space.", Cook, 5000),
                            new Helpers("Angela", "Will help you catch criminals, expanding the time limit for the mini game.", Intern, 6000),
                            new Helpers("Keith", "Will add solar and nuclear power to your ship, so you don't need fuel anymore.", Engineer, 8000)
                        };
            Console.WriteLine("To hire a person, please type the number beside their name.");
            for (int i = 0; i < availableHelpers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {availableHelpers[i].Name} - Description: {availableHelpers[i].Skills} - Cost: {availableHelpers[i].Cost} credits"); //looks at list, and shows to player
            }

            Console.Write("Enter the number of the person you want to hire: ");
            int choice = int.Parse(Console.ReadLine()); //Need to convert to correct formatting.

            if (choice > 0 && choice <= availableHelpers.Count)
            {
                Helpers helper = availableHelpers[choice - 1];
                Console.WriteLine($"You have hired {helper.Name}, and they have joined your crew");
                Console.WriteLine($"{helper.Name} will now begin to do their job.");

                if (helper.Role == WorkerRole.Soldier)
                {
                    await helper.CompleteMissionAsync(helper, "Going off to catch a criminal in your name, and they will give you 60% of the reward.", 12)

                    Console.WriteLine($"They have succeeded! {helper.Name} gives you 300 credits.");
                    player.Credits += 300;
                }
                if (helper.Role == WorkerRole.Cook)
                {
                    await helper.CompleteMissionAsync(helper, "Cooking food for you and any others you have on your ship.", 5);

                    Console.WriteLine($"They have succeeded, and the ship smells like fresh cooked food! {helper.Name} has done a great job.");
                    player.Hunger = 10000000000000;
                }
                if (helper.Role == WorkerRole.Intern)
                {
                    Console.WriteLine("The intern plops down into a chair on your ship, waiting to head out to catch one of those bastardly criminals!");

                }
                if (helper.Role == WorkerRole.Engineer )
                {
                    await helper.CompleteMissionAsync(helper, "Fixing up your ship, upgrading it so it will never need that silly fuel ever again!", 30);

                    Console.WriteLine($"After quite some time, the tired but accomplished {helper.Name} reports that your ship is ready to go!");
                    player.PlayerShip.Fuel = 1000000000000000;
                }

            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }
    }


}
}