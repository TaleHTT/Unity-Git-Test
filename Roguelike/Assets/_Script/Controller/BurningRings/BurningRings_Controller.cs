using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class BurningRings_Controller : MonoBehaviour
{
    public ObjectPool<GameObject> burningRingsPool;
    private float timer;
    private float damageTimer = 1;
    private float burningDamage;
    public LayerMask whatIsEnemy;
    private void Awake()
    {

    }
    private void Start()
    {
        transform.localScale *= DataManager.instance.caster_Skill_Data.skill_1_explodeRadius;
        burningDamage = (1 + DataManager.instance.caster_Skill_Data.skill_1_extraAddExplodeDamage) * PrefabManager.instance.player_Orb_Controller.damage;
        timer = DataManager.instance.caster_Skill_Data.duration;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            burningRingsPool.Release(gameObject);
            return;
        }
        damageTimer -= Time.deltaTime;
        if(damageTimer <= 0)
        {
            Trigger();
            damageTimer = 1;
        }
    }
    public void Trigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.caster_Skill_Data.skill_1_explodeRadius, whatIsEnemy);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyBase>() != null)
                hit.GetComponent<EnemyStats>().AuthenticTakeDamage(burningDamage);
        }
    }
}