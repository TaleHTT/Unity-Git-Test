using UnityEngine;

public class Player_BurningRings_Controller : BurningRings_Controller
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        burningDamage = (1 + DataManager.instance.caster_Skill_Data.skill_1_extraAddExplodeDamage) * PrefabManager.instance.player_Orb_Controller.damage;
    }
    protected override void Update()
    {
        base.Update();
        if (damageTimer <= 0)
        {
            Trigger();
            damageTimer = 1;
        }
    }
    public void Trigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.caster_Skill_Data.skill_1_explodeRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyBase>() != null)
            {
                hit.GetComponent<EnemyStats>().AuthenticTakeDamage(burningDamage);
                hit.GetComponent<EnemyBase>().isHit = true;
            }
        }
    }
}