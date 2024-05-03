using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ��ɫ�������������ȫ���ƽ��Ĺ����л�ȡ�̵�ҳ��Ľ�ɫ���ݼ��ص�ս�������У�
/// �洢���������еĽ�ɫ��Ϣ
/// </summary>
public class PlayerTeamManager : MonoBehaviour
{
    public static PlayerTeamManager Instance { get; private set; }
    /// <summary>
    /// ʵʱ�õ���PlayerPrefab
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