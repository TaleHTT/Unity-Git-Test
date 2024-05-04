using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Player_Orb_Controller : Orb_Controller
{
    [HideInInspector] public Player_Caster_Skill_Controller player_Caster_Skill_Controller;
    protected override void OnEnable()
    {
        base.OnEnable();
        AttackTarget();
        AttackDir();
        transform.localScale = new Vector3(5, 5, 0);
        isStrengthen = false;
    }
    protected override void Awake()
    {
        base.Awake();
        burningRingsPool = new ObjectPool<GameObject>(CreateburningRingsFunc, ActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
    }
    protected override void Start()
    {
        base.Start();
        num = player_Caster_Skill_Controller.numberOfAttack;
    }
    protected override void Update()
    {
        base.Update();
        if(num >= DataManager.instance.caster_Skill_Data.maxNumberOfAttack)
        {
            transform.localScale *= 2; 
            isStrengthen = true;
            player_Caster_Skill_Controller.numberOfAttack = 0;
            num = player_Caster_Skill_Controller.numberOfAttack;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") || collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            if (isStrengthen == true)
            {
                burningTransform = collision.transform;
                burningRingsPool.Get();
            }
            AttackTakeDamage();
            orbPool.Release(gameObject);
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
        if(isStrengthen == false)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
            foreach (Collider2D hit in colliders)
            {
                if (hit.GetComponent<EnemyStats>() != null)
                {
                    hit.GetComponent<EnemyStats>()?.AuthenticTakeDamage(damage);
                    hit.GetComponent<EnemyBase>().isHit = true;
                    if (SkillManger.instance.caster_Skill.isHave_X_Equipment == true)
                        hit.GetComponent<EnemyBase>().layersOfBurning++;
                }
            }
        }
        else
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.caster_Skill_Data.skill_1_explodeRadius);
            foreach (Collider2D hit in colliders)
            {
                if (hit.GetComponent<EnemyStats>() != null)
                {
                    hit.GetComponent<EnemyStats>()?.AuthenticTakeDamage(strengthExplosionDamage);
                    hit.GetComponent<EnemyBase>().isHit = true;
                    if (SkillManger.instance.caster_Skill.isHave_X_Equipment == true)
                    {
                        hit.GetComponent<EnemyBase>().layersOfBurning++;
                    }
                }
            }
        }
    }
    public GameObject CreateburningRingsFunc()
    {
        var _object = Instantiate(burningRingsPrefab, burningTransform.position, Quaternion.identity);
        _object.GetComponent<Player_BurningRings_Controller>().burningRingsPool = burningRingsPool;
        return _object;
    }
}
