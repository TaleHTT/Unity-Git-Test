using System.Collections;

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.EventSystems;

using UnityEngine.UI;



public class MoveImageItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool isInTeam = false;
    bool purchaseState;

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
        gra = FindObjectOfType<GraphicRaycaster>();
        rectTransform = GetComponent<RectTransform>();
        //canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// 开始拖拽时执行一次，保留当前UI元素的初始位置，若移动后物体下方的物体标签不为Slot，则返回在此时保存的初始位置
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        vector = this.transform.position;
        purchaseState = false;
        if (!isInTeam) purchaseState = true; 
        //canvasGroup.blocksRaycasts = false;
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
        Debug.Log("End Dragging");

        //判断是否拖拽到格子上
        bool isSolt = false;

        //保存格子的位置
        Vector3 SlotVector = new Vector3();

        //通过定义的读取方法读取鼠标所在位置的UI对象
        var list = GraphicRaycaster(Input.mousePosition);

        foreach (var item in list)
        {
            if (item.gameObject == gameObject) continue;
            //检测是否在物品上
            if (item.gameObject.tag == "Item")
            {
                if (!isInTeam)
                {
                    isSolt = false;
                    break;
                }
                //如果在物品上则执行以下代码
                //---交换位置---//
                this.rectTransform.position = item.gameObject.transform.position;
                item.gameObject.transform.position = vector;
            }

            //检测是否在格子上
            else if (item.gameObject.tag == "PlayerSlot")
            {
                Debug.Log("test");
                //如果在格子上则将isSlot设置为true方便后续代码
                isSolt = true;

                isInTeam = true;

                //保存格子位置坐标以便切换位置
                SlotVector = item.gameObject.transform.position;
            }

        }


        if (GameRoot.Progress.currentCoin < PlayerBase.cost) isSolt = false;
        //判断是否检测在格子上
        if (isSolt)
        {
            //如果在格子上,则切换到格子上
            this.rectTransform.position = SlotVector;

            //------------------------金币计算相关------------------------
            if(purchaseState) GameRoot.Progress.currentCoin -= PlayerBase.cost;
        }
        else
        {
            //如果不在格子上,则返回原位置
            this.rectTransform.position = vector;
        }
        PlayerTeamSlotDetect.Instance.ItemInSlotDetect();
        //canvasGroup.blocksRaycasts = true;
        //ClearLog();
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
}