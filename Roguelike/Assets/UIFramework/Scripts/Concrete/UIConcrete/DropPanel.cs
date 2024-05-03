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
        Time.timeScale = 0;
        //Debug.Log("ÓÎÏ·ÔÝÍ£");
        UITool.GetOrAddComponentInChildren<Button>("ContinueButton").onClick.AddListener(() =>
        {
            GameRoot.Instance.mapGenerator.SetActive(true);
            //GameRoot.Instance.sceneSystem.SetScene(new StoreScene());
            GameRoot.SaveData();
        });
    }

    public override void OnExit()
    {
        base.OnExit();
        Time.timeScale = 1;
        //Debug.Log("ÓÎÏ·¼ÌÐø");
    }

    public override void OnPause()
    {
        base.OnPause();
    }

    public override void OnResume()
    {

    }
}