using System.Xml.Linq;
using GamePrototype.Dungeon;
using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Units;

public enum Difficulty
{
    Easy,
    Hard
}

public interface IDurability
{
    int Durability { get; set; }
    int MaxDurability { get; }
    void TakeDurabilityDamage();
    void Repair();
}

public interface IUnitFactory
{
    Unit CreatePlayer(string name);
    Unit CreateEnemy();
}

public interface IDungeonBuilder
{
    DungeonRoom BuildDungeon();
}