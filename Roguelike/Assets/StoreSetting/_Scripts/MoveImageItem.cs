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

    //记录玩家开始拖拽时的位置
    private Vector3 vector;

    //需要移动物品的位置组件
    private RectTransform rectTransform;

    //UI事件管理器
    private EventSystem _EventSystem;
    private GraphicRaycaster gra;

    //private CanvasGroup canvasGroup;

    private void Awake()

    {
        //场景中要有EventSystem
        _EventSystem = FindObjectOfType<EventSystem>();
        //主场景的射线检测器
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
        levelText.text = $"等级：{level}";
        costText.text = $"花费：{3}";
    }

    /// <summary>
    /// 开始拖拽时执行一次，保留当前UI元素的初始位置，若移动后物体下方的物体标签不为Slot，则返回在此时保存的初始位置
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
    /// 拖拽时持续调用
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        this.rectTransform.anchoredPosition += eventData.delta;
    }

    /// <summary>
    /// 结束拖拽时执行一次
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        isMoving = false;
        //Debug.Log("End Dragging");

        //判断是否拖拽到格子上
        bool isSolt = false;

        //保存格子的位置
        Vector3 SlotVector = new Vector3();

        //通过定义的读取方法读取鼠标所在位置的UI对象
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

            
            //检测是否在物品上
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

            //检测是否在格子上
            else if (item.gameObject.tag == "PlayerSlot")
            {
                //Debug.Log("test");
                //如果在格子上则将isSlot设置为true方便后续代码
                isSolt = true;

                isInTeam = true;

                //保存格子位置坐标以便切换位置
                SlotVector = item.gameObject.transform.position;
            }

        }

        if (GameRoot.Progress.currentCoin < PlayerBase.cost && !StoreSceneManager.instance.test) isSolt = false;
        //判断是否检测在格子上
        if (isSolt)
        {
            //如果在格子上,则切换到格子上
            this.rectTransform.position = SlotVector;

            //------------------------金币计算相关------------------------
            if(SceneManager.GetActiveScene().name == "StoreScene") 
            {
                if (purchaseState) GameRoot.Progress.currentCoin -= PlayerBase.cost;
            }
        }
        else
        {
            //如果不在格子上,则返回原位置
            this.rectTransform.position = vector;
        }
        PlayerTeamSlotDetect.Instance.ItemInSlotDetect();
        //canvasGroup.blocksRaycasts = true;
        //ClearLog();
        UpdateShowPanel();
    }

    /// <summary>
    /// 定义通过射线读取所在位置的UI对象
    /// </summary>
    /// <param name="pos">射线位置</param>
    /// <returns>返回读取的所有UI对象</returns>
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
                //先加载价格再脱离父物体
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
        // 获取UI元素的画布组件
        Canvas canvas = element.GetComponentInParent<Canvas>();

        // 修改UI元素的渲染顺序
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