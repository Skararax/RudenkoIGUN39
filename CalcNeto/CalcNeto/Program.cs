using System;

Console.WriteLine("Enter the first number:");
if (!int.TryParse(Console.ReadLine(), out int a))
{
    Console.WriteLine("Error! Invalid input for the first number.");
    return;
}

Console.WriteLine("Enter the second number:");
if (!int.TryParse(Console.ReadLine(), out int b))
{
    Console.WriteLine("Error! Invalid input for the second number.");
    return;
}

Console.WriteLine("Enter the operator (&, |, or ^):");
string operatorInput = Console.ReadLine();

if (operatorInput == null || (operatorInput != "&" && operatorInput != "|" && operatorInput != "^"))
{
    Console.WriteLine("Error! Invalid operator. Please use &, |, or ^.");
    return;
}

int result = 0;
switch (operatorInput)
{
    case "&":
        result = a & b;
        break;
    case "|":
        result = a | b;
        break;
    case "^":
        result = a ^ b;
        break;
}

Console.WriteLine($"Decimal result: {result}");
Console.WriteLine($"Binary result: {Convert.ToString(result, 2)}");
Console.WriteLine($"Hexadecimal result: {Convert.ToString(result, 16).ToUpper()}");