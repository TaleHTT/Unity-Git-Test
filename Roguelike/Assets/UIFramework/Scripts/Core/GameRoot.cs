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

    static public void SaveData()
    {
        Progress.SaveData();
        PlayerTeam.SaveData();
    }

    static public void LoatData()
    {
        Progress.LoadData();
        PlayerTeam.LoadData();
    }

    public class Progress
    {
        static private readonly string PROGRESS_DATA = "ProgressData.data";
        static public int currentLevel = 0;
        static public int currentCoin = 0;

        class ProgressData
        {
            public int level;
            public int coin;
        }

        static public void SaveData()
        {
            ProgressData data = new ProgressData();
            data.level = currentLevel;
            data.coin = currentCoin;
            SaveSystem.SaveByJson(PROGRESS_DATA, data);
        }

        static public void LoadData()
        {
            ProgressData data = SaveSystem.LoadFromJson<ProgressData>(PROGRESS_DATA);
            currentLevel = data.level;
            currentCoin = data.coin;
        }

        static public void DeleteData()
        {
            SaveSystem.DeleteSaveFile(PROGRESS_DATA);
        }
    }

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        sceneSystem = new SceneSystem();
        panelManager = new PanelManager();
        DontDestroyOnLoad(this.gameObject);
        StartCoroutine(InitMap());
        Progress.currentCoin = 10;
    }

    private void Start()
    {
        sceneSystem.SetScene(new StartScene());
    }

    private void Update()
    {
        //Debug.Log(Progress.currentLevel);
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

    public void DestroyMyself()
    {
        Destroy(this.gameObject);
    }
}
