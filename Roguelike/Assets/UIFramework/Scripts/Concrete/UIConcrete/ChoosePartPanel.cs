using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 开始主界面
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
}
