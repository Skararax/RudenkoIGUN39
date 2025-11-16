using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal sealed class Blackjack : CasinoGameBase
{
    public int CardCounter { get; }

    private Queue<Card> _deck;
    private Random _random;

    public Blackjack(int cardCounter, GameProfil gameProfil) : base("Блэк-Джэк", gameProfil)
    {
        if (cardCounter <= 0 || cardCounter > int.MaxValue || cardCounter < 10)
        {
            throw new WrongNumbCardException(cardCounter);
        }

        CardCounter = cardCounter;
        _deck = new Queue<Card>();
        _random = new Random();

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
        Shuffle(CardCounter);
    }

    public void Shuffle(int cardCount)
    {
        _deck.Clear();

        for (int i = 0; i < cardCount; i++)
        {
            Card randomCard = CreateRandomCard();
            _deck.Enqueue(randomCard);
        }

        Console.WriteLine($"Создано {cardCount} случайных карт");
    }

    public Card CreateRandomCard()
    {
        CardSuit[] allSuits = Enum.GetValues<CardSuit>();
        CardRank[] allRanks = Enum.GetValues<CardRank>();

        CardSuit randomSuit = allSuits[_random.Next(allSuits.Length)];
        CardRank randomRank = allRanks[_random.Next(allRanks.Length)];

        return new Card(randomSuit, randomRank);
    }


    public override void PlayGame()
    {
        List<Card> playerHand = CardDispenser(_deck, "Игрок");
        List<Card> computerHand = CardDispenser(_deck, "Компьютер");

        DisplayPlayerHand(playerHand, "Игрок");
        DisplayComputerHand(computerHand, "Компьютер");

        int playerResult = CalculateScore(playerHand, "Игрок");
        int computerResult = CalculateScore(computerHand, "Компьютер");

        Console.WriteLine("\nНажмите Enter чтобы продолжить!");
        Console.ReadLine();

        GameLogic(playerResult, computerResult, playerHand, computerHand);
    }

    private List<Card> CardDispenser(Queue<Card> deck, string playerName)
    {
        Console.WriteLine($"\nРаздаем две карты пользователю: {playerName}");

        List<Card> cards = new List<Card>();

        for (int i = 0; i < 2; i++)
        {
            if (deck.Count > 0)
            {
                Card card = deck.Dequeue();
                cards.Add(card);
            }
            else
            {
                Console.WriteLine("В колоде закончились карты!");
                break;
            }
        }

        Console.WriteLine($"Карты успешно отданы пользователю : {playerName}");

        return cards;
    }

    private void DisplayPlayerHand(List<Card> card, string playerName)
    {
        Console.WriteLine($"\nУ пользователя {playerName} следующие карты:");
        foreach (Card i in card)
        {
            Console.WriteLine($"{i.Suit} {i.Rank}");
        }
    }

    private void DisplayComputerHand(List<Card> card, string playerName)
    {
        Console.WriteLine($"\nУ пользователя {playerName} следующие карты:");

        for (int i = 0; i < 1; i++)
        {
            Console.WriteLine($"{card[i].Suit} {card[i].Rank}");
        }

        Console.WriteLine("И одна скрытая :)");
    }

    private int CalculateScore(List<Card> hand, string playerName)
    {
        int sum = 0;
        foreach (Card card in hand)
        {
            sum += ((int)card.Rank);
        }

        Console.WriteLine($"\nСчет у {playerName} = {sum}");

        return sum;
    }

    private void GameLogic(int playerResult, int computerResult, List<Card> playerDeck, List<Card> compuerDeck)
    {
        if (playerResult == computerResult && playerResult <= 21)
        {
            Console.WriteLine("\nУ игрока и компьютера патовая ситуация! Добавляем вам по карте! Нажмите Enter чтобы продолжить!");
            Console.ReadLine();

            Card bonusPlayerCard = CreateRandomCard();
            playerDeck.Add(bonusPlayerCard);
            Card bonusComputerCard = CreateRandomCard();
            compuerDeck.Add(bonusComputerCard);

            DisplayPlayerHand(playerDeck, "Игрок");
            DisplayComputerHand(compuerDeck, "Компьютер");

            int playerResultTwo = CalculateScore(playerDeck, "Игрок");
            int computerResultTwo = CalculateScore(compuerDeck, "Компьютер");

            GameLogic(playerResultTwo, computerResultTwo, playerDeck, compuerDeck);
        }
        else if (playerResult == 21)
        {
            OnWinInvoke();
            Console.WriteLine("Игра окончена!");
        }
        else if (playerResult < computerResult && playerResult <= 21 && computerResult > 21)
        {
            OnWinInvoke();
            Console.WriteLine("Игра окончена!");
        }
        else if (playerResult > computerResult)
        {
            OnWinInvoke();
            Console.WriteLine("Игра окончена!");
        }
        else if (computerResult == 21)
        {
            OnLooseInvoke();
            Console.WriteLine("Игра окончена!");
        }
        else if (computerResult < playerResult && computerResult <= 21 && playerResult > 21)
        {
            OnLooseInvoke();
            Console.WriteLine("Игра окончена!");
        }
        else if (computerResult > playerResult)
        {
            OnLooseInvoke();
            Console.WriteLine("Игра окончена!");
        }
        else if (playerResult == computerResult && playerResult > 21)
        {
            OnDrawInvoke();
            Console.WriteLine("Игра окончена!");
        }
    }
}