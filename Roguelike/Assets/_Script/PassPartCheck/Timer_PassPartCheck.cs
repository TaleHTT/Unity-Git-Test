using UnityEngine;

public class Timer_PassPartCheck : MonoBehaviour
{
    [Tooltip("关卡时间")]
    public float timer;
    [Tooltip("物资生命值")]
    public int hp;
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
            EntityEventSystem.instance.Timer_SuccessPassPart();
        /*if(hp <= 0 || PlayerManager.instance.playerCount <= 0)
            EntityEventSystem.instance.Timer_FailPassPart();*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            hp -= 1;
    }
}
