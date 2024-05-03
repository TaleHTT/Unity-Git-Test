using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum E_BattleType
{
    None = 0,
    Part_1 = 1,
    Part_2 = 2,
    Part_3 = 3,
    Length = 4,
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
    /// <summary>
    /// 通关该节点关卡获得的金币数量
    /// </summary>
    public int value = 0;

    /// <summary>
    /// 是否为普通关卡（每一个战斗关卡都分Normal和Expert）
    /// </summary>
    public bool isNormalLevel = true;

    public Vector3 position;
    public E_NodeType type = E_NodeType.Battle;
    public Image uiImage;
    public Sprite icon;
    public GameObject nodeUI = null;
    public List<Node> upperNodes;
    public List<Node> lowerNodes;
    public List<GameObject> lineUIs;
    /// <summary>
    /// 该节点是否被选中，作为本次地图显示的节点
    /// </summary>
    public bool IsSeleced = true;
    /// <summary>
    /// 根据进度判断当前节点是否可以点击
    /// </summary>
    public bool IsActive = false;
    /// <summary>
    /// 若为战斗节点对应的类型
    /// </summary>
    public E_BattleType battleType = 0;

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
