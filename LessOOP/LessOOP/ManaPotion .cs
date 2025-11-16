using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal sealed class HealPotion : Item
{
    public int RegenPower { get; protected set; }

    public HealPotion(int quantity, string name, int regenPower) : 
                      base(quantity, name)
    {
        RegenPower = regenPower;    
    }

    public override void UseItem(Character target)
    {
        if (Quantity <= 0) 
        {
            Console.WriteLine($"{Name} закончилось!");
            return;
        }

        Quantity--;
        Console.WriteLine($"Использовано {Name} на {target.Name}!");
        target.Heal(RegenPower);
    }
}

