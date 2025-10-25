using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePrototype.Utils;

namespace GamePrototype.Items.EquipItems
{
    internal class RangeWeapon : EquipItem
    {
        public uint RangeDistance { get; }
        public uint Damage { get; }
        public uint Durability { get; private set; }
        public uint MaxDurability { get; }

        public RangeWeapon(uint rangeDistance, uint damage, uint durability, string name) : base(name)
        {
            RangeDistance = rangeDistance;
            Damage = damage;
            Durability = durability;
            MaxDurability = durability;
        }

        public override EquipSlot Slot => EquipSlot.RangeWeapon;

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
