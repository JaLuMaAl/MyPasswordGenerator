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
        // array con los caracteres (letras mayusc y minysc + números) 
        private const string characters = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789";

        // CONSTRUCTORES
        public Generator() {}

        // MÉTODOS

        // Función que se llama desde fuera y permite generar una nueva contraseña       
        public string GeneratePassword(int numChar)
        {
            // Variable que guarda el índice correspondiente al caractér aleatorio dentro del string characters
            int charIndex;

            // Instancio objeto de la clase StringBuilder que permite concatenar strings de forma más eficiente
            StringBuilder stringBuild = new StringBuilder(numChar);            

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
