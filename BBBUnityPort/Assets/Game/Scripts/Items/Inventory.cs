using System;
using System.Collections.Generic;

[Serializable]
public class Inventory
{
    public int capacity = 30;
    public List<ItemStack> items = new();

    public bool TryAdd(ItemData item, int amount = 1)
    {
        if (item == null || amount <= 0) return false;

        if (item.stackable)
        {
            // Try stack into existing
            foreach (var stack in items)
            {
                if (stack.data == item && stack.count < item.maxStack)
                {
                    int space = item.maxStack - stack.count;
                    int add = Math.Min(space, amount);
                    stack.count += add;
                    amount -= add;
                    if (amount <= 0) return true;
                }
            }
        }

        // Add new stacks if room
        while (amount > 0)
        {
            if (items.Count >= capacity) return false;

            int add = item.stackable ? Math.Min(item.maxStack, amount) : 1;
            items.Add(new ItemStack(item, add));
            amount -= add;
        }

        return true;
    }

    public bool Remove(ItemData item, int amount = 1)
    {
        if (item == null || amount <= 0) return false;

        for (int i = items.Count - 1; i >= 0 && amount > 0; i--)
        {
            if (items[i].data != item) continue;

            int take = Math.Min(items[i].count, amount);
            items[i].count -= take;
            amount -= take;

            if (items[i].count <= 0)
                items.RemoveAt(i);
        }

        return amount == 0;
    }
}
