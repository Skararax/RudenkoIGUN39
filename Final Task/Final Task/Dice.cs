using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct Dice
{
    private int _min;
    private int _max;
    private Random _random;

    public Dice(int min, int max)
    {
        if (min <= 0 || min > int.MaxValue)
        {
            throw new WrongDiceNumberException(min, 1, int.MaxValue);
        }

        if (max <= 0 || max < min || max > int.MaxValue)
        {
            throw new WrongDiceNumberException(max, 1, int.MaxValue);
        }

        _max = max;
        _min = min;
        _random = new Random();
    }

    public int Number { get => _random.Next(_min, _max + 1); }
}
