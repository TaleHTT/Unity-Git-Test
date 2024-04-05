using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��ʼ������
/// </summary>
public class ChoosePartPanel : BasePanel
{
    static readonly string path = "Prefab/Panel/ChoosePartPanel";

    public ChoosePartPanel() : base(new UIType(path)) { }

    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<Button>("Part_1").onClick.AddListener(() =>
        {
            GameRoot.Instance.SceneSystem.SetScene(new Part_1());
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
