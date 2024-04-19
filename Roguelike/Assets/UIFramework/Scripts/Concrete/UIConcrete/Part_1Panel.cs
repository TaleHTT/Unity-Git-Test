using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 开始主界面
/// </summary>
public class Part_1Panel : BasePanel
{
    static readonly string path = "Prefab/Panel/Part_1Panel";

    public Part_1Panel() : base(new UIType(path)) { }

    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<Button>("ContinueButton").onClick.AddListener(() =>
        {
            GameRoot.Progress.currentLevel++;
            GameRoot.Progress.SaveData();
            MapGenerator.Instance.NodeLevelSet(GameRoot.Progress.currentLevel);
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
