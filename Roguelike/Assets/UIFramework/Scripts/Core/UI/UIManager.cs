using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 存储所有UI信息，并可以创建或者销毁UI
/// </summary>
public class UIManager
{
    /// <summary>
    /// 存储所有UI信息的字典，每一个UI信息都会对应一个GameObject
    /// </summary>
    private Dictionary<UIType, GameObject> dicUI;

    public UIManager() 
    {
        dicUI = new Dictionary<UIType, GameObject>();
    }

    /// <summary>
    /// 获取一个UI对象
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public GameObject GetSingleUI(UIType type)
    {
        GameObject parent = GameObject.Find("Canvas");
        if (!parent)
        {
            Debug.LogError("Canvas不存在，请查找有无该对象");
            return null; 
        }
        if(dicUI.ContainsKey(type)) 
            return dicUI[type];
        GameObject ui = GameObject.Instantiate(Resources.Load<GameObject>(type.Path));
        ui.transform.SetParent(parent.transform, false);
        ui.name = type.Name;
        dicUI.Add(type, ui);
        return ui;
    }

    /// <summary>
    /// 销毁一个UI对象
    /// </summary>
    /// <param name="tyep">UI信息</param>
    public void DestroyUI(UIType type)
    {
        if (dicUI.ContainsKey(type))
        {
            GameObject.Destroy(dicUI[type]);
            dicUI.Remove(type);
        }
    }
}
