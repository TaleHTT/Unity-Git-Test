using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 开始主界面
/// </summary>
public class StartPanel : BasePanel
{
    static readonly string path = "Prefab/Panel/StartPanel";

    public StartPanel() : base(new UIType(path)) { }

    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<Button>("NewGameButton").onClick.AddListener(() =>
        {
            PlayerTeam.DeleteData();
            GameRoot.Progress.DeleteData();
            MapGenerator.Instance.NodeLevelSet(GameRoot.Progress.currentLevel);
            GameRoot.Instance.sceneSystem.SetScene(new StoreScene());
        });

        UITool.GetOrAddComponentInChildren<Button>("ContinueGameButton").onClick.AddListener(() =>
        {
            PlayerTeam.LoadData();
            GameRoot.Progress.LoadData();
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
