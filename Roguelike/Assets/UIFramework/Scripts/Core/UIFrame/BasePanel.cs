using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Panel父类
/// </summary>
public class BasePanel
{
    public UIType uiType;

    [HideInInspector]
    /// <summary>
    /// 此Panel在场景里面对应的物体
    /// </summary>
    public GameObject ActiveObj;

    public BasePanel(UIType uiType)
    {
        this.uiType = uiType;
    }

    private void Awake()
    {
        ActiveObj = new GameObject();
    }

    public virtual void OnStart()
    {
        Debug.Log($"{uiType.Name}开始使用");
        UIMethods.GetInstance().AddOrGetComponent<CanvasGroup>(ActiveObj).interactable = true;
    }

    public virtual void OnEable()
    {
        
        UIMethods.GetInstance().AddOrGetComponent<CanvasGroup>(ActiveObj).interactable = true;
    }

    public virtual void OnDisabled()
    {
        UIMethods.GetInstance().AddOrGetComponent<CanvasGroup>(ActiveObj).interactable = false;
    }

    public virtual void OnDestory()
    {
        UIMethods.GetInstance().AddOrGetComponent<CanvasGroup>(ActiveObj).interactable = false;
        
    }
}
