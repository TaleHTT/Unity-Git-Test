using System.Collections.Generic;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    int unlockTarget;
    int whichSlot = 0;
    [SerializeField] int gridSizeWidth;
    [SerializeField] int gridSizeHeight;
    [HideInInspector] public const float tileSizeWidth = 96;
    [HideInInspector] public const float tileSizeHeight = 96;
    [HideInInspector] public RectTransform rectTransform;
    [SerializeField] private List<bool> isCanUseSlot;
    [SerializeField] private List<GameObject> slots;
    bool[,] canUseGrid;
    Equipment equipment;
    EquipmentItem[,] equipmentItemSlot;
    Vector2 positionOnTheGrid = new Vector2();
    Vector2Int tileGridPosition = new Vector2Int();
    private void Awake()
    {
        equipment = GetComponent<Equipment>();
        canUseGrid = new bool[gridSizeWidth, gridSizeHeight];
        isCanUseSlot = new List<bool>();
        for (int i = 0; i < gridSizeWidth; i++)
        {
            for (int j = 0; j < gridSizeHeight; j++)
            {
                isCanUseSlot.Add(canUseGrid[i, j]);
            }
        }
        if(gridSizeWidth == 3 &&  gridSizeHeight == 3)
        {
            int unlockSlot = Random.Range(1, 6);
            for (int i = 0; i < unlockSlot; i++)
            {
                int x = Random.Range(1, isCanUseSlot.Count);
                if (unlockTarget != x)
                {
                    unlockTarget = x;
                }
                isCanUseSlot[x] = true;
            }
        }
        else if(gridSizeWidth >= 3 && gridSizeHeight >= 3)
        {
            for(int i = 0;i < gridSizeWidth; i++)
            {
                for(int j = 0;j < gridSizeHeight; j++)
                {
                    isCanUseSlot[whichSlot] = true;
                    whichSlot++;
                }
            }
            whichSlot = 0;
        }
    }
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Init(gridSizeWidth, gridSizeHeight);
        for (int i = 0; i < gridSizeWidth; i++)
        {
            for (int j = 0; j < gridSizeHeight; j++)
            {
                canUseGrid[i, j] = isCanUseSlot[whichSlot];
                if (canUseGrid[i, j] == false)
                {
                    slots[whichSlot].SetActive(false);
                }
                else if (canUseGrid[i, j] == true)
                {
                    slots[whichSlot].SetActive(true);
                }
                whichSlot++;
            }
        }
        whichSlot = 0;
    }
    private void Init(int width, int height)
    {
        equipmentItemSlot = new EquipmentItem[width, height];
        Vector2 size = new Vector2(width * tileSizeWidth, height * tileSizeHeight);
        rectTransform.sizeDelta = size;
    }
    public Vector2Int GetTilePosition(Vector2 mousePosition)
    {
        positionOnTheGrid.x = mousePosition.x - rectTransform.position.x;
        positionOnTheGrid.y = rectTransform.position.y - mousePosition.y;

        tileGridPosition.y = (int)(positionOnTheGrid.y / tileSizeHeight);
        tileGridPosition.x = (int)(positionOnTheGrid.x / tileSizeWidth);

        return tileGridPosition;
    }
    public bool PlaceItem(EquipmentItem equipmentItem, int posX, int posY, ref EquipmentItem overlapItem, Shape itemShape)
    {
        int[,] whatIsShape = equipmentItem.ItemShape(itemShape);
        if (BoundryCheck(posX, posY, equipmentItem.Width, equipmentItem.Heigth) == false)
        {
            return false;
        }

        if (OverlapCheck(posX, posY, equipmentItem.Width, equipmentItem.Heigth, ref overlapItem, whatIsShape) == false)
        {
            overlapItem = null;
            return false;
        }
        if (overlapItem != null)
        {
            CleanGridReference(overlapItem);
        }
        RectTransform rectTransform = equipmentItem.GetComponent<RectTransform>();

        for (int x = 0; x < equipmentItem.Width; x++)
        {
            for (int y = 0; y < equipmentItem.Heigth; y++)
            {
                if (whatIsShape[x, y] == 0)
                    continue;
                if (canUseGrid[posX + x, posY + y] == false)
                    return false;
                equipmentItemSlot[posX + x, posY + y] = equipmentItem;
            }
        }
        rectTransform.SetParent(this.rectTransform);
        equipmentItem.onGridPositionX = posX;
        equipmentItem.onGridPositionY = posY;
        Vector2 position = CalculatePositionOnGrid(equipmentItem, posX, posY);

        rectTransform.localPosition = position;
        if(equipment != null)
            equipment.AddItem(equipmentItem.itemData);
        return true;
    }
    public Vector2 CalculatePositionOnGrid(EquipmentItem inventoryItem, int posX, int posY)
    {
        Vector2 position = new Vector2();
        position.x = posX * tileSizeWidth + tileSizeWidth * inventoryItem.Width / 2;
        position.y = -(posY * tileSizeHeight + tileSizeHeight * inventoryItem.Heigth / 2);
        return position;
    }

    private bool OverlapCheck(int posX, int posY, int width, int height, ref EquipmentItem overlapItem, int[,] item)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (item[x, y] == 0)
                    continue;
                if (equipmentItemSlot[posX + x, posY + y] != null)
                {
                    if (overlapItem == null)
                    {
                        overlapItem = equipmentItemSlot[posX + x, posY + y];
                    }
                    else
                    {
                        if (overlapItem != equipmentItemSlot[posX + x, posY + y])
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }

    internal EquipmentItem PickUpItem(int x, int y)
    {
        EquipmentItem toReturn = equipmentItemSlot[x, y];
        if (toReturn == null)
            return null;
        CleanGridReference(toReturn);
        if(equipment != null)
            equipment.RemoveItem(toReturn.itemData);
        return toReturn;
    }

    private void CleanGridReference(EquipmentItem item)
    {
        int[,] whatIsShape = item.ItemShape(item.itemData.shape);
        for (int _x = 0; _x < item.Width; _x++)
        {
            for (int _y = 0; _y < item.Heigth; _y++)
            {
                if (whatIsShape[_x, _y] == 0)
                    continue;
                equipmentItemSlot[item.onGridPositionX + _x, item.onGridPositionY + _y] = null;
            }
        }
    }

    public bool PositionCheck(int posX, int posY)
    {
        if (posX < 0 || posY < 0)
            return false;
        if (posX >= gridSizeWidth || posY >= gridSizeHeight)
            return false;
        return true;
    }
    public bool BoundryCheck(int posX, int posY, int width, int height)
    {
        if (PositionCheck(posX, posY) == false)
            return false;
        posX += width - 1;
        posY += height - 1;
        if (PositionCheck(posX, posY) == false)
            return false;
        return true;
    }

    internal EquipmentItem GetItem(int x, int y)
    {
        return equipmentItemSlot[x, y];
    }
}
