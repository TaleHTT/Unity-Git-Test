using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreSceneManager : MonoBehaviour
{
    static public StoreSceneManager instance;
    public bool test;

    [SerializeField]
    GameObject storePanel;

    [SerializeField]
    GameObject[] storeSlots;

    /// <summary>
    /// 商店界面在StoreSlot会生成的角色
    /// </summary>
    [SerializeField]
    GameObject[] playerInStoreSlotPrefabs;

    [SerializeField]
    TextMeshProUGUI coinText;

    /*class ImageGameObject
    {
        public int cost;
        GameObject playerInStoreSlotPrefab;
    }*/

    public static class ImagePlayerPrefabTransition
    {
        public static string saberImageGameObjectName = "Saber(Clone)";
        public static string saberPrefabPath = "PlayerPrefab/PlayerSaber";

        public static string saberPrefabName = "PlayerSaber";
        public static string saberImagePreafabPath = "PlayerImage/Saber";


        public static string archerImageGameObjectName = "Archer(Clone)";
        public static string archerPrefabPath = "PlayerPrefab/PlayerArcher";

        public static string archerPrefabName = "PlayerArcher";
        public static string archerImagePreafabPath = "PlayerImage/Archer";


        public static string casterImageGameObjectName = "Caster(Clone)";
        public static string casterPrefabPath = "PlayerPrefab/PlayerCaster";

        public static string casterPrefabName = "PlayerCaster";
        public static string casterImagePreafabPath = "PlayerImage/Caster";

        public static Dictionary<string, Object> imageToPlayerPrefab;
        public static Dictionary<string, Object> playerPrefabToImage;

        public static void Init()
        {
            imageToPlayerPrefab = new Dictionary<string, Object>
            {
                { saberImageGameObjectName, Resources.Load<GameObject>(saberPrefabPath) },
                { archerImageGameObjectName, Resources.Load<GameObject>(archerPrefabPath) },
                { casterImageGameObjectName, Resources.Load<GameObject>(casterPrefabPath) }
            };

            playerPrefabToImage = new Dictionary<string, Object>
            {
                { saberPrefabName, Resources.Load<GameObject>(saberImagePreafabPath) },
                { archerPrefabName, Resources.Load<GameObject>(archerImagePreafabPath) },
                { casterPrefabName, Resources.Load<GameObject>(casterImagePreafabPath) }
            };


        }
    }

    private void Awake()
    {
        ImagePlayerPrefabTransition.Init();
        instance = this;
        //playerPrefabInTeam = new GameObject[6];

    }

    private void Start()
    {
        if(!test)
            RandomGeneratePlayerInStoreSlot();
        SetPlayerInTeamDataToPlayerTeamSlot();
    }

    private void Update()
    {
        if(!test)
            SetPlayerPrefabInTeam();
        ShowPlayerPrefabInTeam();
        if (SceneManager.GetActiveScene().name == "StoreScene") 
        {
            coinText.text = GameRoot.Progress.currentCoin.ToString();
        } 
    }

    /// <summary>
    /// 将data中的playerprefab数据转换成image显示出来
    /// </summary>
    void SetPlayerInTeamDataToPlayerTeamSlot()
    {
        for(int i = 0; i < 6; i++)
        {
            if(PlayerTeam.playerInTeamPrefabs[i] != null)
            {
                //PlayerTeamSlotDetect.Instance.playerTeamSlots[i] = 
                GameObject _ = Instantiate(ImagePlayerPrefabTransition.playerPrefabToImage[PlayerTeam.playerInTeamPrefabs[i].name] as GameObject, 
                    storePanel.transform);
                _.transform.position = PlayerTeamSlotDetect.Instance.playerTeamSlots[i].transform.position;
                PlayerTeamSlotDetect.Instance.playersInTeam[i] = _;

            }
        }
    }

    /// <summary>
    /// 在选取格中随机生成image物体
    /// </summary>
    void RandomGeneratePlayerInStoreSlot()
    {
        int minPrefabIndex = 0, maxPrefabIndex = playerInStoreSlotPrefabs.Length;
        for(int i = 0; i < storeSlots.Length; i++)
        {
            int index = Random.Range(minPrefabIndex, maxPrefabIndex);
            GameObject _ = Instantiate(playerInStoreSlotPrefabs[index], storePanel.transform);
            _.transform.position = storeSlots[i].transform.position;
        }
    }

    public void SetPlayerPrefabInTeam()
    {
        for(int i = 0; i < PlayerTeamSlotDetect.Instance.globalMaxPlayerNum; i++)
        {
            if (PlayerTeamSlotDetect.Instance.playersInTeam[i] != null)
            {
                PlayerTeamManager.Instance.playerPrefabInTeam[i] = ImagePlayerPrefabTransition.imageToPlayerPrefab[PlayerTeamSlotDetect.Instance.playersInTeam[i].GetComponent<Image>().name] as GameObject;
                //Debug.Log(playerPrefabInTeam[i]);
                Debug.Log("hello");
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
            //Debug.Log($"{PlayerTeamSlotDetect.Instance.playerTeamSlots[i].name} : {PlayerTeamManager.Instance.playerPrefabInTeam[i]}");
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
