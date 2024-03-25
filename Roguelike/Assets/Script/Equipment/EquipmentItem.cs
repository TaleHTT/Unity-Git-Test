using UnityEngine;
using UnityEngine.UI;

public class EquipmentItem : MonoBehaviour
{
    RectTransform rectTransform;
    public ItemData itemData;
    public int Heigth
    {
        get
        {
            if (rotated == false)
                return itemData.height;
            return itemData.width;
        }
    }
    public int Width
    {
        get
        {
            if (rotated == false)
                return itemData.width;
            return itemData.height;
        }
    }
    public int onGridPositionX;
    public int onGridPositionY;
    public int rotateSum;
    public bool rotated = false;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public int[,] ItemShape(Shape itemShape)
    {
        int[,] regularShape = new int[Width, Heigth];
        if (itemShape == Shape.regular)
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Heigth; j++)
                {
                    regularShape[i, j] = 1;
                }
            }
            return regularShape;
        }
        else if (itemShape == Shape.L)
        {
            if (rectTransform.rotation.z == 0)
                return new int[,]
                {
                    { 1, 1 },
                    { 0, 1 },
                    { 0, 1 },
                };
            else if (rotateSum == 1)
                return new int[,]
                {
                    { 1, 1, 1 },
                    { 1, 0, 0 },
                };
            else if (rotateSum == 2)
                return new int[,]
                {
                    { 1, 0 },
                    { 1, 0 },
                    { 1, 1 },
                };
            else if (rotateSum == 3)
                return new int[,]
                {
                    { 0, 0, 1 },
                    { 1, 1, 1 },
                };
        }
        else if (itemShape == Shape.samllL)
        {
            if (rectTransform.rotation.z == 0)
                return new int[,]
                {
                    { 0, 1 },
                    { 1, 1 },
                };
            else if (rotateSum == 1)
                return new int[,]
                {
                    { 1, 1 },
                    { 0, 1 },
                };
            else if (rotateSum == 2)
                return new int[,]
                {
                    { 1, 1 },
                    { 1, 0 },
                };
            else if (rotateSum == 3)
                return new int[,]
                {
                    { 1, 0 },
                    { 1, 1 }
                };
        }
        else if (itemShape == Shape.T)
        {
            if (rectTransform.rotation.z == 0)
                return new int[,]
                {
                    { 1, 0, 0 },
                    { 1, 1, 1 },
                    { 1, 0, 0 },
                };
            else if (rotateSum == 1)
                return new int[,]
                {
                    { 0, 1, 0 },
                    { 0, 1, 0 },
                    { 1, 1, 1 },
                };
            else if (rotateSum == 2)
                return new int[,]
                {
                    { 0, 0, 1 },
                    { 1, 1, 1 },
                    { 0, 0, 1 },
                };
            else if (rotateSum == 3)
                return new int[,]
                {
                    { 1, 1, 1 },
                    { 0, 1, 0 },
                    { 0, 1, 0 },
                };
        }
        else if (itemShape == Shape.U)
        {
            if (rectTransform.rotation.z == 0)
                return new int[,]
                {
                    { 1, 1 },
                    { 0, 1 },
                    { 1, 1 },
                };
            else if (rotateSum == 1)
                return new int[,]
                {
                    { 1, 1, 1 },
                    { 1, 0, 1 },
                };
            else if (rotateSum == 2)
                return new int[,]
                {
                    { 1, 1 },
                    { 1, 0 },
                    { 1, 1 },
                };
            else if (rotateSum == 3)
                return new int[,]
                {
                    { 1, 0, 1 },
                    { 1, 1, 1 },
                };
        }
        else if (itemShape == Shape.Z)
        {
            if (rectTransform.rotation.z == 0 || rotateSum == 2)
                return new int[,]
                {
                    { 1, 1, 0 },
                    { 0, 1, 0 },
                    { 0, 1, 1 },
                };
            else if (rotateSum == 1 || rotateSum == 3)
                return new int[,]
                {
                    { 0, 0, 1 },
                    { 1, 1, 1 },
                    { 1, 0, 0 },
                };
        }
        else if (itemShape == Shape.samllT)
        {
            if (rectTransform.rotation.z == 0)
                return new int[,]
                {
                    { 0, 1 },
                    { 1, 1 },
                    { 0, 1 },
                };
            else if (rotateSum == 1)
                return new int[,]
                {
                    { 1, 1, 1 },
                    { 0, 1, 0 },
                };
            else if (rotateSum == 2)
                return new int[,]
                {
                    { 1, 0 },
                    { 1, 1 },
                    { 1, 0 },
                };
            else if (rotateSum == 3)
                return new int[,]
                {
                    { 0, 1, 0 },
                    { 1, 1, 1 },
                };
        }
        else if (itemShape == Shape.samllZ)
        {
            if (rectTransform.rotation.z == 0)
                return new int[,]
                {
                    { 1, 1 },
                    { 1, 1 },
                    { 0, 1 },
                };
            else if (rotateSum == 1)
                return new int[,]
                {
                    { 1, 1, 1 },
                    { 1, 1, 0 },
                };
            else if (rotateSum == 2)
                return new int[,]
                {
                    { 1, 0 },
                    { 1, 1 },
                    { 1, 1 },
                };
            else if (rotateSum == 3)
                return new int[,]
                {
                    { 0, 1, 1 },
                    { 1, 1, 1 },
                };
        }
        else if (itemShape == Shape.Rootnumber)
        {
            if (rectTransform.rotation.z == 0)
                return new int[,]
                {
                    { 1, 1, 0 },
                    { 0, 1, 1 },
                    { 0, 1, 0 },
                };
            else if (rotateSum == 1)
                return new int[,]
                {
                    { 0, 1, 0 },
                    { 1, 1, 1 },
                    { 1, 0, 0 },
                };
            else if (rotateSum == 2)
                return new int[,]
                {
                    { 0, 1, 0 },
                    { 1, 1, 0 },
                    { 0, 1, 1 },
                };
            else if (rotateSum == 3)
                return new int[,]
                {
                    { 0, 0, 1 },
                    { 1, 1, 1 },
                    { 0, 1, 0 },
                };
        }
        else if (itemShape == Shape.X)
        {
            return new int[,]
            {
                    { 0, 1, 0 },
                    { 1, 1, 1 },
                    { 0, 1, 0 },
            };
        }
        return null;
    }
    internal void Rotate()
    {
        rotated = !rotated;
        rectTransform.Rotate(0, 0, -90);
        rotateSum++;
        if (rotateSum >= 4)
            rotateSum = 0;
    }

    internal void Set(ItemData itemData)
    {
        this.itemData = itemData;
        GetComponent<Image>().sprite = itemData.itemIcon;
        Vector2 size = new Vector2();
        size.x = itemData.width * ItemGrid.tileSizeWidth;
        size.y = itemData.height * ItemGrid.tileSizeHeight;
        GetComponent<RectTransform>().sizeDelta = size;
    }
}
