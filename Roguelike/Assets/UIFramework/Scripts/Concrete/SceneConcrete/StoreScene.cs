using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ��ʼ����
/// </summary>
public class StoreScene : SceneState
{
    public static StoreScene Instance { get; private set; }

    /// <summary>
    /// ��������
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
    /// �����������֮���ִ�з���
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="load"></param>
    public void SceneLoaded(Scene scene, LoadSceneMode load)
    {
        panelManager.Push(new StorePanel());
        //GameRoot.Instance.mapGenerator.SetActive(true);
        Debug.Log($"{sceneName}����������ϣ�");
    }
}
