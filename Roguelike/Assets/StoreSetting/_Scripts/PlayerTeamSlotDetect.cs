using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerTeamSlotDetect : MonoBehaviour
{
    public static PlayerTeamSlotDetect Instance { get; private set; }
    /// <summary>
    /// 存Slot物体
    /// </summary>
    public GameObject[] playerTeamSlots;
    /// <summary>
    /// 存储场景中每个slot对应的image的GameObject
    /// </summary>
    public GameObject[] playersInTeam;
    private EventSystem _EventSystem;
    private GraphicRaycaster gra;
    public int globalMaxPlayerNum;

    private void Awake()
    {
        Instance = this;
        globalMaxPlayerNum = 6;
        //场景中要有EventSystem
        _EventSystem = FindObjectOfType<EventSystem>();
        //主场景的射线检测器
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
            Debug.Log("StoreScene 的 StorePanel 没有获取到6个Slot，请检查");
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
}
