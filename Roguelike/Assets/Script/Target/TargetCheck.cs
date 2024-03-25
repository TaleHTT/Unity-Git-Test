using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCheck : MonoBehaviour
{
    public List<GameObject> playerCheck;
    public List<GameObject> enemyCheck;
    public GameObject bossCheck;
    public bool isCheckBoss;
    private void Update()
    {
        if(playerCheck.Count == 0 || isCheckBoss == false)
        {
            //结束游戏的代码
            return;
        }
        if (enemyCheck.Count == 0)
        {
            //结束游戏代码
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            playerCheck.Add(collision.gameObject);
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            enemyCheck.Add(collision.gameObject);
        if(collision.gameObject.layer == LayerMask.NameToLayer("Boss"))
            isCheckBoss = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            playerCheck.Remove(collision.gameObject);
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            enemyCheck.Remove(collision.gameObject);
        if(collision.gameObject.layer == LayerMask.NameToLayer("Boss"))
            isCheckBoss = false;
    }
}
