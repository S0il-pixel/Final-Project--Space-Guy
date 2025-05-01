using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
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
                { "planets in your area", PlanetExploration},
                { "back to menu", MenuChoices},
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
            Dictionary<string, int> shopItems = new Dictionary<string, int> //Another use of dictionary
                        {
                            { "fuel refill", 50 },
                            { "food refill", 30 },
                            { "lazer gun", new LazerGun() },
                            { "crowbar", new Crowbar() },
                            { "lazer riffle", new LazerRiffle() },
                            { "electric dagger", new ElectricDagger() },
                            { "saber of light", new SaberOfLight() },
                            { "club", new Club() }
                        };

            Console.WriteLine("Welcome to the shop! Items available:");
            foreach (var item in shopItems)
            {
                Console.WriteLine($"{item.Key}: {item.Value} credits");
                Console.WriteLine($"{item.Value.Name}: {item.Value.Cost} credits - {item.Value.Description}");
            }

            Console.Write("What would you like to buy? ");
            string itemChoice = Console.ReadLine()?.ToLower();

            if (shopItems.ContainsKey(itemChoice) && player.Credits >= shopItems[itemChoice])
            {
                player.Credits -= shopItems[itemChoice];
                if (itemChoice == "fuel refill")
                {
                    int refillAmount = 50; // Fuel refill amount
                    int availableSpace = 100 - player.PlayerShip.Fuel; // Max fuel capacity is 100
                    int refillableAmount = Math.Min(refillAmount, availableSpace); // Prevent overfilling

                    player.PlayerShip.Fuel += refillableAmount;
                    player.Credits -= shopItems[itemChoice];
                    Console.WriteLine($"Fuel refilled! Remaining credits: {player.Credits}. Current Fuel: {player.PlayerShip.Fuel}/100");
                }
                else if (itemChoice == "food refill")
                {
                    int refillAmount = 30; // Food refill amount
                    int availableSpace = 100 - player.Hunger; // Max hunger capacity is 100
                    int refillableAmount = Math.Min(refillAmount, availableSpace); // Prevent overfilling

                    player.Hunger += refillableAmount;
                    player.Credits -= shopItems[itemChoice];
                    Console.WriteLine($"Food refilled! Remaining credits: {player.Credits}. Current Hunger: {player.Hunger}/100");
                }
                Tools selectedTool = shopItems[itemChoice];
                if (player.Credits >= selectedTool.Cost)
                {
                    player.Credits -= selectedTool.Cost;
                    player.Gear.Add(selectedTool); // Add tool to player's gear
                    Console.WriteLine($"{selectedTool.Name} purchased! Remaining credits: {player.Credits}");
                }
                else
                {
                    Console.WriteLine("Not enough credits to buy this item.");
                }
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
                            new Criminal("Mafie guy: Al Capone!", 2000, 3),
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
                            new Criminal("Brutus Bloodfang", 1400, 3),
                        };

            Console.WriteLine("Wanted Board: ");

            availableCriminals.ForEach(c => Console.WriteLine($"Name: {Criminal.Name}, Bounty: {Criminal.Bounty}, Difficulty: {Criminal.Difficulty}"));

            Console.WriteLine("Choose the difficulty you want to hunt. 1, 2, 3 :");
            string difficultyLevel = Console.ReadLine();
            int difficulty = int.Parse(difficultyLevel);

            var filteredCriminals = criminals.Where(Criminals => Criminals.Difficulty == difficulty).ToList();

            Console.WriteLine($"Criminals with difficulty {Criminal.Difficulty}:");
            filteredCriminals.ForEach(c => Console.WriteLine($"Name: {Criminal.Name}, Bounty: {Criminal.Bounty}, Difficulty: {Criminal.Difficulty}"));

            Console.Write("Enter the name of the criminal you want to hunt: ");
            string choice = Console.ReadLine();

            var target = availableCriminals.FirstOrDefault(c => c.Name.Equals(choice, StringComparison.OrdinalIgnoreCase));
            if (choice == availableCriminals[Criminal.Name])
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
                        Game("The criminal runs, and jumps over a barel.");
                            
                        break;
                    }
                case 2:
                    {
                        //mini game 2
                        Game("The criminal tries to shoot you!");
                           
                        break;
                    }
                case 3:
                    {
                        //mini game 3
                        Game("You find your target, and throw a rock.");
                            
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

        public void Game(PlayerCharacter player, Criminal target, string sentenceToType, int baseTimeLimit)
        {
            if (player.Gear.Any(g => g.Name == "Saber of Light"))
            {
                // Automatically win the fight
                Console.WriteLine("You ignite the Saber of Light, its radiant blade illuminating the area.");
                Console.WriteLine($"The moment {target.Name} sees it, they drop their weapon, frozen in fear.");
                Console.WriteLine("Without hesitation, they surrender, and you cuff them with ease.");
                Console.WriteLine($"You have caught {target.Name}! You earn {target.Bounty} credits.");

                // Add the criminal to the player's captured list and increase credits
                player.CapturedCriminals.Add(target);
                player.Credits += target.Bounty;
                return; // Exit the method since the fight is resolved
            }

            Console.WriteLine($"Your target is {target.Name}, with a bounty of {target.Bounty} credits and a difficulty level of {target.Difficulty}.");
            Console.WriteLine("Your sentence to type is:");
            Console.WriteLine(sentenceToType);
            Console.WriteLine("Ready? Go!");

            DateTime startTime = DateTime.Now;
            string userInput = Console.ReadLine();
            DateTime endTime = DateTime.Now;

            TimeSpan timeTaken = endTime - startTime;
            int totalTimeLimit = baseTimeLimit;

            // Apply tools
            foreach (var tool in player.Gear)
            {
                if (tool.UseTool()) // Decrease tool uses
                {
                    totalTimeLimit += GetToolTimeBonus(tool); // Add time bonus
                    Console.WriteLine($"{tool.Name} used! Time limit increased by {GetToolTimeBonus(tool)} seconds. Remaining uses: {tool.UsesLeft}");
                }
            }

            // Determine fight outcome
            bool isSuccessful = (timeTaken.TotalSeconds <= totalTimeLimit && userInput == sentenceToType);

            if (isSuccessful)
            {
                Console.WriteLine($"Great job! You typed the sentence correctly in {timeTaken.TotalSeconds:F2} seconds. You have caught the criminal!");
                DisplaySuccessStory(player, target); // Call success story method
            }
            else
            {
                Console.WriteLine($"Oops! You didn't type the sentence correctly, or you ran out of time. The criminal escaped.");
                DisplayFailureStory(player, target); // Call failure story method
            }
        }

        private void DisplaySuccessStory(PlayerCharacter player, Criminal target)
        {
            if (player.Gear.Any(g => g.Name == "Lazer Gun"))
            {
                Console.WriteLine("You chase the criminal as they wind through the alleyways, over barels and rubble, and up a ladder. You follow suit, snapping at their heals. You persue them over the roof tops, " +
                                "leaping over gaps as they desperately attempt to escape you. But, as they glance back to check how close they are, they don't notice the clothes line. They run right into it, getting snagged " +
                                "and wrapped up in sheets! They hit the ground, the air being knocked out of them, trapped in the household item. You laugh, taking advantage of the moment to cuff them. The criminal groans, " +
                                "knowing they've met their match. You take them back to your ship with a triumphant grin. You didn't even need to use your blaster, after all.");
            }
            else if (player.Gear.Any(g => g.Name == "Crowbar"))
            {
                Console.WriteLine("The criminal, armed with a lzer blaster, attempts to shoot you. You duck behind a crumbling cement wall, dust flying past you as lazers just barely miss you. You scowl, flinching " +
                               "as another blast skids off the side of the rubble. You glance around, in doing so you spot a rock. With your own blaster being broken, it may be your best bet. You grab the rock, clutching it in " +
                               "your gloved fists. You wait for your moment to strike, and you find it when an ugly rat runs out. The criminal shoots at the movement, making the rat squeak and run around. You take your chance, " +
                               " standing and throwing the rock. You hear a faint 'ouch!', then a thud. You got them! You walk over, crowbar in hand. You really thought it would be more useful, but hey, it worked out.");
            }
            else if (player.Gear.Any(g => g.Name == "Lazer Riffle"))
            {
                Console.WriteLine("The criminal wakes up with a splutter, kicking their feet and adjusting their hat. You laugh, scratching your head. The criminal looks up at you, a look of shock on their face. " +
                                "They have quickly realized, they've been caught. With a sigh, they offer up their wrists, and you cuff them. Sometimes, this feels like a pretty easy job. No riffle needed, hah!");
            }
            else if (player.Gear.Any(g => g.Name == "Electric Charged Dagger"))
            {
                Console.WriteLine("The criminal was waiting for you, a mask over their face, and a savage looking dagger grasped in their fist. A look in their tells you that they have a thirst for blood, specifically " +
                                "your blood. With a gasp you again duck to the side, avoiding a violent slash aimed at your torso. You slash at them with your own dagger, which sparks with angry energy. " +
                                "The criminal whips around, knicking your arm with the savage blade. Beads of blood well up " +
                                "out of the gash, staining your clothes. The criminal laughs like a wild animal, attacking again. But you're ready this time, whipping asside, grabbing their arm as you do. Twisting it, the criminal " +
                                "yelps. The dagger forced out of their grasp, their arm straining. You force them to their knees, struggling to cuff them as they fight against you every step of the way.");
            }
            else if (player.Gear.Any(g => g.Name == "Club"))
            {
                Console.WriteLine("You let in a sharp inhale as you feel the cold muzzle of a blaster pressed against your back. The criminal, in a gruff voice, orders you to raise your hands. You slowly do as ordered, " +
                                "taking slow deep breaths as you feel the hands of the criminal snatching anything valuable off of you as they can, including your club. You glance back, your eyes flicking over their features. Their hand on the blaster " +
                                "is steady, but their eyes are distracted. Just the opportunity you needed. You grab the blaster, and twist it around. The criminal yelps in surprise, their finger pulling the trigger. Luckily for the " +
                                "both of you, the lazer shoots into a wall. You both wrestle for the blaster, and you end up the winner. Out of breath, you point the blaster at the criminal. Turning the tides. They growl, but slowly " +
                                "raise their hands. You have won!");
            }
            else
            {
                Console.WriteLine($"After a thrilling chase, you manage to apprehend {target.Name}, claiming their bounty of {target.Bounty} credits.");
            }
        }

        private void DisplayFailureStory(PlayerCharacter player, Criminal target)
        {
            if (player.Gear.Any(g => g.Name == "Lazer Gun"))
            {
                Console.WriteLine($"You shoot at the criminal, but they are somehow able to guess your every move. With a grunt of frustration, you lunge after them. Only for them to laugh, and shoot you in the knee.");
            }
            else if (player.Gear.Any(g => g.Name == "Crowbar"))
            {
                Console.WriteLine($"You wildly swing your crowbar, but you are outnumbered. You feel your crowbar come in contact with one of them, but just as it does, another criminal knocks you out.");
            }
            else if (player.Gear.Any(g => g.Name == "Lazer Riffle"))
            {
                Console.WriteLine("You hold the riffle to the back of the criminal. But int he blink of an eye, the table turns. The criminal grabs the riffle and turns it on you.");
            }
            else if (player.Gear.Any(g => g.Name == "Electric Charged Dagger"))
            {
                Console.WriteLine("You swing the dagger, clashing with the enemies dagger. They yelp as they are shocked, but as they do, you feel cold metal stab into your side. You forgot about their partner.");
            }
            else if (player.Gear.Any(g => g.Name == "Club"))
            {
                Console.WriteLine("The club is knocked out of your hands, sending it flying. Without your weapon, up against the enemy with a knife, you're forced to retreat.");
            }
    else
            {
                Console.WriteLine($"{target.Name} proves too clever and eludes your capture. Better luck next time!");
            }
        }

        private int GetToolTimeBonus(Tools tool)
        {
            return tool switch
            {
                LazerGun => 5,
                Crowbar => 10,
                LazerRiffle => 25,
                ElectricDagger => 15,
                Club => 30,
                _ => 0 // Default case for tools without time bonuses
            };
        }

        public async Task Guilds(PlayerCharacter player)
        {
            player.UpdateHungerAndFuel();

            List<Helpers> availableHelpers = new List<Helpers>
    {
        new Helpers("Shane", "Will go off and collect criminals. Will give you 60% of the credits they earn.", WorkerRole.Soldier, 3000),
        new Helpers("Amanda", "Will make you food, so that you don't go hungry while out in space.", WorkerRole.Cook, 5000),
        new Helpers("Angela", "Will help you catch criminals, expanding the time limit for the mini game.", WorkerRole.Intern, 6000),
        new Helpers("Keith", "Will add solar and nuclear power to your ship, so you don't need fuel anymore.", WorkerRole.Engineer, 8000)
    };

            Console.WriteLine("To hire a person, please type the number beside their name.");
            for (int i = 0; i < availableHelpers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {availableHelpers[i].Name} - Role: {availableHelpers[i].Role} - Description: {availableHelpers[i].Skills} - Cost: {availableHelpers[i].Cost} credits");
            }

            Console.Write("Enter the number of the person you want to hire: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int choice) && choice > 0 && choice <= availableHelpers.Count)
            {
                Helpers helper = availableHelpers[choice - 1];
                Console.WriteLine($"You have hired {helper.Name}, and they have joined your crew.");
                player.Helpers.Add(helper);

                switch (helper.Role)
                {
                    case WorkerRole.Soldier:
                        await helper.CompleteMissionAsync(helper, "Going off to catch a criminal in your name, and they will give you 60% of the reward.", 12);
                        Console.WriteLine($"They have succeeded! {helper.Name} gives you 300 credits.");
                        player.Credits += 300;
                        break;

                    case WorkerRole.Cook:
                        await helper.CompleteMissionAsync(helper, "Cooking food for you and any others you have on your ship.", 5);
                        Console.WriteLine($"They have succeeded, and the ship smells like fresh cooked food! {helper.Name} has done a great job.");
                        player.Hunger = int.MaxValue;
                        break;

                    case WorkerRole.Intern:
                        Console.WriteLine("The intern plops down into a chair on your ship, waiting to head out to catch one of those bastardly criminals!");
                        break;

                    case WorkerRole.Engineer:
                        await helper.CompleteMissionAsync(helper, "Fixing up your ship, upgrading it so it will never need that silly fuel ever again!", 30);
                        Console.WriteLine($"After quite some time, the tired but accomplished {helper.Name} reports that your ship is ready to go!");
                        player.PlayerShip.Fuel = int.MaxValue;
                        break;

                    default:
                        Console.WriteLine($"Unknown role: {helper.Role}. No task assigned.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }

        public void PlanetExploration(PlayerCharacter player)
        {
            Console.WriteLine("Here are some of the planets in your current galaxy:");
            DisplayPlanets();

        }

    }

}