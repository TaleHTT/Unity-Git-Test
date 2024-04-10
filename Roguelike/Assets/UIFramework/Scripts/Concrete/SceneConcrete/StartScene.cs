using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 开始场景
/// </summary>
public class StartScene : SceneState
{
    /// <summary>
    /// 场景名称
    /// </summary>
    readonly string sceneName = "StartScene";


    /// <summary>
    /// 进入场景执行方法，具体内容在SceneLoaded中实现
    /// </summary>
    public override void OnEnter()
    {

        Time.timeScale = 1.0f;
        if(SceneManager.GetActiveScene().name != sceneName)
        {
            SceneManager.LoadScene(sceneName);
            SceneManager.sceneLoaded += SceneLoaded;
        }
        else
        {
            GameRoot.Instance.panelManager.Push(new StartPanel());
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
        GameRoot.Instance.panelManager.Push(new StartPanel());
        Debug.Log($"{sceneName}场景加载完毕！");
    }
}
