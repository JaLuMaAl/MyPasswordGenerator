namespace MyPasswordGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Generator Generator = new Generator();
            MenuManager Menu = new MenuManager(Generator);

            if ((args.Length == 1) && (args[0] == "-UIon"))
            {
                Menu.UserFriendlyMenu();

            }
            else if ((args.Length == 2) && (args[0].StartsWith("-numChar:")) && (args[1].StartsWith("-numPassword:")))
            {
                int numChar = int.Parse(args[0].Split(':')[1]);
                int numPasswords = int.Parse(args[1].Split(':')[1]);

                Menu.QuietMenu(numChar, numPasswords);
            }
            else
            {
                Console.WriteLine("Invalid arguments");
            }

            Console.ReadLine();

        }
    }
}
