using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPasswordGenerator
{
    internal class MenuManager
    {
        private Generator PasswordGenerator;
        private int NumChar;
        private int NumPasswords;

        // CONSTRUCTORES
        public MenuManager(Generator passwordGen)
        {
            PasswordGenerator = passwordGen;
        }

        // MÉTODOS

        // Método que muestra como resultado de la llamada al programa únicamente las contraseñas generadas, sin comentarios extra
        public void QuietMenu(int numChar, int numPasswords)
        {
            NumChar = numChar;
            NumPasswords = numPasswords;

            string newPassword = "";

            for (int i = 0; i < NumPasswords; i++)
            {
                newPassword = PasswordGenerator.GenerateNewPassword(NumChar);
                Console.WriteLine(newPassword);
            }

            return;
        }

        // Opción que muestra como resultado de la llamada al programa un menú que incluye comentarios para el usuario
        public void UserFriendlyMenu()
        {
            Console.WriteLine("--- PASSWORD GENERATOR ---");
            Console.WriteLine("Welcome to MyPasswordGenerator by Marcos Alonso!");
            Console.WriteLine("Generate random passwords in seconds and keep your accounts safe");


            Console.WriteLine("Lets Generate a New Password");
            Console.Write("Number of characters of the password: ");
            // Realizo la conversión de la entrada obtenida a un int con TryParse y guardo el resultado en validNumChar
            bool validNumChar = int.TryParse(Console.ReadLine(), out int parseNumChar);

            // Compruebo que el número de caracteres introducido es válido: mayor que 0 y menor o igual a 128 (número máximo establecido)
            if (validNumChar && (parseNumChar > 0) && (parseNumChar <= 128))
            {
                NumChar = parseNumChar;

                Console.Write("Number of passwords you want to generate: ");

                // Empleo TryParse para verificar si el parse a int se ha realizado con éxito.
                // Obtengo un código más robusto ante los posibles valores introducidos por el usuario en ReadLine()
                // Si la conversión no se ha podido realizar TryParse() devuelve false con un valor de 0 en parseNumPasswords
                bool validNumPasswords = int.TryParse(Console.ReadLine(), out int parseNumPasswords);

                // Compruebo que el número de contraseñas a generar (parseNumPasswords) obtenido es válido
                if (validNumPasswords && (parseNumPasswords > 0) && (parseNumPasswords <= 128))
                {
                    NumPasswords = parseNumPasswords;
                    string newPassword = "";

                    Console.WriteLine($"Generating {NumPasswords} new random passwords of {NumChar} characters...");

                    for (int i = 0; i < NumPasswords; i++)
                    {
                        newPassword = PasswordGenerator.GenerateNewPassword(NumChar);
                        Console.WriteLine(newPassword);
                    }

                    Console.WriteLine("New Password/s successfully generated!");
                }
                else
                {
                    Console.WriteLine("Invalid value");
                }
            }
            else
            {
                Console.WriteLine("Invalid value");
            }
        }
    }
}
