using System.Collections;
using UnityEngine;

public class Target_PassPartCheck : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(IE_Update());
    }

    IEnumerator IE_Update()
    {
        yield return new WaitForSeconds(3f);
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (PlayerTeamManager.Instance.currentPlayerNum <= 0)
            {
                EntityEventSystem.instance.Target_FailPassPart();
                break;
            }
                
            if (!EnemyCollector.hasEnemiesActive)
            {
                EntityEventSystem.instance.Traget_SuccessPassPart();
                break;
            }
                
        }
    }
}
