
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCollector:MonoBehaviour
{
    static public bool hasEnemiesActive = true;

    private void Start()
    {
        StartCoroutine(IE_EnemyDetect());
    }

    IEnumerator IE_EnemyDetect()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (this.transform.childCount == 0) 
            {
                hasEnemiesActive = false;
            }
            Debug.Log(transform.childCount);
        }
    }
}
