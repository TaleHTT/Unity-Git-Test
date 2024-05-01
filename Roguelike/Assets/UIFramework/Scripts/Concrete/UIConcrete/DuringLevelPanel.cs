using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 开始主界面
/// </summary>
public class DuringLevelPanel : BasePanel
{
    static readonly string path = "Prefab/Panel/DuringLevelPanel";

    public DuringLevelPanel() : base(new UIType(path)) { }

    public override void OnEnter()
    {
        
        UITool.GetOrAddComponentInChildren<Button>("SuccessButton").onClick.AddListener(() =>
        {
            if(SceneManager.GetActiveScene().name == "BossScene")
            {
                GameRoot.Instance.panelManager.Push(new WinPanel());
                return;
            }
            GameRoot.Progress.currentLevel++;
            //GameRoot.Progress.SaveData();
            MapGenerator.Instance.NodeLevelSet(GameRoot.Progress.currentLevel);
            GameRoot.SaveData();
            GameRoot.Instance.panelManager.Push(new DropPanel());
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
