using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Inventory
{
    private List<Item> _items = new List<Item>();
    private int _maxSize;

    public IReadOnlyList<Item> Items => _items.AsReadOnly();
    public int Count => _items.Count;
    public bool IsFull => _items.Count >= _maxSize;

    public Inventory(int maxSize = 10)
    {
        _maxSize = maxSize;
    }

    public void AddItem(Item item)
    {
        if (IsFull)
        {
            Console.WriteLine("Инвентарь полон!");
            return;
        }

        _items.Add(item);
        Console.WriteLine($"{item.Name} успешно добавлен!");
    }

    public void ReturnItem(Item item)
    {
        _items.Remove(item);
    }
}

