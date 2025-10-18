using System;
using System.Text;

namespace HomeWork
{
    internal class Program
    {
        // Задание 1
        public static string ConcatenateStrings(string str1, string str2)
        {
            return str1 + str2;
        }

        // Задание 2
        public static string GreetUser(string name, int age)
        {
            return $"Hello, {name}!\nYou are {age} years old.";
        }

        // Задание 3
        public static string GetStringInfo(string input)
        {
            return $"Количество символов: {input.Length}\nВ верхнем регистре: {input.ToUpper()}\nВ нижнем регистре: {input.ToLower()}";
        }

        // Задание 4
        public static string GetFirstFiveCharacters(string input)
        {
            if (input.Length >= 5)
                return input.Substring(0, 5);
            else
                return input;
        }

        // Задание 5
        public static StringBuilder BuildSentence(string[] words)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string word in words)
            {
                sb.Append(word);
                sb.Append(" ");
            }

            return sb;
        }

        // Задание 6
        public static string ReplaceWords(string inputString, string wordToReplace, string replacementWord)
        {
            return inputString.Replace(wordToReplace, replacementWord);
        }

        static void Main(string[] args)
        {
            Console.WriteLine(ConcatenateStrings("Hello ", "World"));
            Console.WriteLine(GreetUser("Alice", 25));
            Console.WriteLine(GetStringInfo("Hello World"));
            Console.WriteLine(GetFirstFiveCharacters("Hello World"));

            string[] words = { "This", "is", "a", "test" };
            Console.WriteLine(BuildSentence(words));

            string result = ReplaceWords("Hello world", "world", "universe");
            Console.WriteLine(result);
        }
    }
}
