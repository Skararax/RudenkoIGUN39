using System;

// Задание 1: Числа Фибоначчи
Console.WriteLine("Задание 1: Первые 10 чисел Фибоначчи");
int fib1 = 0, fib2 = 1;

for (int i = 0; i < 10; i++)
{
    Console.WriteLine($"F({i}) = {fib1}");
    int nextFib = fib1 + fib2;
    fib1 = fib2;
    fib2 = nextFib;
}

// Задание 2: Четные числа
Console.WriteLine("\nЗадание 2: Четные числа от 2 до 20");

for (int even = 2; even <= 20; even += 2)
{
    Console.WriteLine(even);
}

// Задание 3: Таблица умножения
Console.WriteLine("\nЗадание 3: Таблица умножения от 1 до 5");

for (int row = 1; row <= 5; row++)
{
    for (int col = 1; col <= 5; col++)
    {
        Console.Write($"{row} × {col} = {row * col}\t");
    }
    Console.WriteLine();
}

// Задание 4: Проверка пароля
Console.WriteLine("Задание 4: Система проверки пароля");
string correctPassword = "qwerty";
string userPassword;

do
{
    Console.Write("Введите пароль: ");
    userPassword = Console.ReadLine();

    if (userPassword != correctPassword)
    {
        Console.WriteLine("Неверный пароль! Попробуйте снова.");
    }

} while (userPassword != correctPassword);

Console.WriteLine("Пароль верный! Доступ разрешен.");