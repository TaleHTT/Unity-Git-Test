using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 管理全局
/// </summary>
public class GameRoot : MonoBehaviour
{
    public static GameRoot Instance { get; private set; }
    /// <summary>
    /// 场景管理器
    /// </summary>
    public SceneSystem sceneSystem { get; private set; }
    public PanelManager panelManager { get; private set; }
    public GameObject mapGenerator;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        sceneSystem = new SceneSystem();
        panelManager = new PanelManager();
        DontDestroyOnLoad(this.gameObject);
        StartCoroutine(InitMap());
    }

    private void Start()
    {
        sceneSystem.SetScene(new StartScene());
    }

    IEnumerator InitMap()
    {
        mapGenerator = GameObject.Find("mapGenerator");
        mapGenerator.GetComponent<Canvas>().sortingOrder = -2;
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => MapGenerator.Instance.currentMap != null);
        MapGenerator.Instance.currentMap.GetComponent<Canvas>().sortingOrder = -1;
        //yield return new WaitForSeconds(1f);
        mapGenerator.gameObject.SetActive(false);
        mapGenerator.GetComponent<Canvas>().sortingOrder = 1;
        MapGenerator.Instance.currentMap.GetComponent<Canvas>().sortingOrder = 2;
        yield return null;
    }
}
