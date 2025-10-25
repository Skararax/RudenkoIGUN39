using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePrototype.Items.EquipItems;
using GamePrototype.Utils;

namespace GamePrototype.Items.EquipItems
{
    public sealed class Helmet: EquipItem
    {
        public uint Defence { get; }
        public uint Durability { get; private set; }
        public uint MaxDurability { get; }

        public Helmet(uint defence, uint durability, string name) : base(name) 
        { 
            Defence = defence;
            Durability = durability;
            MaxDurability = durability;
        }

        public override EquipSlot Slot => EquipSlot.Helmet;

        public void LoseDurability()
        {
            if (Durability > 0)
            {
                Durability--;
            }
        }

        public void Repair(uint repairAmount)
        {
            Durability += repairAmount;
            if (Durability > MaxDurability)
                Durability = MaxDurability;
        }

    }
}
