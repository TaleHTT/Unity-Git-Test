using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TeamWheel : MonoBehaviour
{
    [Tooltip("角色列表")]
    public List<GameObject> charactersInTeam;
    private List<GameObject> charactersInTeamInScene;
    public GameObject TeamCharaters;

    private int currentCharacterNum;
    private int previousCurrentCharacterNum;
    private int maxCharacterNum;
    /// <summary>
    /// 整局最大的上限为10
    /// </summary>
    private int globalMaxCharacterNum = 10;
    [Tooltip("角色放置位置的图标")]
    public GameObject characterPlacePoint;
    public GameObject centerPoint;

    private GameObject[] characterPlacePoints;

    public bool isCurrentCharacterNumChange;

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
        Instantiate(TeamCharaters);
        //charactersInTeamInScene = charactersInTeam;
        charactersInTeamInScene =  new List<GameObject>(globalMaxCharacterNum);  
        characterPlacePoints = new GameObject[globalMaxCharacterNum];
        for (int i = 0; i < globalMaxCharacterNum; i++)
        {
            characterPlacePoints[i] = Instantiate(characterPlacePoint,centerPoint.transform);
            characterPlacePoints[i].SetActive(false);
        }
        isCurrentCharacterNumChange = false;
        currentCharacterNum = charactersInTeam.Count;
        previousCurrentCharacterNum = currentCharacterNum;
        //Debug.Log(charactersInTeamInScene.Count);
        CharactersPointInit();
    }

    private void Update()
    {
        currentCharacterNum = charactersInTeam.Count;
        if (previousCurrentCharacterNum != currentCharacterNum)
        {
            previousCurrentCharacterNum = currentCharacterNum;
            isCurrentCharacterNumChange=true;
        }

        if(isCurrentCharacterNumChange)
        {
            CharactersPointUpdate();
            isCurrentCharacterNumChange = false;
        }

        if(charactersInTeam.Count > globalMaxCharacterNum) 
        {
            Debug.LogWarning($"charactersInTeam List的长度大于{globalMaxCharacterNum}，超出最大的角色队列存储范围");
        }
        MoveDir();
    }

    private void FixedUpdate()
    {
        GameObjectMoveToCHaractersPoint();
    }

    [Tooltip("队伍中物体靠近对应点的速度")]
    public float speed;
    public void GameObjectMoveToCHaractersPoint()
    {
        float step = speed * Time.deltaTime;
        for (int i = 0; i < charactersInTeamInScene.Count; i++)
        {
            Vector3 newPos = Vector3.Lerp(charactersInTeamInScene[i].transform.position,
                characterPlacePoints[i].transform.position, step);
            charactersInTeamInScene[i].transform.position = newPos;
        }
    }

    public void CharactersPointInit()
    {
        //if (charactersInTeamInScene.Count == 0) return;
        SetAllCharacterPlacePointActive(false);
        angleInDegrees = 360 / charactersInTeam.Count;
        angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        float temp = 0;
        for (int i = 0; i < charactersInTeam.Count; i++)
        {
            temp += angleInRadians;
            float x = centerPoint.transform.position.x + radius * Mathf.Cos(temp);
            float y = centerPoint.transform.position.y + radius * Mathf.Sin(temp);
            float z = centerPoint.transform.position.z;
            characterPlacePoints[i].transform.position = new Vector3(x, y, z);
            characterPlacePoints[i].SetActive(true);
            charactersInTeamInScene.Add(Instantiate(charactersInTeam[i], characterPlacePoints[i].transform.position, Quaternion.identity, GameObject.Find("TeamCharacters(Clone)").transform));
        }
    }

    public void CharactersPointUpdate()
    {
        //if (charactersInTeamInScene.Count == 0) return;
        SetAllCharacterPlacePointActive(false);
        angleInDegrees = 360 / charactersInTeam.Count;
        angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        float temp = 0;
        for(int i = 0; i < charactersInTeam.Count; i++)
        {
            Destroy(charactersInTeamInScene[i]);
            temp += angleInRadians;
            float x = centerPoint.transform.position.x + radius * Mathf.Cos(temp);
            float y = centerPoint.transform.position.y + radius * Mathf.Sin(temp);
            float z = centerPoint.transform.position.z;
            characterPlacePoints[i].transform.position = new Vector3(x, y, z);
            characterPlacePoints[i].SetActive(true);
            charactersInTeamInScene.Add(Instantiate(charactersInTeam[i], characterPlacePoints[i].transform.position, Quaternion.identity, GameObject.Find("TeamCharacters").transform));
        }
        charactersInTeamInScene.Clear();
    }

    public void SetAllCharacterPlacePointActive(bool _)
    {
        for(int i = 0; i < globalMaxCharacterNum; i++)
        {
            characterPlacePoints[i].SetActive(_);
        }
    }
    public void MoveDir()
    {
        Vector2 mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 arrowheadposition = new Vector2(centerPoint.transform.position.x, centerPoint.transform.position.y);
        float angle = WhatAngle(mouseposition, arrowheadposition);
        centerPoint.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    public float WhatAngle(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
