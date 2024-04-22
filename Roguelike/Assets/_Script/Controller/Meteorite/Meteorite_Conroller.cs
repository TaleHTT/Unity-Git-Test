using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Meteorite_Conroller : MonoBehaviour
{
    private float moveSpeed;
    private List<GameObject> attackDetects;
    private Transform attackTarget;
    private Vector3 attackDir;
    public float damage {  get; set; }
    public float attackRadius {  get; set; } = Mathf.Infinity;
    public bool drawTheBorderOrNot;
    public ObjectPool<GameObject> meteoritePool;
    private void OnEnable()
    {
        AttackTarget();
        AttackDir();
    }
    private void Awake()
    {
        damage = PrefabManager.instance.player_Orb_Controller.damage * 0.8f;
        moveSpeed = PrefabManager.instance.player_Orb_Controller.moveSpeed * 0.8f;
        transform.localScale = PrefabManager.instance.player_Orb_Controller.transform.localScale * 3;
    }
    private void Update()
    {
        transform.Translate(attackDir * moveSpeed * Time.deltaTime);
        StartCoroutine(DestoryGameObject());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            ContactPoint2D cp = collision.GetContact(0);
            Vector3 vect = cp.normal;
            Vector3 reflecct = Vector3.Reflect(attackDir, vect);
            attackDir = reflecct;
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyStats>()?.TakeDamage(damage);
            //collision.transform.position = new Vector2(transform.position.x - attackDir.x, transform.position.y - attackDir.y);
            meteoritePool.Release(gameObject);
            attackDetects.Clear();
        }
    }
    public void AttackTarget()
    {
        attackDetects = new List<GameObject>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius);
        foreach (var target in colliders)
        {
            if (target.GetComponent<EnemyBase>() != null)
            {
                attackDetects.Add(target.gameObject);
                AttackLogic();
            }
        }
    }
    public IEnumerator DestoryGameObject()
    {
        yield return new WaitForSeconds(5);

        meteoritePool.Release(gameObject);
    }
    public void AttackDir() => attackDir = (attackTarget.position - transform.position).normalized;
    public void AttackLogic()
    {
        float distance = Mathf.Infinity;
        for (int i = 0; i < attackDetects.Count; i++)
        {
            if (distance > Vector3.Distance(attackDetects[i].transform.position, transform.position))
            {
                distance = Vector3.Distance(attackDetects[i].transform.position, transform.position);
                attackTarget = attackDetects[i].gameObject.transform;
            }
        }
    }
    public void AttackTakeDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.caster_Skill_Data.skill_2_explodeRadius);
        foreach (Collider2D hit in colliders)
        {
            if (hit.GetComponent<EnemyStats>() != null)
            {
                hit.GetComponent<EnemyStats>()?.TakeDamage(damage * (1 + DataManager.instance.caster_Skill_Data.skill_2_extraAddExplodeDamage));
                if (SkillManger.instance.caster_Skill.isHave_X_Equipment == true)
                    hit.GetComponent<EnemyBase>().layersOfBurning++;
            }
        }
    }
    public void OnDrawGizmos()
    {
        if (!drawTheBorderOrNot)
            Gizmos.DrawWireSphere(transform.position, DataManager.instance.caster_Skill_Data.skill_2_explodeRadius);
    }
}