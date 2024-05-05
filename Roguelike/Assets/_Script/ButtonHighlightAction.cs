using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHighlightAction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button button;
    public GameObject show;

    private void Start()
    {
        // ��ȡ��ť��EventTrigger���
        EventTrigger eventTrigger = button.GetComponent<EventTrigger>();

        // ����һ��Entry����Ӹ����¼��Ļص�����
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((data) => { OnButtonHighlighted((PointerEventData)data); });

        // ��Entry��ӵ�EventTrigger��triggers�б���
        eventTrigger.triggers.Add(entry);
    }

    public void OnButtonHighlighted(PointerEventData eventData)
    {
        // ����ť����ʱִ�еĲ���
        //Debug.Log("Button Highlighted!");
        // �������������Ҫִ�еĴ���
        show.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // �����ָ���뿪��ťʱִ�еĲ���
        //Debug.Log("Button Unhighlighted!");
        // �������������Ҫִ�еĴ���
        show.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }
}