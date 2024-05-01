using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropPanel : BasePanel
{
    static readonly string path = "Prefab/Panel/DropPanel";

    public DropPanel() : base(new UIType(path)) { }

    public override void OnEnter()
    {
        UITool.GetOrAddComponentInChildren<Button>("ContinueButton").onClick.AddListener(() =>
        {
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

    }
}