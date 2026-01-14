using System;
using System.Collections.Generic;

[Serializable]
public class PlayerState
{
    public int level = 1;
    public int gold = 0;

    public int baseMaxHp = 20;
    public int baseAttack = 3;
    public int baseDefense = 1;

    public int currentHp = 20;

    public Inventory inventory = new();

    // Equipment by slot
    public Dictionary<ItemSlot, ItemData> equipped = new()
    {
        { ItemSlot.Weapon, null },
        { ItemSlot.Head, null },
        { ItemSlot.Chest, null },
        { ItemSlot.Legs, null },
        { ItemSlot.Trinket, null },
    };

    public int MaxHp => baseMaxHp + SumEquipped(i => i.maxHpBonus);
    public int Attack => baseAttack + SumEquipped(i => i.attack);
    public int Defense => baseDefense + SumEquipped(i => i.defense);

    private int SumEquipped(Func<ItemData, int> selector)
    {
        int total = 0;
        foreach (var kv in equipped)
        {
            var item = kv.Value;
            if (item != null) total += selector(item);
        }
        return total;
    }

    public bool TryEquip(ItemData item)
    {
        if (item == null) return false;
        if (item.type != ItemType.Gear) return false;
        if (item.slot == ItemSlot.None) return false;

        equipped[item.slot] = item;
        // Optional: clamp HP if MaxHp decreased
        currentHp = Math.Min(currentHp, MaxHp);
        return true;
    }
}
