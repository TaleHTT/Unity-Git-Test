using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl
{
    private static SceneControl instance;

    /// <summary>
    /// <�������֣�����>
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
    /// <param name="scene_name">Ŀ�곡������</param>
    /// <param name="sceneBase">Ŀ�곡����SceneBase</param>
    public void LoadScene(string scene_name, SceneBase sceneBase)
    {
        Debug.Log("���س�����");
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
            Debug.LogWarning($"SceneControl���ֵ��в�����{SceneManager.GetActiveScene().name}");
        }

        //���ջ���Ƴ�ȫ�����
        UIManager.GetInstance().PopAll();

        SceneManager.LoadScene(scene_name);
        sceneBase.EnterScene();
    }
}
