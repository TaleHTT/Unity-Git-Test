using UnityEngine;
using UnityEngine.Pool;

public class Dagger_Controller : MonoBehaviour
{
    public ObjectPool<GameObject> daggerPool {  get; set; }
    public float damage {  get; set; }
    public float moveSpeed;
    public Vector2 moveDir;
    private void Update()
    {
        transform.Translate(moveDir * moveSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyStats>().TakeDamage(damage);
            collision.gameObject.GetComponent<EnemyBase>().isHunting = true;
            collision.gameObject.GetComponent<EnemyBase>().markDurationTimer = DataManager.instance.assassin_Skill_Data.skill_3_durationTimer;
        }
    }
}