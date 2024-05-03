using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 游戏通关弹出的UI画面
/// </summary>
public class WinPanel : BasePanel
{
    static readonly string path = "Prefab/Panel/WinPanel";

    public WinPanel() : base(new UIType(path)) { }

    public override void OnEnter()
    {
        Time.timeScale = 0f;
        UITool.GetOrAddComponentInChildren<Button>("BackButton").onClick.AddListener(() =>
        {
            GameRoot.Instance.DestroyMyself();
            GameRoot.Instance.sceneSystem.SetScene(new StartScene());
        });
    }

    IEnumerator IE_Func()
    {
        yield return new WaitForSeconds(1f);

    }

    public override void OnExit()
    {
        base.OnExit();
        Time.timeScale = 1f;
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
