using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class WrongDiceNumberException : Exception
{
    public WrongDiceNumberException(int invalidNumber, int minAllowed, int maxAllowed) 
        : base($"Некорректное число: {invalidNumber}. Допустимый диапазон: от {minAllowed} до {maxAllowed}")
    {
    } 
}

