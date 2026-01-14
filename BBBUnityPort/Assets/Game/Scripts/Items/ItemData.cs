using UnityEngine;

[CreateAssetMenu(menuName = "BBB/Item")]
public class ItemData : ScriptableObject
{
    public string id;              // unique string (e.g., "iron_sword")
    public string displayName;     // "Iron Sword"
    [TextArea] public string description;

    public Sprite icon;
    public ItemType type = ItemType.Gear;
    public ItemSlot slot = ItemSlot.None;
    public ItemRarity rarity = ItemRarity.Common;

    // Simple stats (extend later)
    public int attack;
    public int defense;
    public int maxHpBonus;

    public int valueGold = 10;
    public bool stackable = false;
    public int maxStack = 99;
}
