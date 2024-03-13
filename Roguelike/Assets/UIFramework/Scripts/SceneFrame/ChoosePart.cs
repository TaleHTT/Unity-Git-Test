using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePart : SceneBase
{
    public override string SceneName { get { return "ChoosePart"; } }

    public override void EnterScene()
    {
        Debug.Log($"½øÈë{SceneName}");
        UIManager.GetInstance().Push(new ChoosePart_Panel());
    }

    public override void ExitScene()
    {

    }
}
