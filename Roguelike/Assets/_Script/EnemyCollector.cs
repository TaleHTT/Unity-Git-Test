
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCollector:MonoBehaviour
{
    static public bool hasEnemiesActive = true;
    static public EnemyCollector instance { get; private set;}


    private void Awake()
    {
        StartCoroutine(IE_EnemyDetect());
        instance = this;
    }

    IEnumerator IE_EnemyDetect()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            hasEnemiesActive = false;
            foreach (Transform child in this.transform)
            {
                if (child.gameObject.activeSelf)
                {
                    hasEnemiesActive = true;
                    break;
                }
            }  
            //Debug.Log(transform.childCount);
        }
    }
}
