using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHighlightAction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button button;
    public GameObject show;

    private void Start()
    {
        // 获取按钮的EventTrigger组件
        EventTrigger eventTrigger = button.GetComponent<EventTrigger>();

        // 创建一个Entry并添加高亮事件的回调函数
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((data) => { OnButtonHighlighted((PointerEventData)data); });

        // 将Entry添加到EventTrigger的triggers列表中
        eventTrigger.triggers.Add(entry);
    }

    public void OnButtonHighlighted(PointerEventData eventData)
    {
        // 当按钮高亮时执行的操作
        //Debug.Log("Button Highlighted!");
        // 在这里添加你想要执行的代码
        show.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 当鼠标指针离开按钮时执行的操作
        //Debug.Log("Button Unhighlighted!");
        // 在这里添加你想要执行的代码
        show.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }
}