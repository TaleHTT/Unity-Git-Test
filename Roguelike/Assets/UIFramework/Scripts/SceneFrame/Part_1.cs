using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part_1 : SceneBase
{
    public override string SceneName { get { return "Part_1"; } }

    public override void EnterScene()
    {
        Debug.Log($"����{SceneName}");
    }

    public override void ExitScene()
    {

    }
}
