using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public List<EquipmentSavaObject> equipmentSaveObejects;
    public Dictionary<ItemData, EquipmentSavaObject> equipmentDictionary;
    private void Start()
    {
        equipmentSaveObejects = new List<EquipmentSavaObject>();
        equipmentDictionary = new Dictionary<ItemData, EquipmentSavaObject>();
    }
    public void AddItem(ItemData item)
    {
        if (equipmentDictionary.TryGetValue(item, out EquipmentSavaObject value))
        {
            value.AddStack();
        }
        else
        {
            EquipmentSavaObject newItem = new EquipmentSavaObject(item);
            equipmentSaveObejects.Add(newItem);
            equipmentDictionary.Add(item, newItem);
        }
        item.AddModfiers();
    }
    public void RemoveItem(ItemData item)
    {
        if (equipmentDictionary.TryGetValue(item, out EquipmentSavaObject value))
        {
            if (value.stackSize <= 1)
            {
                equipmentSaveObejects.Remove(value);
                equipmentDictionary.Remove(item);
            }
            else
            {
                value.RemoveStack();
            }
        }
        item.RemoveModfiers();
    }
}
