using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


public class StatesManager
{
    private Random _random = new Random();

    public virtual States DecideAction(Character person)
    {
        if (person.Health < person.MaxHealth * 0.4f)
        {
            return States.heal;
        }
        if (_random.Next(0, 2) == 1)
        {
            return States.special;
        }
        else
        {
            return States.attack;
        }
    }

    public virtual void PickState(States state, Character person, Character target = null)
    {
        switch (state)
        {
            case States.attack:
                if (target == null)
                {
                    Console.WriteLine("Нет цели для атаки");
                    return;
                }
                Console.WriteLine($"{person.Name} использует аттаку!");

                person.Attack(target);
                break;

            case States.heal:
                if (person.Health == person.MaxHealth)
                {
                    Console.WriteLine($"{person.Name} пытался излечиться, но у него полное хп!");
                    return;
                }

                person.Heal(8);
                Console.WriteLine($"{person.Name}  излечился!");
                break;

            case States.special:
                {
                    person.SpecialAbility(target);
                    break;
                }
        }
    }
}

