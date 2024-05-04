using System.Collections;

using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class MoveImageItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int level = 1;
    bool isInItem = false;
    bool costPanelChange = false;
    public bool isInTeam = false;
    bool purchaseState;
    bool isMoving = false;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI costText;
    public GameObject showPanel;

    //��¼��ҿ�ʼ��קʱ��λ��
    private Vector3 vector;

    //��Ҫ�ƶ���Ʒ��λ�����
    private RectTransform rectTransform;

    //UI�¼�������
    private EventSystem _EventSystem;
    private GraphicRaycaster gra;

    //private CanvasGroup canvasGroup;

    private void Awake()

    {
        //������Ҫ��EventSystem
        _EventSystem = FindObjectOfType<EventSystem>();
        //�����������߼����
        if(SceneManager.GetActiveScene().name == "StoreScene")
        {
            gra = GameObject.Find("StorePanel").GetComponent<GraphicRaycaster>();
        }
        else
        {
            gra = GameObject.Find("DropPanel").GetComponent<GraphicRaycaster>();
        }
        
        rectTransform = GetComponent<RectTransform>();
        //canvasGroup = GetComponent<CanvasGroup>();
        //Debug.Log(1);
        StartCoroutine(IE_wait());
    }

    IEnumerator IE_wait()
    {
        showPanel.SetActive(true);
        yield return null;
        UpdateShowPanel();
        showPanel.SetActive(false);
    }
    
    public void UpdateShowPanel()
    {
        levelText.text = $"�ȼ���{level}";
        costText.text = $"���ѣ�{3}";
    }

    /// <summary>
    /// ��ʼ��קʱִ��һ�Σ�������ǰUIԪ�صĳ�ʼλ�ã����ƶ��������·��������ǩ��ΪSlot���򷵻��ڴ�ʱ����ĳ�ʼλ��
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        isMoving = true;
        vector = this.transform.position;
        purchaseState = false;
        if (!isInTeam) purchaseState = true;
        //canvasGroup.blocksRaycasts = false;
        Transform[] allGameObjects = GetComponentsInChildren<Transform>(true);
        foreach (Transform item in allGameObjects)
        {
            GameObject itemGamObject = item.gameObject;
            if (itemGamObject.name == "ShowPanel")
            {
                //Debug.Log("false");
                itemGamObject.SetActive(false);
                //SetSortingOrder(itemGamObject, 5);
            }
        }
    }



    /// <summary>
    /// ��קʱ��������
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        this.rectTransform.anchoredPosition += eventData.delta;
    }

    /// <summary>
    /// ������קʱִ��һ��
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        isMoving = false;
        //Debug.Log("End Dragging");

        //�ж��Ƿ���ק��������
        bool isSolt = false;

        //������ӵ�λ��
        Vector3 SlotVector = new Vector3();

        //ͨ������Ķ�ȡ������ȡ�������λ�õ�UI����
        var list = GraphicRaycaster(Input.mousePosition);
        /*for (int i = 0; i < list.Count; ++i)
        {
            Debug.Log(list[i].gameObject.name);
        }*/
        foreach (var item in list)
        {
            if (item.gameObject == gameObject) continue;
            if (item.gameObject.tag == "Delete" && isInTeam)
            {
                Destroy(this.gameObject);
            }

            
            //����Ƿ�����Ʒ��
            if (item.gameObject.tag == "Item")
            {
                MoveImageItem imageItem = item.gameObject.GetComponent<MoveImageItem>();
                if (item.gameObject.GetComponent<Image>().name == GetComponent<Image>().name && imageItem.level == level && level <= 3 && imageItem.isInTeam)
                {
                    imageItem.level++;
                    imageItem.UpdateShowPanel();
                    Destroy(gameObject);
                }
                else
                {
                    if (!isInTeam)
                    {
                        isSolt = false;
                        break;
                    }
                    this.rectTransform.position = item.gameObject.transform.position;
                    item.gameObject.transform.position = vector;
                }
            }

            //����Ƿ��ڸ�����
            else if (item.gameObject.tag == "PlayerSlot")
            {
                //Debug.Log("test");
                //����ڸ�������isSlot����Ϊtrue�����������
                isSolt = true;

                isInTeam = true;

                //�������λ�������Ա��л�λ��
                SlotVector = item.gameObject.transform.position;
            }

        }

        if (GameRoot.Progress.currentCoin < PlayerBase.cost && !StoreSceneManager.instance.test) isSolt = false;
        //�ж��Ƿ����ڸ�����
        if (isSolt)
        {
            //����ڸ�����,���л���������
            this.rectTransform.position = SlotVector;

            //------------------------��Ҽ������------------------------
            if(SceneManager.GetActiveScene().name == "StoreScene") 
            {
                if (purchaseState) GameRoot.Progress.currentCoin -= PlayerBase.cost;
            }
        }
        else
        {
            //������ڸ�����,�򷵻�ԭλ��
            this.rectTransform.position = vector;
        }
        PlayerTeamSlotDetect.Instance.ItemInSlotDetect();
        //canvasGroup.blocksRaycasts = true;
        //ClearLog();
        UpdateShowPanel();
    }

    /// <summary>
    /// ����ͨ�����߶�ȡ����λ�õ�UI����
    /// </summary>
    /// <param name="pos">����λ��</param>
    /// <returns>���ض�ȡ������UI����</returns>
    private List<RaycastResult> GraphicRaycaster(Vector2 pos)
    {
        var mPointerEventData = new PointerEventData(_EventSystem);
        mPointerEventData.position = pos;
        List<RaycastResult> results = new List<RaycastResult>();
        gra.Raycast(mPointerEventData, results);
        return results;
    }

    void ClearLog()
    {
        System.Reflection.Assembly assembly = System.Reflection.Assembly.GetAssembly(typeof(UnityEditor.SceneView));
        System.Type logEntries = assembly.GetType("UnityEditor.LogEntries");
        System.Reflection.MethodInfo clearConsoleMethod = logEntries.GetMethod("Clear");
        clearConsoleMethod.Invoke(new object(), null);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log($"OnPointerEnter {this.gameObject.name}");
        if(isMoving == false && !isInItem)
        {
            Transform[] allGameObjects = GetComponentsInChildren<Transform>(true);
            foreach (Transform item in allGameObjects)
            {        
                GameObject itemGamObject = item.gameObject;
                if (itemGamObject.name == "ShowPanel")
                {
                    itemGamObject.SetActive(true);
                    SetSortingOrder(itemGamObject, 5);
                }
                //�ȼ��ؼ۸������븸����
                if (itemGamObject.name == "CostPanel" && !costPanelChange)
                {
                    itemGamObject.transform.SetParent(this.transform.parent);
                    costPanelChange = true;
                }
            }
        }
        isInItem = true;
    }

    void SetSortingOrder(GameObject element, int sortingOrder)
    {
        // ��ȡUIԪ�صĻ������
        Canvas canvas = element.GetComponentInParent<Canvas>();

        // �޸�UIԪ�ص���Ⱦ˳��
        canvas.overrideSorting = true;
        canvas.sortingOrder = sortingOrder;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isInItem = false;
        //Debug.Log($"OnPointerExit {this.gameObject.name}");
        Transform[] allGameObjects = GetComponentsInChildren<Transform>(true);
        foreach (Transform item in allGameObjects)
        {
            GameObject itemGamObject = item.gameObject;
            if (itemGamObject.name == "ShowPanel")
            {
                //Debug.Log("false");
                itemGamObject.SetActive(false);
                //SetSortingOrder(itemGamObject, 5);
            }
        }
    }
}