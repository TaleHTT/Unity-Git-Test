using UnityEngine;
using UnityEngine.Pool;

public class Bat_Controller : MonoBehaviour
{
    public Player_Bloodsucker player_Bloodsucker {  get; set; }
    public ObjectPool<GameObject> batPool {  get; set; }
    public float damage {  get; set; }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            player_Bloodsucker.stats.TakeTreat(damage * (1 + DataManager.instance.bloodsucker_Skill_Data.normalExtraAddHp_2));
            collision.GetComponent<EnemyStats>().TakeDamage(damage);
            batPool.Release(gameObject);
        }
    }
}