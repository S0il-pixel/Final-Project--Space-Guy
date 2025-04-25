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
            try //Error handling
            {
                DateTime startT = DateTime.Now;
                Console.WriteLine("Welcome to the Space Bounty Hunter RPG game! You are at the main menu. To select an option, type in the letter in the brackets() beside the option.");
                Console.WriteLine(
                    "(N)New Game" +
                    "(Q)Quit" +
                    "(S)Settings");
                string PlayerChoice = Console.ReadLine();
                char choice = char.Parse(PlayerChoice);
                MenuChoices(choice);
                DateTime endT = DateTime.Now;
            }
            catch (Exception ex) { Console.WriteLine($"Error!: {ex.Message}"); }

        }

    }
}
