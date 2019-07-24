using System;
using System.Collections.Generic;
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

        public void Write(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);
        }

        internal int GetNumericInput(string prompt)
        {
            Write(prompt);

            while (true)
            {
                var input = Console.ReadLine();
                try
                {
                    int inputNumeric = int.Parse(input);
                    return inputNumeric;
                }
                catch (FormatException)
                {
                    Write("\nVänligen mata in en siffra: ");
                }
            }
        }

        internal string GetSQLValidString(string prompt, int maxLength = 49)
        {
            Write(prompt);
            while (true)
            {
                string newTitle = Console.ReadLine();

                if (newTitle.Contains('\''))
                    Write("Titel får ej innehålla \' ");
                else if (newTitle.Length > maxLength)
                    Write($"Titel får inte vara längre än {maxLength} tecken ");
                else
                {
                    return newTitle;
                }
            }
        }
    }
}
