using System;

[Serializable]
public class ItemStack
{
    public ItemData data;
    public int count;

    public ItemStack(ItemData data, int count)
    {
        this.data = data;
        this.count = count;
    }
}
