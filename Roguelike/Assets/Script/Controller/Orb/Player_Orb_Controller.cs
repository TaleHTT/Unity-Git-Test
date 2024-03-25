using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Orb_Controller : Orb_Controller
{
    protected void Awake()
    {
        AttackTarget();
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") || collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(gameObject);
        }
    }
    public void AttackTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius);
        foreach (var target in colliders)
        {
            if (target.GetComponent<EnemyBase>() != null)
            {
                attackDetects.Add(target.transform);
                AttackLogic();
            }
        }
    }
}
