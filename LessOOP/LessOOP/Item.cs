using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public abstract class Item
{
    public int Quantity { get; protected set; }
    public string Name { get; protected set; }

    public Item(int quantity, string name) 
    {
        Quantity = quantity;
        Name = name;    
    }

    public abstract void UseItem(Character target);
}

