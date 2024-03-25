using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(ItemGrid))]
public class GridInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    InventoryController inventoryController;
    ItemGrid itemGrid;
    RectTransform rectTransform;

    public void OnPointerEnter(PointerEventData eventData)
    {
        inventoryController.SelectedItemGrid = itemGrid;
        inventoryController.SelectedItemGrid.GetComponent<RectTransform>().SetParent(rectTransform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inventoryController.SelectedItemGrid = null;
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        itemGrid = GetComponent<ItemGrid>();
        inventoryController = FindObjectOfType(typeof(InventoryController)) as InventoryController;
    }
}
