using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI中调用的方法
/// </summary>
public class UIMethods
{
    private static UIMethods instance;

    public static UIMethods GetInstance()
    {
        if(instance == null)
        {
            instance = new UIMethods();
        }
        return instance;
    }

    /// <summary>
    /// 获得场景中的Canvas
    /// </summary>
    /// <returns></returns>
    public GameObject FindMainCanvas()
    {
        Canvas[] canvas = GameObject.FindObjectsOfType<Canvas>();
        foreach (Canvas c in canvas)
        {
            if (c.gameObject.name == "MainCanvas")
            {
                return c.gameObject;
            }
        }

        Debug.Log("场景中不存在名为MainCanvas的画布");
        return null;
    }

    public GameObject FindObjectInChildren(GameObject parentPanel, string childName)
    {
        Transform[] transforms = parentPanel.GetComponentsInChildren<Transform>();

        foreach(Transform t in transforms)
        {
            if(t.gameObject.name == childName)
            {
                return t.gameObject;
            }
        }
        Debug.LogWarning($"{parentPanel.name}中没有找到{childName}物体");
        return null;
    }

    public T AddOrGetComponent<T>(GameObject go) where T : Component
    {
        if (go.GetComponent<T>() == null)
        {
            Debug.Log($"{go.name}物体上不存在目标组件");
            go.AddComponent<T>();
            Debug.Log($"已经为{go.name}物体添加目标组件");
        }
        return go.GetComponent<T>();
    }

    /// <summary>
    /// 获取或添加孩子的组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="go">父对象</param>
    /// <param name="childName">寻找的孩子的名称</param>
    /// <returns></returns>
    public T AddOrGetComponentInChildren<T>(GameObject go, string childName) where T : Component
    {
        Transform[] transforms = go.GetComponentsInChildren<Transform>();

        foreach (Transform t in transforms)
        {
            if (t.gameObject.name == childName)
            {
                return AddOrGetComponent<T>(t.gameObject);
            }
        }
        Debug.LogWarning($"{go.name}中没有找到{childName}物体");
        return null;
    }
}
