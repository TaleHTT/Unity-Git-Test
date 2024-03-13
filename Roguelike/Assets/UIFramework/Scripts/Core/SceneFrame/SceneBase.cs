using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneBase
{
    //private string _SceneName;
    public abstract string SceneName{ get;}
    public abstract void EnterScene();

    public abstract void ExitScene();   

    


}
