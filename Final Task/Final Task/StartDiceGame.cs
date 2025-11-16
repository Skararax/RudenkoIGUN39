using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class StartDiceGame
{
    public StartDiceGame(GameProfil gameProfil) 
    {
        CreateDiceGame(gameProfil);
    }

    private void CreateDiceGame(GameProfil gameProfil) 
    {
        Console.WriteLine("Введите количество кубиков");
        int diceCount = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите минимальное значение кубика");
        int min = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите максимальное значение кубика");
        int max = int.Parse(Console.ReadLine());

        TheDiceGame theDiceGame = new TheDiceGame(diceCount, min, max, gameProfil);
    }
}

