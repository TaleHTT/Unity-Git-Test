using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum E_LevelType
{
    None = 0,
    Part_1 = 1,
    Part_2 = 2,
    Part_3 = 3,
    Num = 4, //枚举的数量，类似Count+1的存在
    Shop = 5,
}

public enum E_NodeType
{
    Battle = 0, // 战斗
    Shop = 1, // 商店
    Boss = 2, //boss

    /*Rest = 3, // 营火
    Elite = 4, // 精英
    Random = 5,//随机
    Treasure = 6, // 宝箱*/
}

public class Node : MonoBehaviour
{
    

    public Vector3 position;
    public E_NodeType type = E_NodeType.Battle;
    public Image uiImage;
    public Sprite icon;
    public GameObject nodeUI = null;
    public List<Node> upperNodes;
    public List<Node> lowerNodes;
    public List<GameObject> lineUIs;
    /// <summary>
    /// 该节点是否被选中
    /// </summary>
    public bool IsSeleced = false;
    public bool IsActive = false;
    /// <summary>
    /// 该节点点击后前往的场景
    /// </summary>
    public E_LevelType level;

    public Node(Vector2 position, E_NodeType type = E_NodeType.Battle)
    {
        this.position = position;
        this.type = type;
        this.upperNodes = new List<Node>();
        this.lowerNodes = new List<Node>();
    }

    public void Start()
    {
        this.icon = null;
        position = this.transform.position;
        this.upperNodes = new List<Node>();
        this.lowerNodes = new List<Node>();
    }

    public void SetTrue()
    {
        this.uiImage.color = new Color(0, 0, 0, 1);
    }
}
