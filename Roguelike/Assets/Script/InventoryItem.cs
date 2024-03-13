using System;

[Serializable]
public class InventoryItem
{
    public ItemData data;
    public int stackSize;
    public InventoryItem(ItemData newdata)
    {
        data = newdata;
        AddStack();
    }
    public void AddStack() => stackSize++;
    public void RemoveStack() => stackSize--;
}
