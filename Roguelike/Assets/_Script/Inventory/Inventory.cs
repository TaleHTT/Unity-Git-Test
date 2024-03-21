using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public List<InventoryItem> inventoryItems;
    public Dictionary<ItemData, InventoryItem> inventoryDictionary;
    [SerializeField] private Transform inventorySlotParent;
    private UI_ItemSlot[] itemSlot;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        inventoryItems = new List<InventoryItem>();
        inventoryDictionary = new Dictionary<ItemData, InventoryItem>();
        itemSlot = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
    }
    private void Update()
    {
        
    }
    private void UpdataSlotUI()
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            itemSlot[i].UpdataSlot(inventoryItems[i]);
        }
    }
    public void AddItem(ItemData item)
    {
        if(inventoryDictionary.TryGetValue(item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(item);
            inventoryItems.Add(newItem);
            inventoryDictionary.Add(item, newItem);
        }
        UpdataSlotUI();
    }
    public void RemoveItem(ItemData item)
    {
        if(inventoryDictionary.TryGetValue(item,out InventoryItem value))
        {
            if(value.stackSize <= 1)
            {
                inventoryItems.Remove(value);
                inventoryDictionary.Remove(item);
            }
            else
                value.RemoveStack();
        }
        UpdataSlotUI();
    }
}
