using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

abstract public class Character
{
    public string Name { get; set; }
    public int Level { get; protected set; }
    public int Health { get; protected set; }
    public int MaxHealth { get; protected set; }
    public int Damage { get; protected set; }
    public Inventory Inventory { get; protected set; }

    public bool IsAlive => Health > 0;

    private int _expCounter = 0;

    public Character(string name, int level, int health, int damage, int inventorySize = 1)
    {
        Name = name;
        Level = level;
        Health = health;
        MaxHealth = health;
        Damage = damage;
        Inventory = new Inventory(inventorySize);
    }

    public virtual void Attack(Character target)
    {
        if (!IsAlive) return;

        target.TakeDamage(Damage);

        _expCounter++;

        Console.WriteLine($"{Name} наносит урон {target.Name} в количестве {Damage}");

        if (_expCounter >= 5) 
        {
            LevelUp();
            _expCounter = 0;
        }
    }

    public virtual void TakeDamage(int damage)
    {
        if (!IsAlive) return;

        Health -= damage;
    }

    public virtual void Heal(int amount)
    {
        if (!IsAlive) 
        {
            Console.WriteLine($"{Name} мертв и не может быть вылечен!");
            return; 
        } 

        Health += amount;

        if (Health > MaxHealth)
            Health = MaxHealth;

        Console.WriteLine($"{Name} восстановил {amount} здоровья. Теперь: {Health}/{MaxHealth}");
    }

    public abstract void SpecialAbility(Character target);

    public virtual void LevelUp()
    {
        Level++;
        MaxHealth += 10;
        Damage += 4;
        Health = MaxHealth;

        Console.WriteLine($"{Name} достиг {Level} уровня! " +
             $"HP: {MaxHealth}, Урон: {Damage}");
    }

    public void UseItemFromInventory(string name, Character target)
    {
        
        if ( target == null )
            return;
    }
}
