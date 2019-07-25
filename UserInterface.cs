using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatingSite.Demo
{
    class UserInterface
    {

        public void Header(string text)
        {
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text.ToUpper());
            Console.WriteLine();
            Console.ResetColor();
        }

        public void WriteLine(string text = "", ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public void Write(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        public void WriteLogo(int length = 25, int height = 12)
        {
            Header("Welcome to Casanova Kim");
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (i == 0)
                    {
                        if (j == 0 || j == height
                                || j == length - 1)
                        {
                            Console.Write("*");
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }

                    else if (i == height - 1)
                    {
                        Console.Write("*");
                    }

                    else if ((j < i || j > height - i) &&
                                    (j < height + i ||
                                    j >= length - i))
                        Console.Write("#");
                    else
                        Console.Write(" ");
                }

                Console.WriteLine();
            }

        }
        internal int GetNumericInput(string prompt)
        {
            Write(prompt, ConsoleColor.Green);

            while (true)
            {
                var input = Console.ReadLine().Trim();
                try
                {
                    int inputNumeric = int.Parse(input);
                    return inputNumeric;
                }
                catch (FormatException)
                {
                    Write("\nPlease enter a whole number ", ConsoleColor.Red);
                }
            }
        }


        internal string GetSQLValidString(string prompt, int maxLength = 49)
        {
            Write(prompt, ConsoleColor.Green);
            while (true)
            {
                string newTitle = NormalCapitalization(Console.ReadLine().Trim());

                if (newTitle.Contains('\''))
                    Write("It should not contain får \' ", ConsoleColor.Red);
                else if (newTitle.Length > maxLength)
                    Write($"It should not contain mora than {maxLength} letter ", ConsoleColor.Red);
                else
                {
                    return newTitle;
                }
            }
        }

        public static string NormalCapitalization(string input)
        {
            if (String.IsNullOrEmpty(input))
                return "";
            return input.First().ToString().ToUpper() + input.Substring(1).ToLower();
        }
    }
}
