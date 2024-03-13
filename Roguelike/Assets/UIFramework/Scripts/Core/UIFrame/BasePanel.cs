using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Panel����
/// </summary>
public class BasePanel
{
    public UIType uiType;

    [HideInInspector]
    /// <summary>
    /// ��Panel�ڳ��������Ӧ������
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
        Debug.Log($"{uiType.Name}��ʼʹ��");
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
