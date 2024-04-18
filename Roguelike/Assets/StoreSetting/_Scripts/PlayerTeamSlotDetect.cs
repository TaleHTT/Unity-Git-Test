using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerTeamSlotDetect : MonoBehaviour
{
    public static PlayerTeamSlotDetect Instance { get; private set; }
    /// <summary>
    /// ��Slot����
    /// </summary>
    public GameObject[] playerTeamSlots;
    /// <summary>
    /// �洢������ÿ��slot��Ӧ��image��GameObject
    /// </summary>
    public GameObject[] playersInTeam;
    private EventSystem _EventSystem;
    private GraphicRaycaster gra;
    public int globalMaxPlayerNum;

    private void Awake()
    {
        Instance = this;
        globalMaxPlayerNum = 6;
        //������Ҫ��EventSystem
        _EventSystem = FindObjectOfType<EventSystem>();
        //�����������߼����
        gra = FindObjectOfType<GraphicRaycaster>();
    }

    private void Start()
    {
        playersInTeam = new GameObject[globalMaxPlayerNum];
        PlayerTeamSLotsInit();
    }

    private void PlayerTeamSLotsInit()
    {
        playerTeamSlots = new GameObject[globalMaxPlayerNum];
        int index = 0;
        foreach (Transform item in transform) 
        {
            if(item.gameObject.tag == "Slot")
            {
                playerTeamSlots[index] = item.gameObject;
                index++;
            }
        }
        if(index != globalMaxPlayerNum)
        {
            Debug.Log("StoreScene �� StorePanel û�л�ȡ��6��Slot������");
        }
    }

    public void ItemInSlotDetect()
    {
        for(int i = 0; i < globalMaxPlayerNum; i++)
        {
            foreach (var item in GraphicRaycaster(playerTeamSlots[i].transform.position))
            {
                if (item.gameObject == null)
                {
                    Debug.Log("item in GraphicRaycaster is null");
                }
                if (item.gameObject.tag == "Item")
                {
                    playersInTeam[i] = item.gameObject;
                    break;
                    //Debug.Log(playersInTeam[i].name);
                }
                else
                {
                    playersInTeam[i] = null;
                }
            }
            /*if (playersInTeam[i] != null)
                Debug.Log($"playersInTeam[{i}].name = " + playersInTeam[i].name);*/
        }
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
}
