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
        gra = FindObjectOfType<GraphicRaycaster>();
        rectTransform = GetComponent<RectTransform>();
        //canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// ��ʼ��קʱִ��һ�Σ�������ǰUIԪ�صĳ�ʼλ�ã����ƶ��������·��������ǩ��ΪSlot���򷵻��ڴ�ʱ����ĳ�ʼλ��
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
        Debug.Log("End Dragging");

        //�ж��Ƿ���ק��������
        bool isSolt = false;

        //������ӵ�λ��
        Vector3 SlotVector = new Vector3();

        //ͨ������Ķ�ȡ������ȡ�������λ�õ�UI����
        var list = GraphicRaycaster(Input.mousePosition);

        foreach (var item in list)
        {
            if (item.gameObject == gameObject) continue;
            //����Ƿ�����Ʒ��
            if (item.gameObject.tag == "Item")
            {
                if (!isInTeam)
                {
                    isSolt = false;
                    break;
                }
                //�������Ʒ����ִ�����´���
                //---����λ��---//
                this.rectTransform.position = item.gameObject.transform.position;
                item.gameObject.transform.position = vector;
            }

            //����Ƿ��ڸ�����
            else if (item.gameObject.tag == "PlayerSlot")
            {
                Debug.Log("test");
                //����ڸ�������isSlot����Ϊtrue�����������
                isSolt = true;

                isInTeam = true;

                //�������λ�������Ա��л�λ��
                SlotVector = item.gameObject.transform.position;
            }

        }


        if (GameRoot.Progress.currentCoin < PlayerBase.cost) isSolt = false;
        //�ж��Ƿ����ڸ�����
        if (isSolt)
        {
            //����ڸ�����,���л���������
            this.rectTransform.position = SlotVector;

            //------------------------��Ҽ������------------------------
            if(purchaseState) GameRoot.Progress.currentCoin -= PlayerBase.cost;
        }
        else
        {
            //������ڸ�����,�򷵻�ԭλ��
            this.rectTransform.position = vector;
        }
        PlayerTeamSlotDetect.Instance.ItemInSlotDetect();
        //canvasGroup.blocksRaycasts = true;
        //ClearLog();
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
}