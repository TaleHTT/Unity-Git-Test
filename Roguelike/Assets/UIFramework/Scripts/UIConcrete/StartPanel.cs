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
        UITool.GetOrAddComponentInChildren<Button>("StartButton").onClick.AddListener(() =>
        {
            GameRoot.Instance.SceneSystem.SetScene(new ChoosePart());
        });
    }
}
