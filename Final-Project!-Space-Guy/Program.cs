namespace Program
{
    public class program
    {
        public static void MenuChoices(string choice)
        {
            switch (choice)
            {
                case 'O':
                    //If no saves, this returns an error. If there is saves, open save menu, and let player select one.
                    Console.WriteLine("Opperational.");
                    break;
                case 'N':
                    //This creates a new, empty character. It lets the player make the name, and appearance. It also shows what you have.
                    Console.WriteLine("Normal");
                    break;
                case 'Q':
                    //This will quit the game and close the program.
                    Console.WriteLine("Quite well");
                    break;
                case 'S':
                    //They can change the game to baby mode, normal mode, or expert mode. 
                    Console.WriteLine("Snapping");
                    break;
            }
    }
}