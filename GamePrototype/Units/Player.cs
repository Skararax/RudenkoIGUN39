using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Utils;
using System.Text;

namespace GamePrototype.Units
{
    public sealed class Player : Unit
    {
        private readonly Dictionary<EquipSlot, EquipItem> _equipment = new();

        public Player(string name, uint health, uint maxHealth, uint baseDamage) : base(name, health, maxHealth, baseDamage)
        {
        }

        public override uint GetUnitDamage()
        {
            if (_equipment.TryGetValue(EquipSlot.Weapon, out var item) && item is Weapon weapon)
            {
                return BaseDamage + weapon.Damage;
            }
            return BaseDamage;
        }

        public override void HandleCombatComplete()
        {
            var items = Inventory.Items;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] is EconomicItem economicItem)
                {
                    UseEconomicItem(economicItem);
                    Inventory.TryRemove(items[i]);
                }
            }
        }

        public override void AddItemToInventory(Item item)
        {
            if (item is EquipItem equipItem)
            {
                if (TryEquipItem(equipItem))
                {
                    return; 
                }
            }

            base.AddItemToInventory(item);
        }

        private bool TryEquipItem(EquipItem newItem)
        {
            var slot = newItem.Slot;

            if (_equipment.ContainsKey(slot))
            {
                var oldItem = _equipment[slot];
                _equipment[slot] = newItem;

                base.AddItemToInventory(oldItem);

                Console.WriteLine($"{Name} заменил {oldItem.Name} на {newItem.Name} в слоте {GetSlotName(slot)}!");
                return true;
            }
            else
            {
                if (_equipment.TryAdd(slot, newItem))
                {
                    Console.WriteLine($"{Name} экипировал {newItem.Name} в слот {GetSlotName(slot)}!");
                    return true;
                }
            }

            return false;
        }

        private string GetSlotName(EquipSlot slot)
        {
            return slot switch
            {
                EquipSlot.Armour => "Броня",
                EquipSlot.Weapon => "Оружие",
                EquipSlot.Helmet => "Шлем",
                EquipSlot.RangeWeapon => "Дальнее оружие",
                _ => "Неизвестный слот"
            };
        }

        public bool EquipFromInventory(EquipItem item)
        {
            if (!Inventory.Items.Contains(item))
            {
                Console.WriteLine($"Предмет {item.Name} не найден в инвентаре!");
                return false;
            }

            if (TryEquipItem(item))
            {
                Inventory.TryRemove(item);
                return true;
            }

            return false;
        }

        public bool UnequipItem(EquipSlot slot)
        {
            if (_equipment.TryGetValue(slot, out var item))
            {
                if (Inventory.TryAdd(item))
                {
                    _equipment.Remove(slot);
                    Console.WriteLine($"{Name} снял {item.Name} и положил в инвентарь");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Инвентарь полон! Нельзя снять {item.Name}");
                    return false;
                }
            }

            Console.WriteLine($"В слоте {GetSlotName(slot)} ничего не экипировано");
            return false;
        }

        public void DisplayEquipment()
        {
            Console.WriteLine($"\n=== Экипировка {Name} ===");

            foreach (EquipSlot slot in Enum.GetValues(typeof(EquipSlot)))
            {
                if (_equipment.TryGetValue(slot, out var item))
                {
                    Console.WriteLine($"{GetSlotName(slot)}: {item.Name}");
                }
                else
                {
                    Console.WriteLine($"{GetSlotName(slot)}: ---");
                }
            }
        }

        private void UseEconomicItem(EconomicItem economicItem)
        {
            if (economicItem is HealthPotion healthPotion)
            {
                Health += healthPotion.HealthRestore;
                Console.WriteLine($"{Name} использовал зелье здоровья! +{healthPotion.HealthRestore} HP");
            }
        }

        protected override uint CalculateAppliedDamage(uint damage)
        {
            if (_equipment.TryGetValue(EquipSlot.Armour, out var item) && item is Armour armour)
            {
                damage -= (uint)(damage * (armour.Defence / 100f));
            }
            return damage;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(Name);
            builder.AppendLine($"Health {Health}/{MaxHealth}");

            builder.AppendLine("Экипировка:");
            foreach (var equip in _equipment)
            {
                builder.AppendLine($"{GetSlotName(equip.Key)}: {equip.Value.Name}");
            }

            builder.AppendLine("Инвентарь:");
            var items = Inventory.Items;
            for (int i = 0; i < items.Count; i++)
            {
                builder.AppendLine($"[{items[i].Name}] : {items[i].Amount}");
            }
            return builder.ToString();
        }
    }
}