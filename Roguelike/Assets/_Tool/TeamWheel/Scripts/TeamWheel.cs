using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class TeamWheel : MonoBehaviour
{
    public static TeamWheel Instance;

    public bool edit;
    public bool drawTheBorder;
    public GameObject[] charactersInTeamPrefabs;
    /// <summary>
    /// 存储实际游戏场景中的队伍中的角色游戏物体
    /// </summary>
    private GameObject[] charactersInTeam;

    PlayerTeam playerTeam;

    public GameObject centerPointPrefab;
    private GameObject centerPoint;

    public GameObject TeamCharactersCollectorPrefab;
    public GameObject characterPlacePointPrefab;

    /// <summary>
    /// 存储游戏场景中的位置点对应的游戏物体
    /// </summary>
    private GameObject[] characterPlacePoints;
    public int numOfCharacterInTeam = 6;

    /// <summary>
    /// 整局角色的最大上限制，默认为6
    /// </summary>
    private int globalMaxCharacterNum = 6;


    [Tooltip("角色放置位置生成的半径")]
    /// <summary>
    /// 角色放置位置生成的半径
    /// </summary>
    public float radius;
    /// <summary>
    /// 根据maxCharacterNum确定的平均角度
    /// </summary>
    private float angleInDegrees;
    private float angleInRadians;

    private void Awake()
    {
        playerTeam = new PlayerTeam();
        Init();
        NumOfCharacterInTeamsCal();
        Instance = this;
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        CharactersMoveToCharactersPoint();
    }


    public void Init()
    {
        CenterPointInit();
        CharactersPointInit();
        CharactersInTeamInit();
    }

    private void NumOfCharacterInTeamsCal()
    {
        StartCoroutine(IE_NumOfCharacterInTeamsCal());
    }

    private IEnumerator IE_NumOfCharacterInTeamsCal()
    {
        yield return new WaitForSeconds(0.5f);
        /*numOfCharacterInTeam = 0;
        for (int i = 0; i < globalMaxCharacterNum; i++)
        {
            if (charactersInTeam[i].gameObject != null) numOfCharacterInTeam++;
        }*/
    }

    [Tooltip("队伍中物体靠近对应位置点的速度")]
    public float speed;
    /// <summary>
    /// 使charactersInTeam中的Player向位置点平滑移动
    /// </summary>
    public void CharactersMoveToCharactersPoint()
    {
        float step = speed * Time.deltaTime;
        for (int i = 0; i < globalMaxCharacterNum; i++)
        {
            if (charactersInTeam[i] == null || charactersInTeam[i].GetComponent<PlayerBase>().canBreakAwayFromTheTeam)
            {
                continue;
                //charactersInTeam.RemoveAt(i);
            }
            Vector3 newPos = Vector3.Lerp(charactersInTeam[i].transform.position,
                characterPlacePoints[i].transform.position, step);
            charactersInTeam[i].transform.position = newPos;
        }
    }

    //public void MoveDir()
    //{
    //    Vector2 mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    Vector2 arrowheadposition = new Vector2(centerPoint.transform.position.x, centerPoint.transform.position.y);
    //    float angle = WhatAngle(mouseposition, arrowheadposition);
    //    centerPoint.transform.rotation = Quaternion.Euler(0, 0, angle);
    //}
    //public float WhatAngle(Vector2 a, Vector2 b)
    //{
    //    return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    //}

    /// <summary>
    /// 角色位置点初始化，6个点位置设置
    /// </summary>
    public void CharactersPointInit()
    {
        characterPlacePoints = new GameObject[globalMaxCharacterNum];
        for (int i = 0; i < globalMaxCharacterNum; i++)
        {
            characterPlacePoints[i] = Instantiate(characterPlacePointPrefab, centerPoint.transform);
            //characterPlacePoints[i].SetActive(false);
        }
        angleInDegrees = 360 / globalMaxCharacterNum;
        angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        float _ = 0;
        for (int i = 0; i < globalMaxCharacterNum; i++)
        {
            _ += angleInRadians;
            float x = centerPoint.transform.position.x + radius * Mathf.Cos(_);
            float y = centerPoint.transform.position.y + radius * Mathf.Sin(_);
            float z = centerPoint.transform.position.z;
            characterPlacePoints[i].transform.position = new Vector3(x, y, z);
            characterPlacePoints[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            //characterPlacePoints[i].SetActive(true);
        }
    }

    /// <summary>
    /// 中心点的初始化，要在位置点初始化前初始化
    /// </summary>
    private void CenterPointInit()
    {
        centerPoint = GameObject.Instantiate(centerPointPrefab, this.transform);
    }

    /// <summary>
    /// 队伍中角色的初始化
    /// </summary>
    public void CharactersInTeamInit()
    {
        charactersInTeam = new GameObject[globalMaxCharacterNum];
        if (edit)
        {
            for (int i = 0; i < globalMaxCharacterNum; i++)
            {
                if (charactersInTeamPrefabs[i] == null) continue;
                charactersInTeam[i] = Instantiate(charactersInTeamPrefabs[i], characterPlacePoints[i].transform.position,
                    Quaternion.identity, GameObject.Find("TeamCharactersCollector").transform);
            }
        }
        else
        {
            for (int i = 0; i < globalMaxCharacterNum; i++)
            {
                PlayerTeam.LoadData();
                if (PlayerTeam.playerInTeamPrefabs[i] == null) continue;
                charactersInTeam[i] = Instantiate(PlayerTeam.playerInTeamPrefabs[i], characterPlacePoints[i].transform.position,
                    Quaternion.identity, GameObject.Find("TeamCharactersCollector").transform);
            }
        }

        
    }

    private void OnDrawGizmos()
    {
        if (!drawTheBorder) return;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
