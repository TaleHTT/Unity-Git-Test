using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCheck : MonoBehaviour
{
    public float timer;
    public int targetHp;
    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0 || targetHp <= 0)
        {
            //结束游戏的代码
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            targetHp -= 1;
        }
    }
}
