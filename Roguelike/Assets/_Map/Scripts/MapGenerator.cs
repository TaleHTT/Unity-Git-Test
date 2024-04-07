using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MapGenerator : MonoBehaviour
{
    private bool isMapGenerateCompelete = false;

    /// <summary>
    /// 地图预制体，子物体应由Node组成，Node的位置对应的就是预设的可以生成节点的位置
    /// </summary>
    public GameObject mapPrefab;
    private GameObject currentMap;
    /// <summary>
    /// Boss图标
    /// </summary>
    public Sprite bossSprite;

    /// <summary>
    /// 具体图标，索引与E_NodeType枚举对应
    /// </summary>
    public Sprite[] icons;

    /// <summary>
    /// 节点图像
    /// </summary>
    public GameObject nodePrefab;
    /// <summary>
    /// 道路连线预制体，挂在UILine即可
    /// </summary>
    public GameObject linePrefab;

    /// <summary>
    /// 道路连接层级
    /// </summary>
    public Transform LineParent;
    /// <summary>
    /// 父节点
    /// </summary>
    public Transform nodesParent;

    float battleProb = 0.7f; // 战斗节点的概率
    float shopProb = 0.3f; // 商店节点的概率
    const float RANDOMRANGE_ICON = 40;  //Icon
    const float RANDOMRANGE_LINE = 8;   //line

    /// <summary>
    /// 地图的层数
    /// </summary>
    [Header("地图参数")]
    private int LAYERS = 5;
    /// <summary> 实际预制体每层的节点数 </summary>
    private int LAYER_NODES = 7;
    /// <summary> 限制每层最多出现的节点数 </summary>
    const int MAXNODES = 6;
    /// <summary> 限制每层最少出现的节点数 </summary>
    const int MINNODES = 2;

    /// <summary>
    /// 存储地图上节点二维列表
    /// </summary>
    List<List<Node>> nodes = new List<List<Node>>();

    public void Awake()
    {
        StartCoroutine(IE_InitMap());
    }

    /// <summary>
    /// 开始方法，触发时生成地图
    /// </summary>
    public void StartGenerate()
    {
        Debug.Log("Start");
        StartCoroutine(IE_StartGenerate());
    }

    /// <summary>
    /// 获取mapPrefab预制体，生成地图，给二维Node列表对应上每个具体的Node
    /// </summary>
    /// <returns></returns>
    IEnumerator IE_InitMap()
    {
        isMapGenerateCompelete = false;

        if (currentMap != null) Destroy(currentMap);

        int count = LineParent.transform.childCount;
        if(count != 0) foreach(Transform child in LineParent) Destroy(child.gameObject);
        yield return new WaitForSeconds(0.5f);

        currentMap = Instantiate(mapPrefab, nodesParent);
        nodes.Clear();

        List<Node> tempNodes = new List<Node>(); //暂时装currentmap上所有的节点
        tempNodes.AddRange(nodesParent.GetComponentsInChildren<Node>(true));

        //转移到二维nodes节点上
        for(int i = 0; i < LAYERS; i++)
        {
            nodes.Add(new List<Node>());
            for(int j = 0; j < LAYER_NODES; j++)
            {
               nodes[i].Add(tempNodes[i * LAYER_NODES + j]);
            }
        }
        isMapGenerateCompelete = true;
    }

    public void Clear()
    {
        Debug.Log("Clear");
        StartCoroutine(IE_InitMap());
    }

    IEnumerator IE_StartGenerate()
    {
        yield return new WaitUntil(() => this.isMapGenerateCompelete == true);

        //生成节点
        GenerateNodes();

        AssignNodeType();

        AssignNodeIcon();

        CreateNodeUI();

        CreateLineUI();
    }

    /// <summary>
    /// 为每一对相连的节点创建一条UI线段
    /// </summary>
    private void CreateLineUI()
    {
        for(int i = 0; i < LAYERS; i++)
        {
            foreach(var node in nodes[i])
            {
                if (!node.IsSeleced) continue;
                foreach(var lowerNode in node.lowerNodes)
                {
                    //UI线段，作为两个节点之间的图形表示
                    GameObject lineUI = Instantiate(linePrefab, LineParent);
                    //获取UI线段上的LineRenderer组件，设置起点和重点两个节点的位置
                    UILine myLine = lineUI.GetComponent<UILine>();
                    GeneratePositions(myLine, node.position, lowerNode.position);

                    node.lineUIs.Add(lineUI);
                }
            }
        }
    }

    private void GeneratePositions(UILine line, Vector3 source, Vector3 Destination)
    {
        float segement = (int)((Destination - source).magnitude / 30);
        line.PositionCount = (int)segement + 1;
        for(int i = 0; i <= segement; i++)
        {
            //根据插值将一个图标生成线段路线
            Vector3 temp = Vector3.Lerp(source, Destination, i / segement);
            temp += new Vector3(UnityEngine.Random.Range(-RANDOMRANGE_LINE, RANDOMRANGE_LINE), UnityEngine.Random.Range(-RANDOMRANGE_LINE, RANDOMRANGE_LINE));
            line.SetPosition((int)i, temp);
        }
    }

    /// <summary>
    /// 为每一个节点创建UI元素
    /// </summary>
    private void CreateNodeUI()
    {
        for(int i = 0; i < LAYERS - 1;i++) 
        {
            foreach(var node in nodes[i])
            {
                if (!node.IsSeleced) continue;
                GameObject nodeUI = Instantiate(nodePrefab, node.transform);
                nodeUI.transform.position += new Vector3(UnityEngine.Random.Range(-RANDOMRANGE_ICON, RANDOMRANGE_ICON), UnityEngine.Random.Range(-RANDOMRANGE_ICON, RANDOMRANGE_ICON));
                node.position = nodeUI.transform.position;
                nodeUI.GetComponent<Image>().sprite = node.icon;
                node.uiImage = nodeUI.GetComponent<Image>();
                // 获取UI元素上的Button组件，添加一个点击事件，调用SelectNode方法
                ///Button button = nodeUI.GetComponent<Button>();
                //button.onClick.AddListener(() => SelectNode(node));
                // 将UI元素赋值给节点的ui属性
                // node.ui = nodeUI;
            }
        }
    }

    /// <summary>
    /// 节点图标分配
    /// </summary>
    public void AssignNodeIcon()
    {
        for(int i = 0; i < LAYERS - 1; i++)
        {
            foreach(var node in nodes[i])
            {
                if (!node.IsSeleced) continue;

                switch (node.type)
                {
                    case E_NodeType.Battle:
                        node.icon = icons[(int)E_NodeType.Battle];
                        break;
                    case E_NodeType.Shop:
                        node.icon = icons[(int)E_NodeType.Shop];
                        break;
                }
            }
        }
    }

    /// <summary>
    /// 根据概率和限制，为每层的节点随机分派一个类型
    /// </summary>
    private void AssignNodeType()
    {
        for(int i = 0; i < LAYERS - 1; i++)
        {
            #region 特殊节点安在随机节点类型分配之前

            if (i== 0)   //第一层必定为普通怪物
            {
                foreach(var node in nodes[i])
                {
                    if(node.IsSeleced) node.type = E_NodeType.Battle;
                }
                continue;
            }

            #endregion

            //int[] typeCount = new int[2];
            List<Node> thisLayerNode = new List<Node>();
            foreach(var node in nodes[i])
            {
                if (!node.IsSeleced) continue;
                float r = (float)UnityEngine.Random.Range(0f, 1f);
                thisLayerNode.Add(node);
                if(r < battleProb)
                {
                    node.type = E_NodeType.Battle;
                    //typeCount[(int)E_NodeType.Battle]++;
                }
                else
                {
                    node.type = E_NodeType.Shop;
                    //typeCount[(int)E_NodeType.Shop]++;
                }
            }

        }
    }
    /// <summary>
    /// 生成节点
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="MaxNode"></param>
    public void GenerateNodes()
    {
        Debug.Log("Start GenerateNodes");
        //在最高层中间位置生成boss节点，作为起始节点
        if (nodes[LAYERS - 1][LAYER_NODES / 2] != null)
        {
            SetBoss(nodes[LAYERS - 1][LAYER_NODES / 2]);
        }
        else
        {
            Debug.Log("StartNode is null");
            return;
        }

        //从Boss层的下一层开始循环产生节点
        int lastNodeNum = MAXNODES;
        for(int i = LAYERS - 2; i >= 0; i--)
        {
            ConnectNodes(i, ref lastNodeNum);
        }
    }

    /// <summary>
    /// 连接当前层数的节点
    /// </summary>
    /// <param name="layer">层数</param>
    /// <param name="MaxNode">上一次的节点数传入，第一次传入为MAXNODES大小</param>
    private void ConnectNodes(int layer, ref int lastNodeNum)
    {
        int maxNodeNum = MAXNODES, minNodeNum = MINNODES;
        #region 处理并获取当前层节点数量多少的边界，并获取当前层数接受的节点数
        if(lastNodeNum * 2 >= MAXNODES)
        {
            lastNodeNum = MAXNODES;
        }
        else
        {
            lastNodeNum *= 2;
        }
        maxNodeNum = lastNodeNum;
        minNodeNum = Mathf.Max((maxNodeNum + 1) / 2, MINNODES);

        int thisLayerContainCount = UnityEngine.Random.Range(minNodeNum, maxNodeNum);
        #endregion

        #region 节点连线连接规则
        int upIndex = 0;    //上一层索引
        //通过upIndex索引 枚举上一层节点，对IsSeleced为true的节点进行处理
        foreach(var father in nodes[layer + 1])
        {
            //跳过未被选中的上一层中的节点
            if (father.IsSeleced == false)
            {
                upIndex++;
                continue;
            }

            //对Boss的特殊处理
            if(father.type == E_NodeType.Boss)
            {
                for(int j = 0; j < maxNodeNum; j++)
                {
                    GenerateConnection(layer, upIndex, father, MAXNODES+1, 0, ref thisLayerContainCount);
                    //Debug.Log("GenerateConnection未实现");
                }
                break;  
            }

            int upperNodeCount = UnityEngine.Random.Range(1, 4); //该父节点下可以连接的字节点数量
            //如果是最外层节点，只能连到一个下层节点
            if(upIndex == 0 || upIndex == LAYER_NODES - 1)
            {
                upperNodeCount = 1;
            }
            //
            for(int j = 0; j < upperNodeCount; j++)
            {
                //对在不同位置的父节点实行不同处理
                int max;
                int min;
                if (upIndex == 0)
                {
                    //在最左侧的父节点只能连upIndex和upIndex+1位置的节点，即0， 1
                    min = upIndex;
                    max = upIndex + 2;
                    GenerateConnection(layer, upIndex, father, max, min, ref thisLayerContainCount);

                }
                else if (upIndex == LAYER_NODES - 1)
                {
                    //最右侧的父节点只能连upIndex和upIndex - 1位置的节点，即LAYER-NODES - 1 和 LAYER-NODES - 2 
                    min = upIndex - 1;
                    max = upIndex + 1;
                    GenerateConnection(layer, upIndex, father, max, min, ref thisLayerContainCount);
                }
                else
                {
                    //其他在中间位置的节点可以连upIndex-1,upIndex,upIndex+1三个位置的节点
                    min = upIndex - 1;
                    max = upIndex + 2;
                    GenerateConnection(layer, upIndex, father, max, min, ref thisLayerContainCount);
                }
            }
            upIndex++;
        }


        #endregion

    }

    /// <summary>
    /// 为当前层节点和上一层节点之间生成连线
    /// </summary>
    /// <param name="layer">当前层</param>
    /// <param name="upIndex">上一层的索引枚举</param>
    /// <param name="father">父节点</param>
    /// <param name="max">索引上界，不包括</param>
    /// <param name="min">索引下界</param>
    /// <param name="Count"></param>
    public void GenerateConnection(int layer, int upIndex, Node father, int max, int min, ref int thisLayerContainCount)
    {
        int index = UnityEngine.Random.Range(min, max);

        #region 已经用完所有节点，对最后一个节点的处理

        //已经用完所有节点，寻找最后一个子节点 连线
        if (thisLayerContainCount == 0)
        {
            Node lastChildNode = new Node(Vector2.zero);
            //寻找当前层最后一个节点
            foreach(var temp in nodes[layer])
            {
                if (temp.IsSeleced) lastChildNode = temp;
            }
            if (father.lowerNodes.Contains(lastChildNode)) return;

            father.lowerNodes.Add(lastChildNode);
            lastChildNode.upperNodes.Add(father);
            return;
        }

        #endregion

        #region 一般情况下对layer层下的节点的处理

        //进行至多15次的index随机获取，
        int limit = 0;
        while (nodes[layer][index].IsSeleced == true || IsCrossing(layer, index, upIndex))
        {
            index = UnityEngine.Random.Range(min, max);
            limit++;
            if (limit > 15)
            {
                return;
            }
        }

        if (nodes[layer][index].IsSeleced == false)
        {
            nodes[layer][index].IsSeleced = true;
            thisLayerContainCount--;
        }

        //建立连接
        father.lowerNodes.Add(nodes[layer][index]);
        nodes[layer][index].upperNodes.Add(father);

        #endregion
    }

    /// <summary>
    /// layer层上相邻节点向上引出连线的时候 是否会出现交叉的判断，避免实际图像中出现交叉的路径
    /// </summary>
    /// <param name="layer">当前层</param>
    /// <param name="lowerIndex">较小的索引</param>
    /// <param name="upperIndex">较大的索引</param>
    /// <returns></returns>
    private bool IsCrossing(int layer, int lowerIndex, int upperIndex)
    {
        if(lowerIndex == upperIndex)
        {
            return false;
        }
        else
        {
            if (lowerIndex == upperIndex - 1)
            {
                if (nodes[layer + 1][upperIndex - 1].IsSeleced == true)
                {
                    if (nodes[layer + 1][upperIndex - 1].lowerNodes.Contains(nodes[layer][upperIndex]))
                    {
                        return true;
                    }

                }
            }
            if (lowerIndex == upperIndex + 1)
            {
                if (nodes[layer + 1][upperIndex + 1].IsSeleced == true)
                {
                    if (nodes[layer + 1][upperIndex + 1].lowerNodes.Contains(nodes[layer][upperIndex]))
                    {
                        return true;
                    }

                }
            }
        }
        return false;
    }
    /// <summary>
    /// 对某个节点设置为boss节点
    /// </summary>
    /// <param name="node"></param>
    public void SetBoss(Node node)
    {
        GameObject nodeUI = Instantiate(nodePrefab, node.transform);

        node.type = E_NodeType.Boss;
        node.uiImage = nodeUI.GetComponent<Image>();
        node.IsSeleced = true;
        node.icon = bossSprite;

        nodeUI.GetComponent<Image>().sprite = node.icon;
        nodeUI.GetComponent<Image>().SetNativeSize();
    }
}