using UnityEngine;

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

    private void Awake()
    {
        DontDestroyOnLoad(this);
        Instance = this;
        playerPrefabInTeam = new GameObject[globalMaxPlayerNum];
    }


}