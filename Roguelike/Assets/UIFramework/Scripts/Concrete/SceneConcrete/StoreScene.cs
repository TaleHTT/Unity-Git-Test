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


    public override void OnEnter()
    {
        Instance = this;
        SceneManager.LoadScene(sceneName);
        SceneManager.sceneLoaded += SceneLoaded;
    }

    public override void OnExit()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
        GameRoot.Instance.panelManager.PopAll();
    }

    /// <summary>
    /// �����������֮���ִ�з���
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="load"></param>
    public void SceneLoaded(Scene scene, LoadSceneMode load)
    {
        GameRoot.Instance.panelManager.Push(new StorePanel());
        //GameRoot.Instance.mapGenerator.SetActive(true);
        GameRoot.SaveData();
        Debug.Log($"{sceneName}����������ϣ�");
    }
}
