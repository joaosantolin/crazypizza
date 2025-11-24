using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace crazypizza
{
    // Classe responsável por gerar hashes em SHA-1
    public class Sha1Hasher
    {
        // Método estático que recebe uma string e retorna seu hash SHA-1 em hexadecimal

        public static string ComputeSha1Hash(string input)
        {
            // Cria uma instância do algoritmo SHA1
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                // Converte a string de entrada para um array de bytes
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);

                // StringBuilder é usado para montar a string hexadecimal
                byte[] hashBytes = sha1.ComputeHash(inputBytes);

                // Percorre cada byte do hash e converte para hexadecimal
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2")); // "x2" formata como 2 dígitos em hexadecimal
                }
                return sb.ToString();
            }
        }
    }
}