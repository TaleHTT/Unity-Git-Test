using UnityEngine;

public class Timer_PassPartCheck : MonoBehaviour
{
    [Tooltip("�ؿ�ʱ��")]
    public float timer;
    [Tooltip("��������ֵ")]
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
