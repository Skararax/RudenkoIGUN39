using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal sealed class Warrior : Character
{
    public int Defence { get; set; }

    public Warrior(string name, int level, int health, int damage, int defence)
        : base(name, level, health, damage)
    { 
        Defence = defence;
    }

    public override void SpecialAbility(Character target)
    {
        Defence += 1;
        Console.WriteLine($"Воин улучаешт свою броню! Теперь его броня равна = {Defence} !");
    }

    public override void TakeDamage(int damage)
    {
        int blockDamage = damage - Defence;
        base.TakeDamage(blockDamage);

        Console.WriteLine($"Воин блокирует {Defence} урона и по итогу получает {blockDamage}");
    }
}

