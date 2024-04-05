using UnityEngine;

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

    private void Awake()
    {
        DontDestroyOnLoad(this);
        Instance = this;
        playerPrefabInTeam = new GameObject[globalMaxPlayerNum];
    }


}