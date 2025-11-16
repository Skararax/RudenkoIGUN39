using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


internal sealed class Rogue : Character
{
    public int Stamina { get; protected set; }
    public int MaxStamina { get; protected set; }

    public Rogue(string name, int level, int health, int damage, int stamina)
        : base(name, level, health, damage)
    {
        MaxStamina = stamina;        Stamina = MaxStamina;
    }

    public override void Attack(Character target)
    {
        base.Attack(target);
    }

    public override void SpecialAbility(Character target)
    {
        int shadowStep = Damage + 5;

        target.TakeDamage(shadowStep);

        Console.WriteLine($"{Name} применяет специальный навык! И наносит {shadowStep} урона!");
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }
}
