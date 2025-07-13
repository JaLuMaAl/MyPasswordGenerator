namespace MyPasswordGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Generator Generator = new Generator();
            MenuManager Menu = new MenuManager(Generator);

            // Guarda el número máximo de argumentos que se pueden recibir
            const int maxArgs = 4;

            bool friendlyUI = false;

            // Compruebo que se han recibido argumentos
            if (args.Length > 0)
            {
                // Compruebo primero la opción de -UIon como primer argumento. Si es correcto friendlyUI cambia de valor a true
                if ((args.Length == 1) && (args[0] == "-UIon"))
                {
                    friendlyUI = true;
                }
                // Si el primer arg es -UIon pero aún así se han introducido más args devuelvo mensaje de error
                else if (args[0] == "-UIon")
                {
                    Console.WriteLine("Invalid arguments. When -UIon selected no other argument can be recieve.");
                    return;
                }

                // friendlyUI = true -> muestro el menú correspondiente
                // friendlyUI = false -> compruebo los argumentos como parámetros del programa para mostrar el menú sin comentarios
                if (friendlyUI)
                {
                    Menu.UserFriendlyMenu();
                }
                else
                {
                    if (args.Length <= maxArgs)
                    {
                        int numChar = 0;
                        int numPassword = 0;
                        bool invalidValue = false;

                        // Itero sobre los argumentos
                        for (int i = 0; i < maxArgs; i++)
                        {
                            // Compruebo si se recibe un parámetro y si el valor de este es válido
                            switch (args[i])
                            {
                                // Parámetro --numChar del programa
                                case "--numChar":
                                    if (i + 1 < maxArgs)
                                    {
                                        bool parseNumChar = int.TryParse(args[i + 1], out int numCharValue);
                                        if (parseNumChar)
                                        {
                                            numChar = numCharValue;
                                        }
                                        else
                                        {
                                            Console.WriteLine("--numChar invalid value");
                                            invalidValue = true;
                                        }
                                    }
                                    i++;
                                    break;

                                // Parámetro --numPassword del programa
                                case "--numPassword":
                                    if (i + 1 < maxArgs)
                                    {
                                        bool parseNumPassword = int.TryParse(args[i + 1], out int numPasswordValue);
                                        if (parseNumPassword)
                                        {
                                            numPassword = numPasswordValue;
                                        }
                                        else
                                        {
                                            Console.WriteLine("--numPassword invalid value");
                                            invalidValue = true;
                                        }
                                    }
                                    i++;
                                    break;

                                default:
                                    invalidValue = true;
                                    break;
                            }
                        }

                        if (!invalidValue && (numChar > 0) && (numPassword > 0))
                        {
                            Menu.QuietMenu(numChar, numPassword);
                        }
                        else
                        {
                            Console.WriteLine("Error: Invalid argument or invalid value");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: too many arguments");
                    }
                }
            }
            else
            {
                Console.WriteLine("Error: Arguments must be received");
            }            

            Console.ReadLine();
        }
    }
}
