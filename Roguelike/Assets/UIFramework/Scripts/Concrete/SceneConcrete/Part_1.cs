using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 开始场景
/// </summary>
public class Part_1 : SceneState
{
    /// <summary>
    /// 场景名称
    /// </summary>
    readonly string sceneName = "Part_1";

    public override void OnEnter()
    {

        if (SceneManager.GetActiveScene().name != sceneName)
        {
            SceneManager.LoadScene(sceneName);
            SceneManager.sceneLoaded += SceneLoaded;
        }
        else
        {
            //panelManager.Push();
        }

    }

    public override void OnExit()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
        GameRoot.Instance.panelManager.PopAll();
    }

    /// <summary>
    /// 场景加载完毕之后的执行方法
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="load"></param>
    public void SceneLoaded(Scene scene, LoadSceneMode load)
    {
        GameRoot.Instance.panelManager.Push(new Part_1Panel());
        Debug.Log($"{sceneName}场景加载完毕！");
    }
}
