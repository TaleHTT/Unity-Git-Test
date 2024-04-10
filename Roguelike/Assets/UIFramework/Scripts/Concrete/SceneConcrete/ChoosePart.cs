using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ��ʼ����
/// </summary>
public class ChoosePart : SceneState
{
    /// <summary>
    /// ��������
    /// </summary>
    readonly string sceneName = "ChoosePart";

    public override void OnEnter()
    {

        if (SceneManager.GetActiveScene().name != sceneName)
        {
            SceneManager.LoadScene(sceneName);
            SceneManager.sceneLoaded += SceneLoaded;
        }
        else
        {
            GameRoot.Instance.panelManager.Push(new ChoosePartPanel());
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
        GameRoot.Instance.panelManager.Push(new ChoosePartPanel());
        Debug.Log($"{sceneName}����������ϣ�");
    }
}
