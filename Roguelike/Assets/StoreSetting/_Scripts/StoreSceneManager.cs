using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class StoreSceneManager : MonoBehaviour
{
    

    public static class ImageToPlayerPrefab
    {
        public static string saberGameObjectName = "Saber";
        public static string saberPrefabPath = "PlayerPrefab/PlayerSaber";

        public static string archerGameObjectName = "Archer";
        public static string archerPrefabPath = "PlayerPrefab/PlayerArcher";

        public static string casterGameObjectName = "Caster";
        public static string casterPrefabPath = "PlayerPrefab/PlayerCaster";

        public static Dictionary<string, Object> imageToPlayerPrefab;

        public static void Init()
        {
            imageToPlayerPrefab = new Dictionary<string, Object>();

            /*GameObject test = Resources.Load<Object>("PlayerImage/Archer") as GameObject;
            Debug.Log(test);*/

            //imageToPlayerPrefab.Add(Resources.Load<GameObject>(saberImagePath), test);

            imageToPlayerPrefab.Add(saberGameObjectName, Resources.Load<GameObject>(saberPrefabPath));
            imageToPlayerPrefab.Add(archerGameObjectName, Resources.Load<GameObject>(archerPrefabPath));
            imageToPlayerPrefab.Add(casterGameObjectName, Resources.Load<GameObject>(casterPrefabPath));
        }
    }

    private void Awake()
    {
        ImageToPlayerPrefab.Init();
        //playerPrefabInTeam = new GameObject[6];
    }

    private void Start()
    {
    }

    private void Update()
    {
        SetPlayerPrefabInTeam();
        ShowPlayerPrefabInTeam();
    }

    public void SetPlayerPrefabInTeam()
    {
        for(int i = 0; i < PlayerTeamSlotDetect.Instance.globalMaxPlayerNum; i++)
        {
            if (PlayerTeamSlotDetect.Instance.playersInTeam[i] != null)
            {
                PlayerTeamManager.Instance.playerPrefabInTeam[i] = ImageToPlayerPrefab.imageToPlayerPrefab[PlayerTeamSlotDetect.Instance.playersInTeam[i].name] as GameObject;
                //Debug.Log(playerPrefabInTeam[i]);
            }
            else
            {
                PlayerTeamManager.Instance.playerPrefabInTeam[i] = null;
            }
        }
    }

    public void ShowPlayerPrefabInTeam()
    {
        for (int i = 0; i < PlayerTeamSlotDetect.Instance.globalMaxPlayerNum; i++)
        {
            Debug.Log($"{PlayerTeamSlotDetect.Instance.playerTeamSlots[i].name} : {PlayerTeamManager.Instance.playerPrefabInTeam[i]}");
        }
    }

    void ClearLog()
    {
        System.Reflection.Assembly assembly = System.Reflection.Assembly.GetAssembly(typeof(UnityEditor.SceneView));
        System.Type logEntries = assembly.GetType("UnityEditor.LogEntries");
        System.Reflection.MethodInfo clearConsoleMethod = logEntries.GetMethod("Clear");
        clearConsoleMethod.Invoke(new object(), null);
    }
}
