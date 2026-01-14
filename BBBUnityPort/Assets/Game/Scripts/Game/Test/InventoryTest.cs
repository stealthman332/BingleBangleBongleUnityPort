using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    [SerializeField] private ItemData testItem;

    private void Start()
    {
        var p = GameState.I.player;
        p.inventory.TryAdd(testItem, 1);
        p.TryEquip(testItem);

        Debug.Log($"Equipped: {testItem.displayName}");
        Debug.Log($"ATK={p.Attack} DEF={p.Defense} MaxHP={p.MaxHp}");
    }
}
