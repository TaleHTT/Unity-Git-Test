using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//该类需要序列化才能被json转换
[System.Serializable]
class PlayerTeam
{
    const int TeamSize = 6;
    readonly static string PLAYER_TEAM_INFO_FILE_NAME = "PlayerTeamData.data";

    public static GameObject[] playerInTeamPrefabs = new GameObject[TeamSize];

    class PlayerTeamSaveData
    {
        public GameObject[] playerPrefabs;
    }
    private bool inStoreScene;


    public static void SaveData()
    {
        PlayerTeamSaveData playerTeamData = new PlayerTeamSaveData();
        playerTeamData.playerPrefabs = new GameObject[TeamSize];

        Array.Copy(PlayerTeamManager.Instance.playerPrefabInTeam, playerTeamData.playerPrefabs, TeamSize);

        SaveSystem.SaveByJson(PLAYER_TEAM_INFO_FILE_NAME, playerTeamData);
    }

    public static void LoadData()
    {
        PlayerTeamSaveData playerTeamData = SaveSystem.LoadFromJson<PlayerTeamSaveData>(PLAYER_TEAM_INFO_FILE_NAME);

        Array.Copy(playerTeamData.playerPrefabs, playerInTeamPrefabs, TeamSize);
    }

    public static void DeleteData()
    {
        SaveSystem.DeleteSaveFile(PLAYER_TEAM_INFO_FILE_NAME);
    }
}
