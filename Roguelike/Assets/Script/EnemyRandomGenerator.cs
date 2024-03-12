using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomGenerator : MonoBehaviour
{
    public List<GameObject> EnemyList;
    private List<GameObject> EnemyListInScene;
    private Vector3 centerPoint;
    public float radius;

    public GameObject enemyCollector;
    private GameObject enemyCollectorInScene;

    public bool drawTheBorderOrNot;

    private void Awake()
    {
        if (GameObject.Find("EnemyCollector(Clone)") == null)
        {
            enemyCollectorInScene = Instantiate(enemyCollector);
        }
        centerPoint = this.transform.position;
        EnemyListInScene = new List<GameObject>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            for (int i = 0; i < EnemyList.Count; i++)
            {
                float randomRadius = Random.Range(0, radius);
                float randomAngle = Random.Range(0f, 2f * Mathf.PI);

                float x = centerPoint.x + randomRadius * Mathf.Cos(randomAngle);
                float y = centerPoint.y + randomRadius * Mathf.Sin(randomAngle);
                float z = centerPoint.z;
                Vector3 randomPosition = new Vector3(x, y, z);

                EnemyListInScene.Add(Instantiate(EnemyList[i], randomPosition, Quaternion.identity, enemyCollectorInScene.transform));
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (!drawTheBorderOrNot) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}