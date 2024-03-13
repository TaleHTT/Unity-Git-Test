using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoosePart_Panel : BasePanel
{
    private static string panelName = "ChoosePart_Panel";
    private static string path = "Prefab/Panel/ChoosePart_Panel";
    public static UIType uIType = new UIType(path, panelName);

    public ChoosePart_Panel() : base(uIType)
    {

    }

    public override void OnStart()
    {
        base.OnStart();
        UIMethods.GetInstance().AddOrGetComponentInChildren<Button>(ActiveObj, "Part_1").onClick.AddListener(ChangeSceneToPart_1);
    }

    public override void OnEable()
    {
        base.OnEable();
    }

    public override void OnDisabled()
    {
        base.OnDisabled();
    }

    public override void OnDestory()
    {
        base.OnDestory();
    }

    public void ChangeSceneToPart_1()
    {
        SceneControl.GetInstance().LoadScene("Part_1", new Part_1());
    }

    

    

    
}
