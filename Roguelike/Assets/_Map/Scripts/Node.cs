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
    Battle = 0, // ս��
    Shop = 1, // �̵�
    Boss = 2, //boss

    /*Rest = 3, // Ӫ��
    Elite = 4, // ��Ӣ
    Random = 5,//���
    Treasure = 6, // ����*/
}

public class Node : MonoBehaviour
{
    /// <summary>
    /// ͨ�ظýڵ�ؿ���õĽ������
    /// </summary>
    public int value = 0;

    /// <summary>
    /// �Ƿ�Ϊ��ͨ�ؿ���ÿһ��ս���ؿ�����Normal��Expert��
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
    /// �ýڵ��Ƿ�ѡ�У���Ϊ���ε�ͼ��ʾ�Ľڵ�
    /// </summary>
    public bool IsSeleced = true;
    /// <summary>
    /// ���ݽ����жϵ�ǰ�ڵ��Ƿ���Ե��
    /// </summary>
    public bool IsActive = false;
    /// <summary>
    /// ��Ϊս���ڵ��Ӧ������
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
