using UnityEngine;

public class Enemy_MultipleArrow_Controller : MultipleArrow_Controller
{
    public Enemy_Archer enemy_Archer { get; set; }
    protected override void Update()
    {
        base.Update();
        moveDir = (transform.position - enemy_Archer.transform.position).normalized;
        Move();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            collision.GetComponent<PlayerStats>()?.TakeDamage(damage);
            collision.GetComponent<PlayerBase>().isHit = true;
            multipleArrowPool.Release(gameObject);
        }
    }
}