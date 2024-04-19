using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class BurningRings_Controller : MonoBehaviour
{
    public ObjectPool<GameObject> burningRingsPool;
    public float timer;
    public float burningDamage;
    public float burningRadius;
    public LayerMask whatIsEnemy;
    public Player_Orb_Controller player_Orb_Controller;
    private void Awake()
    {
        burningDamage = (1 + DataManager.instance.caster_Skill_Data.skill_1_extraAddExplodeDamage) * player_Orb_Controller.damage;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            burningRingsPool.Release(gameObject);
            return;
        }
        StartCoroutine(DealDamage());
    }
    public IEnumerator DealDamage()
    {
        yield return new WaitForSeconds(1);

        Trigger();
    }
    public void Trigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, burningRadius, whatIsEnemy);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyBase>() != null)
                hit.GetComponent<EnemyStats>().AuthenticTakeDamage(burningDamage);
        }
    }
}