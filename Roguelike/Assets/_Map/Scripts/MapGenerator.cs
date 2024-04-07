using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MapGenerator : MonoBehaviour
{
    private bool isMapGenerateCompelete = false;

    /// <summary>
    /// ��ͼԤ���壬������Ӧ��Node��ɣ�Node��λ�ö�Ӧ�ľ���Ԥ��Ŀ������ɽڵ��λ��
    /// </summary>
    public GameObject mapPrefab;
    private GameObject currentMap;
    /// <summary>
    /// Bossͼ��
    /// </summary>
    public Sprite bossSprite;

    /// <summary>
    /// ����ͼ�꣬������E_NodeTypeö�ٶ�Ӧ
    /// </summary>
    public Sprite[] icons;

    /// <summary>
    /// �ڵ�ͼ��
    /// </summary>
    public GameObject nodePrefab;
    /// <summary>
    /// ��·����Ԥ���壬����UILine����
    /// </summary>
    public GameObject linePrefab;

    /// <summary>
    /// ��·���Ӳ㼶
    /// </summary>
    public Transform LineParent;
    /// <summary>
    /// ���ڵ�
    /// </summary>
    public Transform nodesParent;

    float battleProb = 0.7f; // ս���ڵ�ĸ���
    float shopProb = 0.3f; // �̵�ڵ�ĸ���
    const float RANDOMRANGE_ICON = 40;  //Icon
    const float RANDOMRANGE_LINE = 8;   //line

    /// <summary>
    /// ��ͼ�Ĳ���
    /// </summary>
    [Header("��ͼ����")]
    private int LAYERS = 5;
    /// <summary> ʵ��Ԥ����ÿ��Ľڵ��� </summary>
    private int LAYER_NODES = 7;
    /// <summary> ����ÿ�������ֵĽڵ��� </summary>
    const int MAXNODES = 6;
    /// <summary> ����ÿ�����ٳ��ֵĽڵ��� </summary>
    const int MINNODES = 2;

    /// <summary>
    /// �洢��ͼ�Ͻڵ��ά�б�
    /// </summary>
    List<List<Node>> nodes = new List<List<Node>>();

    public void Awake()
    {
        StartCoroutine(IE_InitMap());
    }

    /// <summary>
    /// ��ʼ����������ʱ���ɵ�ͼ
    /// </summary>
    public void StartGenerate()
    {
        Debug.Log("Start");
        StartCoroutine(IE_StartGenerate());
    }

    /// <summary>
    /// ��ȡmapPrefabԤ���壬���ɵ�ͼ������άNode�б��Ӧ��ÿ�������Node
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

        List<Node> tempNodes = new List<Node>(); //��ʱװcurrentmap�����еĽڵ�
        tempNodes.AddRange(nodesParent.GetComponentsInChildren<Node>(true));

        //ת�Ƶ���άnodes�ڵ���
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

        //���ɽڵ�
        GenerateNodes();

        AssignNodeType();

        AssignNodeIcon();

        CreateNodeUI();

        CreateLineUI();
    }

    /// <summary>
    /// Ϊÿһ�������Ľڵ㴴��һ��UI�߶�
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
                    //UI�߶Σ���Ϊ�����ڵ�֮���ͼ�α�ʾ
                    GameObject lineUI = Instantiate(linePrefab, LineParent);
                    //��ȡUI�߶��ϵ�LineRenderer��������������ص������ڵ��λ��
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
            //���ݲ�ֵ��һ��ͼ�������߶�·��
            Vector3 temp = Vector3.Lerp(source, Destination, i / segement);
            temp += new Vector3(UnityEngine.Random.Range(-RANDOMRANGE_LINE, RANDOMRANGE_LINE), UnityEngine.Random.Range(-RANDOMRANGE_LINE, RANDOMRANGE_LINE));
            line.SetPosition((int)i, temp);
        }
    }

    /// <summary>
    /// Ϊÿһ���ڵ㴴��UIԪ��
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
                // ��ȡUIԪ���ϵ�Button��������һ������¼�������SelectNode����
                ///Button button = nodeUI.GetComponent<Button>();
                //button.onClick.AddListener(() => SelectNode(node));
                // ��UIԪ�ظ�ֵ���ڵ��ui����
                // node.ui = nodeUI;
            }
        }
    }

    /// <summary>
    /// �ڵ�ͼ�����
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
    /// ���ݸ��ʺ����ƣ�Ϊÿ��Ľڵ��������һ������
    /// </summary>
    private void AssignNodeType()
    {
        for(int i = 0; i < LAYERS - 1; i++)
        {
            #region ����ڵ㰲������ڵ����ͷ���֮ǰ

            if (i== 0)   //��һ��ض�Ϊ��ͨ����
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
    /// ���ɽڵ�
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="MaxNode"></param>
    public void GenerateNodes()
    {
        Debug.Log("Start GenerateNodes");
        //����߲��м�λ������boss�ڵ㣬��Ϊ��ʼ�ڵ�
        if (nodes[LAYERS - 1][LAYER_NODES / 2] != null)
        {
            SetBoss(nodes[LAYERS - 1][LAYER_NODES / 2]);
        }
        else
        {
            Debug.Log("StartNode is null");
            return;
        }

        //��Boss�����һ�㿪ʼѭ�������ڵ�
        int lastNodeNum = MAXNODES;
        for(int i = LAYERS - 2; i >= 0; i--)
        {
            ConnectNodes(i, ref lastNodeNum);
        }
    }

    /// <summary>
    /// ���ӵ�ǰ�����Ľڵ�
    /// </summary>
    /// <param name="layer">����</param>
    /// <param name="MaxNode">��һ�εĽڵ������룬��һ�δ���ΪMAXNODES��С</param>
    private void ConnectNodes(int layer, ref int lastNodeNum)
    {
        int maxNodeNum = MAXNODES, minNodeNum = MINNODES;
        #region ������ȡ��ǰ��ڵ��������ٵı߽磬����ȡ��ǰ�������ܵĽڵ���
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

        #region �ڵ��������ӹ���
        int upIndex = 0;    //��һ������
        //ͨ��upIndex���� ö����һ��ڵ㣬��IsSelecedΪtrue�Ľڵ���д���
        foreach(var father in nodes[layer + 1])
        {
            //����δ��ѡ�е���һ���еĽڵ�
            if (father.IsSeleced == false)
            {
                upIndex++;
                continue;
            }

            //��Boss�����⴦��
            if(father.type == E_NodeType.Boss)
            {
                for(int j = 0; j < maxNodeNum; j++)
                {
                    GenerateConnection(layer, upIndex, father, MAXNODES+1, 0, ref thisLayerContainCount);
                    //Debug.Log("GenerateConnectionδʵ��");
                }
                break;  
            }

            int upperNodeCount = UnityEngine.Random.Range(1, 4); //�ø��ڵ��¿������ӵ��ֽڵ�����
            //����������ڵ㣬ֻ������һ���²�ڵ�
            if(upIndex == 0 || upIndex == LAYER_NODES - 1)
            {
                upperNodeCount = 1;
            }
            //
            for(int j = 0; j < upperNodeCount; j++)
            {
                //���ڲ�ͬλ�õĸ��ڵ�ʵ�в�ͬ����
                int max;
                int min;
                if (upIndex == 0)
                {
                    //�������ĸ��ڵ�ֻ����upIndex��upIndex+1λ�õĽڵ㣬��0�� 1
                    min = upIndex;
                    max = upIndex + 2;
                    GenerateConnection(layer, upIndex, father, max, min, ref thisLayerContainCount);

                }
                else if (upIndex == LAYER_NODES - 1)
                {
                    //���Ҳ�ĸ��ڵ�ֻ����upIndex��upIndex - 1λ�õĽڵ㣬��LAYER-NODES - 1 �� LAYER-NODES - 2 
                    min = upIndex - 1;
                    max = upIndex + 1;
                    GenerateConnection(layer, upIndex, father, max, min, ref thisLayerContainCount);
                }
                else
                {
                    //�������м�λ�õĽڵ������upIndex-1,upIndex,upIndex+1����λ�õĽڵ�
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
    /// Ϊ��ǰ��ڵ����һ��ڵ�֮����������
    /// </summary>
    /// <param name="layer">��ǰ��</param>
    /// <param name="upIndex">��һ�������ö��</param>
    /// <param name="father">���ڵ�</param>
    /// <param name="max">�����Ͻ磬������</param>
    /// <param name="min">�����½�</param>
    /// <param name="Count"></param>
    public void GenerateConnection(int layer, int upIndex, Node father, int max, int min, ref int thisLayerContainCount)
    {
        int index = UnityEngine.Random.Range(min, max);

        #region �Ѿ��������нڵ㣬�����һ���ڵ�Ĵ���

        //�Ѿ��������нڵ㣬Ѱ�����һ���ӽڵ� ����
        if (thisLayerContainCount == 0)
        {
            Node lastChildNode = new Node(Vector2.zero);
            //Ѱ�ҵ�ǰ�����һ���ڵ�
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

        #region һ������¶�layer���µĽڵ�Ĵ���

        //��������15�ε�index�����ȡ��
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

        //��������
        father.lowerNodes.Add(nodes[layer][index]);
        nodes[layer][index].upperNodes.Add(father);

        #endregion
    }

    /// <summary>
    /// layer�������ڽڵ������������ߵ�ʱ�� �Ƿ����ֽ�����жϣ�����ʵ��ͼ���г��ֽ����·��
    /// </summary>
    /// <param name="layer">��ǰ��</param>
    /// <param name="lowerIndex">��С������</param>
    /// <param name="upperIndex">�ϴ������</param>
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
    /// ��ĳ���ڵ�����Ϊboss�ڵ�
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