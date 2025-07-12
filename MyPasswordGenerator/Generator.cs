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
        // CONSTRUCTORES
        public Generator() { }

        // MÉTODOS
        public string GenerateNewPassword(int numChar)
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
    }
}
