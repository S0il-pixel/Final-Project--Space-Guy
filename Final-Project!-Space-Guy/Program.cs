using Final_Project__Space_Guy;

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
                case 'T':
                    //Idk, just an easter egg.
                    Console.WriteLine("It's me, Tenna!");
                    break;
            }
        }

        private static void CheckForSaves() //Just checking if there are saves available. Saves are in a 
        {

        }

        //public static void NewCharacter(string name, string description)
        //{
        //    UserCharacter NewCharacter = new UserCharacter
        //    {
        //        Name = name
        //        Description = description
        //    }

        //    string filePath = "myObject.json";
        //    JsonFileHandler.SaveObjectToFile(myObject, filePath);
        //}

        //public static void SaveObjectToFile<T>(T obj, string filePath)
        //{
        //    try
        //    {
        //        string jsonString = JsonConvert.SerializeObject(obj, Formatting.Indented);
        //        File.WriteAllText(filePath, jsonString);
        //        Console.WriteLine("Object saved successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"An error occurred: {ex.Message}");
        //    }
        //}
    }
}