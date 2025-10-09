using System;

public class StringList
{
    List<String> list = new List<string>() { "Apple", "Ice", "Sword" };

    public void UserWord()
    {
        Console.WriteLine("Enter your word");
        string word = Console.ReadLine();
        list.Add(word);

        foreach (string item in list)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("Enter another word to add to the middle");
        string secondWord = Console.ReadLine();

        int middleIndex = list.Count / 2;
        list.Insert(middleIndex, secondWord);

        foreach (string item in list)
        {
            Console.WriteLine(item);
        }
    }
}

public class Note 
{
    private Dictionary<string, int> studentsDict = new Dictionary<string, int>();

    string _name;

    private void NoteLogic() 
    {
        Console.WriteLine("Введите информацию о студентах. Для завершения ввода введите 'exit'");

        while (true) 
        {
            Console.WriteLine("Введите имя студента:");
            _name = Console.ReadLine();

            if (_name == "exit") 
            { 
                break;
            }

            Console.WriteLine("Введите имя оценку:");
            string gradeInput = Console.ReadLine();

            if (int.Parse(gradeInput) < 2 || int.Parse(gradeInput) > 5)
            {
                Console.WriteLine("Ошибка: оценка должна быть от 2 до 5!");
                continue;
            }

            studentsDict[_name] = int.Parse(gradeInput);
        }

        Console.WriteLine("Поиск студента. Введите имя для поиска:");
        string searchStudent = Console.ReadLine();

        if (studentsDict.ContainsKey(searchStudent)) 
        {
            Console.WriteLine($"Студент {searchStudent} найден. Оценка: {studentsDict[searchStudent]}");
        }
        else
        {
            Console.WriteLine($"Студент с именем {searchStudent} не существует.");
        }
    }
}

namespace HomeWork
{
    internal class Program
    {
        private class ListTask
        {
            public void TaskLoop()
            {
                List<int> numbers = new List<int>();

                Console.WriteLine("Введите 3-6 чисел:");

                for (int i = 0; i < 6; i++)
                {
                    Console.Write($"Число {i + 1}: ");
                    int num = int.Parse(Console.ReadLine());
                    numbers.Add(num);

                    if (i >= 2 && i < 5)
                    {
                        Console.Write("Добавить еще? n - нет / y - да ");
                        if (Console.ReadLine().ToLower() != "y")
                            break;
                    }
                }

                Console.Write("Прямой порядок: ");
                foreach (int num in numbers)
                {
                    Console.Write(num + " ");
                }

                Console.Write("Обратный порядок: ");
                for (int i = numbers.Count - 1; i >= 0; i--)
                {
                    Console.Write(numbers[i] + " ");
                }
            }
        }

        static void Main(string[] args)
        {
            var task = new ListTask();
            task.TaskLoop();
        }
    }
}

