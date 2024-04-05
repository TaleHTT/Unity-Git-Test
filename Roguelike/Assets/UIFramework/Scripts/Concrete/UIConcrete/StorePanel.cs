using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 开始主界面
/// </summary>
public class StorePanel : BasePanel
{
    static readonly string path = "Prefab/Panel/StorePanel";

    public StorePanel() : base(new UIType(path)) { }

    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<Button>("ContinueButton").onClick.AddListener(() =>
        {
            GameRoot.Instance.SceneSystem.SetScene(new Part_1());
        });
        UITool.GetOrAddComponentInChildren<Button>("BackButton").onClick.AddListener(() =>
        {
            GameRoot.Instance.SceneSystem.SetScene(new StartScene());
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

    }
}