using GamePrototype.Utils;

namespace GamePrototype.Items.EquipItems
{
    public sealed class Armour : EquipItem
    {
        public Armour(uint defence, uint durability, string name) : base(name)
        {
            Defence = defence;
            Durability = durability;
            MaxDurability = durability;
        }

        public uint Defence { get; }
        public uint Durability { get; private set; }
        public uint MaxDurability { get; }
        public override EquipSlot Slot => EquipSlot.Armour;

        public void TakeDamage() 
        {
            if (Durability > 0) 
            { 
                Durability--;
            }

            if (Durability == 0) 
            {
                Console.WriteLine("Armour is broken!");
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
