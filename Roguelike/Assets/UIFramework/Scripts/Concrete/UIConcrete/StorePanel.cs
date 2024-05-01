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
            GameRoot.Progress.currentLevel++;
            MapGenerator.Instance.NodeLevelSet(GameRoot.Progress.currentLevel);
            GameRoot.Instance.mapGenerator.SetActive(true);
            GameRoot.SaveData();
            //GameRoot.Instance.SceneSystem.SetScene(new Part_1());
        });
        /*UITool.GetOrAddComponentInChildren<Button>("BackButton").onClick.AddListener(() =>
        {
            GameRoot.Instance.SceneSystem.SetScene(new StartScene());
        });*/
        /*UITool.GetOrAddComponentInChildren<Button>("BackButton").onClick.AddListener(() =>
        {
            GameRoot.Instance.sceneSystem.SetScene(new StartScene());
            //GameRoot.Instance.SceneSystem.SetScene(new Part_1());
        });*/
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