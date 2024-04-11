using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ��ʼ����
/// </summary>
public class StartScene : SceneState
{
    /// <summary>
    /// ��������
    /// </summary>
    readonly string sceneName = "StartScene";


    /// <summary>
    /// ���볡��ִ�з���������������SceneLoaded��ʵ��
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
    /// �����������֮���ִ�з���
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="load"></param>
    public void SceneLoaded(Scene scene, LoadSceneMode load)
    {
        GameRoot.Instance.panelManager.Push(new StartPanel());
        Debug.Log($"{sceneName}����������ϣ�");
    }
}
