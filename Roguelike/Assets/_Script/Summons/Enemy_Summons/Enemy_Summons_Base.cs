using System.Collections.Generic;
using UnityEngine;

public class Enemy_Summons_Base : EnemyBase
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        isDead = false;
    }
    protected override void Update()
    {
        base.Update();
    }
    public override void FixedUpdate()
    {

    }
}