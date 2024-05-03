using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 角色队伍管理器，在全卡推进的过程中获取商店页面的角色数据加载到战斗场景中，
/// 存储队伍中所有的角色信息
/// </summary>
public class PlayerTeamManager : MonoBehaviour
{
    public static PlayerTeamManager Instance { get; private set; }
    /// <summary>
    /// 实时得到的PlayerPrefab
    /// </summary>
    public GameObject[] playerPrefabInTeam;
    public int globalMaxPlayerNum = 6;
    public int currentPlayerNum = 0;
    private bool inStoreScene;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        Instance = this;
        playerPrefabInTeam = new GameObject[globalMaxPlayerNum];
        inStoreScene = true;
    }

    private void Update()
    {
        CurrentSceneDetect();
        GetPlayerNumberInTeam();
        //Debug.Log($"currentPlayerNum = {currentPlayerNum}");
    }

    private void CurrentSceneDetect()
    {
        if (SceneManager.GetActiveScene().name == "StoreScene")
        {
            inStoreScene = true;
        }
        else
        {
            inStoreScene = false;
        }
    }

    public void GetPlayerNumberInTeam()
    {
        //if (!inStoreScene) return;
        currentPlayerNum = 0;
        for (int i = 0; i < globalMaxPlayerNum; i++)
        {
            if (playerPrefabInTeam[i] != null)
            {
                //Debug.Log($"PlayerName = {playerPrefabInTeam[i].name}");
                currentPlayerNum++;
            }
        }
        //Debug.Log($"currentPlayerNum = {currentPlayerNum}");
        //return currentPlayerNum;
    }


}