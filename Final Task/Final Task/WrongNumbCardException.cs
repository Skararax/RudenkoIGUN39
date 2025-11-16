using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class WrongNumbCardException : Exception
{
    public WrongNumbCardException(int invalidCardNum)
        : base($"Некорректное число карт {invalidCardNum}. Число карт не может быть отрицательным, равняться нулю, меньше 10 штук и быть больше числа: {int.MaxValue}")
    {
    }
}

