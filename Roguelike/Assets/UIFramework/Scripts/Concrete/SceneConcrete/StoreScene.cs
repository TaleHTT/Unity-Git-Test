using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 开始场景
/// </summary>
public class StoreScene : SceneState
{
    public static StoreScene Instance { get; private set; }

    /// <summary>
    /// 场景名称
    /// </summary>
    readonly string sceneName = "StoreScene";
    public PanelManager panelManager;

    public override void OnEnter()
    {
        Instance = this;
        panelManager = new PanelManager();

        if (SceneManager.GetActiveScene().name != sceneName)
        {
            SceneManager.LoadScene(sceneName);
            SceneManager.sceneLoaded += SceneLoaded;
        }
        else
        {
            panelManager.Push(new StorePanel());
        }

    }

    public override void OnExit()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
        panelManager.PopAll();
    }

    /// <summary>
    /// 场景加载完毕之后的执行方法
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="load"></param>
    public void SceneLoaded(Scene scene, LoadSceneMode load)
    {
        panelManager.Push(new StorePanel());
        //GameRoot.Instance.mapGenerator.SetActive(true);
        Debug.Log($"{sceneName}场景加载完毕！");
    }
}
