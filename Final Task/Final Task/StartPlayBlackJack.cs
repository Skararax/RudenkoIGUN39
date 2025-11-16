using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class StartPlayBlackJack
{
    public StartPlayBlackJack(GameProfil gameProfil) 
    {
        CreateBlackJack(gameProfil);
    }

    public void CreateBlackJack(GameProfil gameProfil) 
    {
        Console.WriteLine("Введите число карт (не меньше 10 карт) для игры в БлэкДжек");
        int cardCounter = int.Parse(Console.ReadLine());

        Blackjack blackjack = new Blackjack(cardCounter, gameProfil);
    }


}

