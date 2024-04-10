using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��Ϸ��ʧ�ܵ�����UI����
/// </summary>
public class FailPanel : BasePanel
{
    static readonly string path = "Prefab/Panel/FailPanel";

    public FailPanel() : base(new UIType(path)) { }

    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<Button>("BackButton").onClick.AddListener(() =>
        {
            GameRoot.Instance.sceneSystem.SetScene(new StartScene());
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
