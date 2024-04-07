using UnityEngine;

public class Target_PassPartCheck : MonoBehaviour
{
    private void Update()
    {
        if (PlayerManager.instance.playerCount <= 0)
            EntityEventSystem.instance.Target_FailPassPart();
        if(EnemyManager.instance.enemyCount <= 0 /*|| BossManager.instance.bossCount <= 0*/)
            EntityEventSystem.instance.Traget_SuccessPassPart();
    }
}