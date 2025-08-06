using System.CommandLine;
using System.CommandLine.Parsing;

namespace MyPasswordGenerator
{
    internal class Program
    {
        static int Main(string[] args)
        {
            Generator Generator = new Generator();
            MenuManager Menu = new MenuManager(Generator);

            // Instancio el root command (comando raíz)
            RootCommand rootCommand = new("Random Secure Password Generator CLI tool");

            // Creo quietCommand (quiet) y lo añado como subcomando de rootCommand 
            Command quietCommand = new("quiet", "The program output will be only the generated passwords");
            rootCommand.Subcommands.Add(quietCommand);
            // Creo guidedCommand (guided) y lo añado como subcomando a rootCommand
            Command guidedCommand = new("guided", "User friendly mode, guide the user throughout the process");
            rootCommand.Subcommands.Add(guidedCommand);

            /* Al estar quietCommand y guidedCommand en el mismo nivel de jerarquía (ambos son "hijos" directos de rootCommand)
            estos comandos son mutuamente excluyentes, no pueden ser introducidos en la misma llamada */

            // Creación de Option --numChar que permite personalizar el número de caracteres de la contraseña generada
            Option<int> numCharOption = new("--numChar", "-c")
            {
                Description = "Number of characters of the generated password",
                // Longitud predeterminada de las contraseñas = 8 caracteres
                DefaultValueFactory = parseResult => 8
            };
            // Se incluye numChar como option de quietCommand
            quietCommand.Options.Add(numCharOption);

            // Creación de Option --numPasswords que permite personalizar la cantidad de contraseñas que genera el programa
            Option<int> numPasswordsOption = new("--numPasswords", "-p")
            {
                Description = "Number of passwords the program will generate",
                // Número predeterminado de contraseñas generadas = 1
                DefaultValueFactory = parseResult => 1
            };
            // Se incluye numPasswords como option de quietCommand
            quietCommand.Options.Add(numPasswordsOption);

            // Acción del subcomando quietCommand
            quietCommand.SetAction(parseResult => LaunchQuietMenu(parseResult.GetValue(numCharOption),parseResult.GetValue(numPasswordsOption),Menu));

            // Acción del subcomando guidedCommand
            guidedCommand.SetAction(parseResult => LaunchGuidedMenu(Menu));

            return rootCommand.Parse(args).Invoke();
        }

        // Función que lanza el programa en modo quiet (muestra solo contraseñas generadas)
        static void LaunchQuietMenu(int numChar, int numPasswords, MenuManager menuMan)
        {
            menuMan.QuietMenu(numChar,numPasswords);
            return;
        }

        // Función que lanza el programa en modo guided (user friendly con texto)
        static void LaunchGuidedMenu(MenuManager menuMan)
        {
            menuMan.GuidedMenu();
        }
    }
}
