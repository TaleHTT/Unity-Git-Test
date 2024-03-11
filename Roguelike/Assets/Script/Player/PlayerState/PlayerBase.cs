using Cinemachine.Utility;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : Entity
{
    public GameObject arrowhead;
    public List<GameObject> enemyDetects;
    public PlayerStateMachine stateMachine { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();
    }
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        MoveDir();
    }
    public void DamageEffect()
    {
        Debug.Log("I am damage");
    }
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, stats.attackRadius.GetValue());
    }
    public virtual void MoveDir()
    {
        Vector2 mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 arrowheadposition = new Vector2(arrowhead.transform.position.x, arrowhead.transform.position.y);
        float angle = WhatAngle(mouseposition, arrowheadposition);
        arrowhead.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    public float WhatAngle(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x)*Mathf.Rad2Deg;
    }
    public virtual void AnimationArcherAttack()
    {

    }
}

