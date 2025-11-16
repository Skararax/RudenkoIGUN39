using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public abstract class CasinoGameBase
{
    public event Action<string> OnWin;
    public event Action<string> OnLoose;
    public event Action<string> OnDraw;
    public GameProfil GameProfil;

    protected string _gameName;

    private int _drawRisk;
    private int _winRisk;

    public CasinoGameBase(string GameName, GameProfil gameProfil) 
    {
        if (GameName == null) 
        {
            Console.WriteLine("Укажите название игры!");
        }

        GameProfil = gameProfil;
        _gameName = GameName;
        PlaceBet();
    }

    public abstract void PlayGame();
    protected abstract void FactoryMethod();

    protected void OnWinInvoke() 
    {
        OnWin?.Invoke("Выйгрыш!");
        GameProfil.creditBalance += _winRisk;
        _winRisk = 0;

        GameProfil.ShowBalance();
    }

    protected void OnLooseInvoke() 
    {
        OnLoose?.Invoke("Проигрыш!");
        _winRisk = 0;

        GameProfil.ShowBalance();
    }

    protected void OnDrawInvoke() 
    {
        OnDraw?.Invoke("Ничья!");
        GameProfil.creditBalance += _drawRisk;
        _winRisk = 0;

        GameProfil.ShowBalance();
    }

    public void DisplayGameResult(string result) 
    {
        Console.WriteLine($"Ваши результаты: {result}");
    }

    public void PlaceBet() 
    {
        Console.WriteLine($"Ваш баланс = {GameProfil.creditBalance} \n Сделать ставку 1 - 2 -?");
        string playerAnswer = Console.ReadLine();

        if (playerAnswer == "1") 
        {
            BetLogic(GameProfil.creditBalance);
        }
        if (playerAnswer == "2")
        {
            Console.WriteLine("Играем без ставок");
        }
        else 
        { 
            Console.WriteLine("НЕПРАВИЛЬНЫЙ ВВОД!");
        }
    }

    private void BetLogic(int credit) 
    {
        Console.WriteLine ("Введите сумму которую хотите поставить!");
        
        int risk = int.Parse(Console.ReadLine());

        if (risk > credit)
        {
            Console.WriteLine("НЕПРАВИЛЬНЫЙ ВВОД!");
        }
        else 
        {
            GameProfil.creditBalance -= risk;
            _winRisk = risk * 2;
            _drawRisk = risk;
        }
    }
}

