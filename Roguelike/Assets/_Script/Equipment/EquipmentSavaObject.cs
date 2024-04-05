using System;
[Serializable]
public class EquipmentSavaObject
{
    public int stackSize;
    public ItemData itemData;
    public EquipmentSavaObject(ItemData itemData)
    {
        this.itemData = itemData;
        AddStack();
    }
    public void AddStack() => stackSize++;
    public void RemoveStack() => stackSize--;
}
