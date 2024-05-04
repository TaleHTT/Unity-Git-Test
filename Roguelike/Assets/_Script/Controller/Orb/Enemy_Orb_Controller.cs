using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy_Orb_Controller : Orb_Controller
{
    [HideInInspector] public Enemy_Caster_Skill_Controller enemy_Caster_Skill_Controller;
    protected override void OnEnable()
    {
        base.OnEnable();
        AttackTarget();
        AttackDir();
    }
    protected override void Awake()
    {
        base.Awake();
        burningRingsPool = new ObjectPool<GameObject>(CreateburningRingsFunc, ActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
    }
    protected override void Start()
    {
        base.Start();
        num = enemy_Caster_Skill_Controller.numberOfAttack;
    }
    protected override void Update()
    {
        base.Update();
        if (num >= DataManager.instance.caster_Skill_Data.maxNumberOfAttack)
        {
            transform.localScale = new Vector2(2, 2);
            isStrengthen = true;
            enemy_Caster_Skill_Controller.numberOfAttack = 0;
            num = enemy_Caster_Skill_Controller.numberOfAttack;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
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
            if(target.GetComponent<PlayerBase>() != null)
            {
                attackDetects.Add(target.gameObject);
                AttackLogic();
            }
        }
    }
    public void AttackTakeDamage()
    {
        if (isStrengthen == false)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
            foreach (Collider2D hit in colliders)
            {
                if (hit.GetComponent<PlayerStats>() != null)
                {
                    hit.GetComponent<PlayerStats>()?.AuthenticTakeDamage(damage);
                    hit.GetComponent<PlayerBase>().isHit = true;
                }
            }
        }
        else
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.caster_Skill_Data.skill_1_explodeRadius);
            foreach (Collider2D hit in colliders)
            {
                if (hit.GetComponent<PlayerStats>() != null)
                {
                    hit.GetComponent<PlayerStats>()?.AuthenticTakeDamage(strengthExplosionDamage);
                    hit.GetComponent<PlayerBase>().isHit = true;
                }
            }
        }
    }
    public GameObject CreateburningRingsFunc()
    {
        var _object = Instantiate(burningRingsPrefab, burningTransform.position, Quaternion.identity);
        _object.GetComponent<Enemy_BurningRings_Controller>().burningRingsPool = burningRingsPool;
        return _object;
    }
}
