using UnityEngine;

public class Enemy_RectangleDamage_Controller : RectangleDamage_Controller
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        if (currentNumOfAttack >= maxAttackNum)
        {
            rectanglePool.Release(gameObject);
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                foreach (var hit in attackTargets)
                {
                    if (hit.GetComponent<PlayerBase>() != null)
                    {
                        hit.GetComponent<PlayerStats>().TakeDamage(damage);
                        hit.GetComponent<PlayerBase>().isHit = true;
                    }
                }
                currentNumOfAttack++;
                timer = 1;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            attackTargets.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            attackTargets.Remove(collision.gameObject);
        }
    }
}