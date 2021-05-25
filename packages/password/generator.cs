using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Spectre.Console;

namespace GeneratePasswords
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                //link to the code: https://codereview.stackexchange.com/a/5994
                //We do not own this code!
                int length = 12;
                Random random = new Random();
                string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                StringBuilder result = new StringBuilder(length);
                for (int i = 0; i < length; i++)
                {
                    result.Append(characters[random.Next(characters.Length)]);
                }
                Console.WriteLine(result.ToString());
                Thread.Sleep(120);
            }
        }
    }
}
