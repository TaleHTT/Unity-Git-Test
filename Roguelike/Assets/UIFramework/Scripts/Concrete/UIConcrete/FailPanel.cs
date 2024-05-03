using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 游戏中失败弹出的UI画面
/// </summary>
public class FailPanel : BasePanel
{
    static readonly string path = "Prefab/Panel/FailPanel";

    public FailPanel() : base(new UIType(path)) { }

    public override void OnEnter()
    {
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
