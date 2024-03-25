using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [HideInInspector] private ItemGrid selectedItemGrid;
    public ItemGrid SelectedItemGrid
    {
        get => selectedItemGrid;
        set
        {
            selectedItemGrid = value;
        }

    }
    [SerializeField] EquipmentItem selectedItem;
    [SerializeField] EquipmentItem overlapItem;
    [SerializeField] List<ItemData> items;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Transform canvasTransform;
    RectTransform rectTransform;
    private void Update()
    {
        ItemIconDrag();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (selectedItem == null)
                CreateRandomItem();
        }
        if (SelectedItemGrid == null)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseButtonPress();
        }
        if (Input.GetMouseButtonDown(1))
        {
            RotateItem();
        }
    }

    private void RotateItem()
    {
        if (selectedItem == null)
            return;
        selectedItem.Rotate();
    }

    private void CreateRandomItem()
    {
        EquipmentItem equipmentItem = Instantiate(itemPrefab).GetComponent<EquipmentItem>();
        selectedItem = equipmentItem;
        rectTransform = equipmentItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);
        int selectedItemID = Random.Range(0, items.Count);
        equipmentItem.Set(items[selectedItemID]);
    }

    private void LeftMouseButtonPress()
    {
        Vector2Int tileGridPosition = GetTileGridPosition();
        if (selectedItem == null)
        {
            selectedItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);
            if (selectedItem != null)
            {
                rectTransform = selectedItem.GetComponent<RectTransform>();
                rectTransform.SetParent(GameObject.Find("Canvas").transform);
            }
        }
        else
        {
            bool capmlete = selectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y, ref overlapItem, selectedItem.itemData.shape);
            if (capmlete)
            {
                selectedItem = null;
                if (overlapItem != null)
                {
                    selectedItem = overlapItem;
                    overlapItem = null;
                    rectTransform = selectedItem.GetComponent<RectTransform>();
                    rectTransform.SetParent(GameObject.Find("Canvas").transform);
                }
            }
        }
    }

    private Vector2Int GetTileGridPosition()
    {
        Vector2 position = Input.mousePosition;
        if (selectedItem != null)
        {
            position.x -= (selectedItem.Width - 1) * ItemGrid.tileSizeWidth / 2;
            position.y += (selectedItem.Heigth - 1) * ItemGrid.tileSizeHeight / 2;
        }
        return selectedItemGrid.GetTilePosition(position);
    }

    private void ItemIconDrag()
    {
        if (selectedItem != null)
        {
            rectTransform.position = Input.mousePosition;
        }
    }
}
