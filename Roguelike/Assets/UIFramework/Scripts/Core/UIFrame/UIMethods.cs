using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI�е��õķ���
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
    /// ��ó����е�Canvas
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

        Debug.Log("�����в�������ΪMainCanvas�Ļ���");
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
        Debug.LogWarning($"{parentPanel.name}��û���ҵ�{childName}����");
        return null;
    }

    public T AddOrGetComponent<T>(GameObject go) where T : Component
    {
        if (go.GetComponent<T>() == null)
        {
            Debug.Log($"{go.name}�����ϲ�����Ŀ�����");
            go.AddComponent<T>();
            Debug.Log($"�Ѿ�Ϊ{go.name}�������Ŀ�����");
        }
        return go.GetComponent<T>();
    }

    /// <summary>
    /// ��ȡ����Ӻ��ӵ����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="go">������</param>
    /// <param name="childName">Ѱ�ҵĺ��ӵ�����</param>
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
        Debug.LogWarning($"{go.name}��û���ҵ�{childName}����");
        return null;
    }
}
