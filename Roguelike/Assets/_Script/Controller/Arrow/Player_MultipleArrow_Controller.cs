using UnityEngine;

public class Player_MultipleArrow_Controller : MultipleArrow_Controller
{
    public Player_Archer player_Archer { get; set; }
    protected override void Update()
    {
        base.Update();
        moveDir = (transform.position - player_Archer.transform.position).normalized;
        Move();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") || collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            collision.GetComponent<EnemyStats>()?.TakeDamage(damage);
            collision.GetComponent<EnemyBase>().isHit = true;
            multipleArrowPool.Release(gameObject);
        }
    }
}