using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    

    public Vector3 position;
    public E_NodeType type = E_NodeType.Battle;
    public Image uiImage;
    public Sprite icon;
    public List<Node> upperNodes;
    public List<Node> lowerNodes;
    public List<GameObject> lineUIs;
    /// <summary>
    /// �ýڵ��Ƿ�ѡ��
    /// </summary>
    public bool IsSeleced = false;
    public bool IsActive = false;

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
