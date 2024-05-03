using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ��ʼ����
/// </summary>
public class Part_3 : SceneState
{
    /// <summary>
    /// ��������
    /// </summary>
    readonly string sceneName = "Part_3";

    public override void OnEnter()
    {

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
        GameRoot.Instance.panelManager.Push(new DuringLevelPanel());
        Debug.Log($"{sceneName}����������ϣ�");
    }
}
