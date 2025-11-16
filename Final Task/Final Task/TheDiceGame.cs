using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;



internal sealed class TheDiceGame : CasinoGameBase
{
    private List<Dice> _playerDices;
    private List<Dice> _computerDices;
    private int _diceCount;
    private int _minValue;
    private int _maxValue;
    private Random _random;

    public TheDiceGame(int diceCount, int minValue, int maxValue, GameProfil gameProfil) : base("Игра в кости", gameProfil)
    {
        if (diceCount <= 0)
            throw new ArgumentException("Количество костей должно быть больше 0", nameof(diceCount));

        if (minValue >= maxValue)
            throw new ArgumentException("Минимальное значение должно быть меньше максимального", nameof(minValue));

        _diceCount = diceCount;
        _minValue = minValue;
        _maxValue = maxValue;
        _random = new Random();
        _playerDices = new List<Dice>();
        _computerDices = new List<Dice>();

        SubscribeToEvents();
        FactoryMethod();
        PlayGame();
    }

    private void SubscribeToEvents()
    {
        OnWin += message => Console.WriteLine($"{message}");
        OnLoose += message => Console.WriteLine($"{message}");
        OnDraw += message => Console.WriteLine($"{message}");
    }

    protected override void FactoryMethod()
    {
        Console.WriteLine($"Инициализация игры '{_gameName}'...");
        Console.WriteLine($"Создаем {_diceCount} костей для игрока и компьютера...");

        for (int i = 0; i < _diceCount; i++)
        {
            _playerDices.Add(new Dice(_minValue, _maxValue));
        }

        for (int i = 0; i < _diceCount; i++)
        {
            _computerDices.Add(new Dice(_minValue, _maxValue));
        }

        Console.WriteLine($"Игра готова! Кости: {_diceCount}шт, диапазон: {_minValue}-{_maxValue}");
    }

    public override void PlayGame()
    {
        Console.WriteLine($"\n════════ {_gameName} ════════");

        int playerScore = CalculateTotalScore(_playerDices, "Игрок");
        int computerScore = CalculateTotalScore(_computerDices, "Компьютер");

        DetermineWinner(playerScore, computerScore);
    }

    private int CalculateTotalScore(List<Dice> playerDices, string playerName)
    {
        List<int> result = new List<int>();

        foreach (var dice in playerDices)
        {
            int score = dice.Number;

            result.Add(score);
        }

        for (int i = 0; i < result.Count; i++)
        {
            Console.WriteLine($"Кость {playerName} #{i + 1} : {result[i]}");
        }

        int totalScore = result.Sum();

        Console.WriteLine($"Сумма всех очков {playerName} : {totalScore}");

        return totalScore;
    }

    private void DetermineWinner(int playerResult, int computerResult)
    {
        Console.WriteLine($"\nИТОГИ БОЯ:");
        Console.WriteLine($"Игрок: {playerResult} очков");
        Console.WriteLine($"Компьютер: {computerResult} очков");

        if (playerResult > computerResult)
        {
            OnWinInvoke();
            Console.WriteLine($"{playerResult} > {computerResult}");
        }

        if (playerResult < computerResult)
        {
            OnLooseInvoke();
            Console.WriteLine($"{playerResult} < {computerResult}");
        }

        if (playerResult == computerResult)
        {
            OnDrawInvoke();
            Console.WriteLine($"{playerResult} = {computerResult}");
        }

    }
}

