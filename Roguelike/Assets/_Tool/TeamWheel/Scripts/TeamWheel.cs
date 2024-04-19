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
    /// �洢ʵ����Ϸ�����еĶ����еĽ�ɫ��Ϸ����
    /// </summary>
    private GameObject[] charactersInTeam;

    PlayerTeam playerTeam;

    public GameObject centerPointPrefab;
    private GameObject centerPoint;

    public GameObject TeamCharactersCollectorPrefab;
    public GameObject characterPlacePointPrefab;

    /// <summary>
    /// �洢��Ϸ�����е�λ�õ��Ӧ����Ϸ����
    /// </summary>
    private GameObject[] characterPlacePoints;
    public int numOfCharacterInTeam = 6;

    /// <summary>
    /// ���ֽ�ɫ����������ƣ�Ĭ��Ϊ6
    /// </summary>
    private int globalMaxCharacterNum = 6;


    [Tooltip("��ɫ����λ�����ɵİ뾶")]
    /// <summary>
    /// ��ɫ����λ�����ɵİ뾶
    /// </summary>
    public float radius;
    /// <summary>
    /// ����maxCharacterNumȷ����ƽ���Ƕ�
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

    [Tooltip("���������忿����Ӧλ�õ���ٶ�")]
    public float speed;
    /// <summary>
    /// ʹcharactersInTeam�е�Player��λ�õ�ƽ���ƶ�
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
    /// ��ɫλ�õ��ʼ����6����λ������
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
    /// ���ĵ�ĳ�ʼ����Ҫ��λ�õ��ʼ��ǰ��ʼ��
    /// </summary>
    private void CenterPointInit()
    {
        centerPoint = GameObject.Instantiate(centerPointPrefab, this.transform);
    }

    /// <summary>
    /// �����н�ɫ�ĳ�ʼ��
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
