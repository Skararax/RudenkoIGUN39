using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class GameProfil
{
    public string Name;

    public int creditBalance;

    public GameProfil(string name)
    {
        Name = name;

        creditBalance = 500;

        HelloUser();
    }

    private void HelloUser()
    {
        Console.WriteLine($"Приветсвуем вас в нашем казино {Name}! Ваш начальный баланс 500 кредитов!");
    }

    public void ShowBalance()
    {
        Console.WriteLine($" Ваш баланс сотавляет {creditBalance} кредитов!");
    }

    public string GetSaveData()
    {
        return $"{Name}|{creditBalance}";
    }

    public static GameProfil FromSaveData(string saveData)
    {
        string[] parts = saveData.Split('|');
        return new GameProfil(parts[0])
        {
            creditBalance = int.Parse(parts[1]),
        };

    }
}

