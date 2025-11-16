using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal sealed class Mage : Character
{
    public int Mana { get; protected set; }
    public int MaxMana { get; protected set; }

    public Mage(string name, int level, int health, int damage, int mana)
        : base(name, level, health, damage)
    {
        Mana = mana;
        MaxMana = mana;
    }

    public override void Attack(Character target)
    {
        base.Attack(target);
    }

    public override void Heal(int amount)
    {
        if (Mana >= 20)
        {
            amount += 10;
            base.Heal(amount);
            Console.WriteLine($"{Name} КАСТУЕТ СИЛЬНОЕ ЛЕЧЕНИЕ И ВОССТАНАВЛИВАЕТ СЕБЕ {amount} ЗДОРОВЬЯ! ");
        }
        else { Console.WriteLine($"Недостаточно маны для заклинания! Нужно 20, есть {Mana}"); }
    }

    public override void SpecialAbility(Character target)
    {
        if (Mana >= 20)
        {
            Mana -= 20;
            int bonusDamage = 3;
            Console.WriteLine($"{Name} кастует усиление урона! +{bonusDamage} к урону");
            Damage += bonusDamage;
        }
        else
        {
            Console.WriteLine($"Недостаточно маны для заклинания! Нужно 20, есть {Mana}");
        }
    }

    public override void LevelUp()
    {
        base.LevelUp();
        MaxMana += 10;
        Mana = MaxMana;
        Console.WriteLine($"И мана: {MaxMana}!");
    }
}

