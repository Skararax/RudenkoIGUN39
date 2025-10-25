using GamePrototype.Items.EquipItems;

namespace GamePrototype.Items.EconomicItems
{
    public sealed class Grindstone : EconomicItem
    {
        public override bool Stackable => false;

        public uint RepairPower { get; }

        public Grindstone(string name, uint repirPower = 10) : base(name)
        {
            RepairPower = repirPower;
        }

        public void UseOnArmour(Armour armour) 
        {
            if (armour == null) 
            { 
                return;
            }

            armour.Repair(RepairPower);
            Console.WriteLine("Repair Success!");
        }
    }
}
