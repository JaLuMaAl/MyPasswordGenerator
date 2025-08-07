using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace MyPasswordGenerator
{
    internal class Generator
    {
        // Lista que contiene contraseñas comunes
        private List<string> WeakPasswords;

        // CONSTRUCTORES
        public Generator()
        {
            WeakPasswords = new List<string>();
        }

        // MÉTODOS

        // Función que se llama desde fuera y permite generar una nueva contraseña
        public string NewPassword(int numChar)
        {
            // Genero nueva contraseña
            string generatedPwd = GeneratePassword(numChar);

            // Leo el archivo con las contraseñas comunes
            // Variable con la ruta del archivo con las contraseñas comunes
            string comPwdFile = "";
            // Leo y cargo el contenido del archivo en WeakPasswords
            int readCommonPwd = ReadCommonPasswordsFile(comPwdFile);

            // Compruebo que la contraseña no coincide con contraseñas comunes
            bool checkCommonPassword = false;

            do
            {
                // Compruebo que la contraseña generada no es común
                checkCommonPassword = IsCommonPassword(generatedPwd);

                // Si es común, la contraseña se genera de nuevo
                if (checkCommonPassword)
                    generatedPwd = GeneratePassword(numChar);
            }
            while (checkCommonPassword);

            // Devuelvo contraseña final
            return generatedPwd;
        }

        private string GeneratePassword(int numChar)
        {
            // Variable que guarda el índice correspondiente al caractér aleatorio dentro del string characters
            int charIndex;

            // Instancio objeto de la clase StringBuilder que permite concatenar strings de forma más eficiente
            StringBuilder stringBuild = new StringBuilder(numChar);

            // Creo string con el abecedario en mayusc y minusc + los números del 0 al 9
            string characters = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789";


            // Se obtienen y concatenan los caracteres aleatorios que componen la password
            for (int i = 0; i < numChar; i++)
            {
                // Empleo una función criptográfica para generar un número aleatorio de forma segura
                charIndex = RandomNumberGenerator.GetInt32(characters.Length);
                stringBuild.Append(characters[charIndex]);
            }

            string password = stringBuild.ToString();
            return password;
        }

        // Función que comprueba que la contraseña generada no coincida con contraseñas comunes
        private bool IsCommonPassword(string passwordReceived)
        {
            // Compruebo si la contraseña recibida coindice con alguna contraseña de la lista de contraseñas débiles
            foreach (string pwd in WeakPasswords)
            {
                if (passwordReceived == pwd)
                {
                    // Contraseñas coinciden, devuelvo true
                    // No necesito seguir comprobando, con return evito comprobar innecesariamente el resto de la lista, mejora la eficiencia
                    return true;
                }
            }
            return false;
        }

        // Función que recibe una ubicación de archivo con contraseñas comunes, lo lee y las almacena en la lista WeakPasswords
        private int ReadCommonPasswordsFile(string rutaPwdFile)
        {
            // Variable que almacena 0 si la función ha finalizado sin errores y 1 si ha ocurrido algún error
            int readCompleted = 0;

            try
            {
                // Compruebo que el archivo existe
                if (File.Exists(rutaPwdFile))
                {
                    // Si el archivo existe lo abro en modo lectura
                    using (StreamReader sr = new StreamReader(rutaPwdFile))
                    {
                        string linea;

                        // Cada línea es una contraseña, recorro todas las líneas añadiendolas a la lista 
                        while ((linea = sr.ReadLine()) != null)
                        {
                            WeakPasswords.Add(linea);
                        }
                    }

                    // Lectura finalilzada sin errores
                    readCompleted = 0;
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                // Error producido
                readCompleted = 1;
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                // Error producido
                readCompleted = 1;
            }

            return readCompleted;
        }
    }
}
