using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static PlayerTeam;

//该类需要序列化才能被json转换
[System.Serializable]
class PlayerTeam
{
    const int TeamSize = 6;
    readonly static string PLAYER_TEAM_INFO_FILE_NAME = "PlayerTeamData.data";

    //public static PlayerTeamSaveData playerInTeamData = new PlayerTeamSaveData();
    static public PlayerTeamData playerTeamData;

    public class PlayerTeamData
    {
        public IndividualPlayerData[] data;
    }

    [System.Serializable]
    public class IndividualPlayerData
    {
        public GameObject playerPrefab;
        public int level = 1;
    }

    private bool inStoreScene;

    public static void SaveData()
    {
        //PlayerTeamSaveData data = new PlayerTeamSaveData();
        PlayerTeamData data = new PlayerTeamData();
        data.data = new IndividualPlayerData[TeamSize];

        //Array.Copy(PlayerTeamManager.Instance.playerPrefabInTeam, playerTeamData.playerPrefabs, TeamSize);
        for (int i = 0; i < TeamSize; i++)
        {
            if (PlayerTeamManager.Instance.playerPrefabInTeam[i] != null)
            {
                data.data[i] = new IndividualPlayerData();
                data.data[i].playerPrefab = PlayerTeamManager.Instance.playerPrefabInTeam[i];
                if (PlayerTeamSlotDetect.Instance != null)
                {
                    data.data[i].level = PlayerTeamSlotDetect.Instance.playersInTeam[i].GetComponent<MoveImageItem>().level;
                }
                else
                {
                    if (playerTeamData.data[i] != null)
                    {
                        data.data[i].level = TeamWheel.Instance.charactersInTeam[i].GetComponent<PlayerStats>().level;
                    }

                }

            }
        }
        SaveSystem.SaveByJson(PLAYER_TEAM_INFO_FILE_NAME, data);
    }

    public static void LoadData()
    {
        PlayerTeamData data = SaveSystem.LoadFromJson<PlayerTeamData>(PLAYER_TEAM_INFO_FILE_NAME);
        playerTeamData = new PlayerTeamData();
        playerTeamData.data = new IndividualPlayerData[TeamSize];
        for (int i = 0; i < TeamSize; i++)
        {
            if (data.data[i].playerPrefab != null)
            {
                playerTeamData.data[i] = new IndividualPlayerData()
;               playerTeamData.data[i].playerPrefab = data.data[i].playerPrefab;
                playerTeamData.data[i].level = data.data[i].level;
            }
        }
        //Array.Copy(playerTeamData.playerPrefabs, playerInTeamPrefabs, TeamSize);
    }


    public static void DeleteData()
    {
        SaveSystem.DeleteSaveFile(PLAYER_TEAM_INFO_FILE_NAME);
    }
}
