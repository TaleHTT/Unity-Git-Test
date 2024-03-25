using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Arrow_Controller : Arrow_Controller
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            collision.GetComponent<CharacterStats>()?.remoteTakeDamage(damage);
            Destroy(gameObject);
        }
    }
    public void AttackTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius);
        foreach (var target in colliders)
        {
            if (target.GetComponent<PlayerBase>() != null)
            {
                attackDetects.Add(target.transform);
                AttackLogic();
            }
        }
    }
}