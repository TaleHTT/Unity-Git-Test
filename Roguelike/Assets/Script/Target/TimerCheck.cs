using UnityEngine;

public class TimerCheck : MonoBehaviour
{
    public static TimerCheck instance;
    public float timer;
    public int targetHp;
    public bool isPass;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            isPass = true;
        }
        if(targetHp <= 0)
        {
            isPass = false;
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
