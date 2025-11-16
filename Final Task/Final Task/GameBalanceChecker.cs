using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class GameBalanceChecker
{
    public GameProfil gameProfil;

    public GameBalanceChecker(GameProfil gameBalance) 
    {
        gameProfil = gameBalance;

        CheckBalance(gameBalance);
    }

    public bool CheckBalance(GameProfil balance) 
    {

        if (balance.creditBalance <= 0)
        {
            Console.WriteLine("No money? Kicked");
            return false;
        }
        if (balance.creditBalance <= 0)
        {
            Console.WriteLine("No money? Kicked");
            return false;
        }
        return true;
    }
}

