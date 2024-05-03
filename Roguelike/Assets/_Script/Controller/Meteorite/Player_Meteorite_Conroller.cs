using System.Collections.Generic;
using UnityEngine;

public class Player_Meteorite_Conroller : Meteorite_Conroller
{
    protected override void OnEnable()
    {
        base.OnEnable();
        AttackTarget();
        AttackDir();
    }
    protected override void Awake()
    {
        base.Awake();
        damage = PrefabManager.instance.player_Orb_Controller.damage * 0.8f;
        moveSpeed = PrefabManager.instance.player_Orb_Controller.moveSpeed * 0.8f;
        transform.localScale = PrefabManager.instance.player_Orb_Controller.transform.localScale * 3;
    }
    protected override void Update()
    {
        base.Update();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            ContactPoint2D cp = collision.GetContact(0);
            Vector3 vect = cp.normal;
            Vector3 reflecct = Vector3.Reflect(attackDir, vect);
            attackDir = reflecct;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            AttackTakeDamage();
            //collision.transform.position = new Vector2(transform.position.x - attackDir.x, transform.position.y - attackDir.y);
            meteoritePool.Release(gameObject);
            attackDetects.Clear();
        }
    }
    public void AttackTarget()
    {
        attackDetects = new List<GameObject>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Mathf.Infinity);
        foreach (var target in colliders)
        {
            if (target.GetComponent<EnemyBase>() != null)
            {
                attackDetects.Add(target.gameObject);
                AttackLogic();
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
                hit.GetComponent<EnemyStats>()?.TakeDamage(damage * DataManager.instance.caster_Skill_Data.skill_2_extraAddExplodeDamage + DataManager.instance.caster_Skill_Data.meteoriteDamageBaseValue);
                hit.GetComponent<EnemyBase>().isHit = true;
                if (SkillManger.instance.caster_Skill.isHave_X_Equipment == true)
                    hit.GetComponent<EnemyBase>().layersOfBurning++;
            }
        }
    }
}