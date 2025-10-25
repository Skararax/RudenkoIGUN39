using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IDurability
{
    int Durability { get; set; }
    int MaxDurability { get; }

    void TakeDurabilityDamage();
    void Repair();
}
