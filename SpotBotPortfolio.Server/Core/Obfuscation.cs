using System;
using System.Text;

namespace SpotBot.Server.Core
{
    public class Obfuscation
    {
        private static readonly string _key = "circumlocutious";

        public static string Obfuscate(string input)
        {
            string result = input;

            // Obfuscation Layer 1: Base64 encoding
            byte[] bytes = Encoding.UTF8.GetBytes(result);
            result = Convert.ToBase64String(bytes);

            // Obfuscation Layer 2: Reverse the string
            char[] charArray = result.ToCharArray();
            Array.Reverse(charArray);
            result = new string(charArray);

            // Obfuscation Layer 3: XOR with the custom key
            string xorKey = _key;
            result = Xor(result, xorKey);

            // Obfuscation Layer 4: Base64 encoding
            bytes = Encoding.UTF8.GetBytes(result);
            result = Convert.ToBase64String(bytes);

            // Obfuscation Layer 5: Replace some characters
            result = result.Replace("C", "#").Replace("B", "@").Replace("A", "!");

            return result;
        }

        public static string Deobfuscate(string input)
        {
            string result = input;

            // Reverse Layer 5: Replace some characters
            result = result.Replace("#", "C").Replace("@", "B").Replace("!", "A");

            // Reverse Layer 4: Base64 decoding
            byte[] decodedBytes = Convert.FromBase64String(result);
            result = Encoding.UTF8.GetString(decodedBytes);

            // Reverse Layer 3: XOR with the custom key
            string xorKey = _key;
            result = Xor(result, xorKey);

            // Reverse Layer 2: Reverse the string
            char[] charArray = result.ToCharArray();
            Array.Reverse(charArray);
            result = new string(charArray);

            // Reverse Layer 1: Base64 decoding
            decodedBytes = Convert.FromBase64String(result);
            result = Encoding.UTF8.GetString(decodedBytes);

            return result;
        }

        private static string Xor(string input, string key)
        {
            StringBuilder resultBuilder = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                char inputChar = input[i];
                char keyChar = key[i % key.Length];
                char xorChar = (char)(inputChar ^ keyChar);
                resultBuilder.Append(xorChar);
            }

            return resultBuilder.ToString();
        }
    }
}