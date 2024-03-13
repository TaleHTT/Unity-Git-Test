using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl
{
    private static SceneControl instance;

    /// <summary>
    /// <场景名字，场景>
    /// </summary>
    public Dictionary<string, SceneBase> dict_scene;

    public SceneControl() 
    {
        instance = this;
        dict_scene = new Dictionary<string, SceneBase>();
    }


    public static SceneControl GetInstance()
    {
        if(instance == null) instance = new SceneControl();
        return instance;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scene_name">目标场景名称</param>
    /// <param name="sceneBase">目标场景的SceneBase</param>
    public void LoadScene(string scene_name, SceneBase sceneBase)
    {
        Debug.Log("加载场景中");
        if (!dict_scene.ContainsKey(scene_name))
        {
            dict_scene.Add(scene_name, sceneBase);
        }

        if (dict_scene.ContainsKey(SceneManager.GetActiveScene().name))
        {
            dict_scene[SceneManager.GetActiveScene().name].ExitScene();
        }
        else
        {
            Debug.LogWarning($"SceneControl的字典中不包含{SceneManager.GetActiveScene().name}");
        }

        //清空栈，推出全部面板
        UIManager.GetInstance().PopAll();

        SceneManager.LoadScene(scene_name);
        sceneBase.EnterScene();
    }
}
