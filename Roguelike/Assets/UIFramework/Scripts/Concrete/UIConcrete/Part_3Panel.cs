using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��ʼ������
/// </summary>
public class Part_3Panel : BasePanel
{
    static readonly string path = "Prefab/Panel/Part_3Panel";

    public Part_3Panel() : base(new UIType(path)) { }

    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<Button>("ContinueButton").onClick.AddListener(() =>
        {
            GameRoot.Instance.sceneSystem.SetScene(new StoreScene());
        });
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnPause()
    {
        base.OnPause();
    }

    public override void OnResume()
    {
        base.OnResume();
    }
}
