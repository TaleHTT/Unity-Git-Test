using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 一个怪物生成器会在一定范围内生成一定的怪物
/// </summary>
public class EnemyGenerator : MonoBehaviour
{
    public Transform[] patrolPoint;
    public List<GameObject> EnemyList;
    private List<GameObject> EnemyListInScene;
    private Vector3 centerPoint;
    public float radius;

    public GameObject enemyCollectorPrefab;
    private GameObject enemyCollectorInScene;

    public bool drawTheBorderOrNot;

    private void Start()
    {
        if (GameObject.Find("EnemyCollector(Clone)") == null)
        {
            enemyCollectorInScene = Instantiate(enemyCollectorPrefab);
        }
        else
        {
            enemyCollectorInScene = GameObject.Find("EnemyCollector(Clone)");
        }
        centerPoint = this.transform.position;
        EnemyListInScene = new List<GameObject>();
        GenerateObject();
    }


    void GenerateObject()
    {
        for(int i = 0; i < EnemyList.Count; i++)
        {
            float randomRadius = Random.Range(0, radius);
            float randomAngle = Random.Range(0f, 2f * Mathf.PI);

            float x = centerPoint.x + randomRadius * Mathf.Cos(randomAngle);
            float y = centerPoint.y + randomRadius * Mathf.Sin(randomAngle);
            float z = centerPoint.z;
            Vector3 randomPosition = new Vector3(x, y, z);
            if(patrolPoint != null)
            {
                foreach(GameObject enemy in EnemyList)
                {
                    enemy.GetComponent<EnemyBase>().patrolPoints = patrolPoint;
                }
            }
            EnemyListInScene.Add(Instantiate(EnemyList[i], randomPosition, Quaternion.identity, enemyCollectorInScene.transform));
        }
    }

    private void OnDrawGizmos()
    {
        if (!drawTheBorderOrNot) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
