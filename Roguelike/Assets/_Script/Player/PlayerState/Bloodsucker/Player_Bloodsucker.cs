﻿using UnityEngine;
using UnityEngine.Pool;

public class Player_Bloodsucker : PlayerBase
{
    public int position;
    public GameObject batPrefab;
    ObjectPool<GameObject> batPool;
    public PlayerBloodsuckerIdleState bloodsuckerIdleState { get; set; }
    public PlayerBloodsuckerDeadState bloodsuckerDeadState { get; set; }
    public PlayerBloodsuckerAttackState bloodsuckerAttackState { get; set; }
    protected override void Awake()
    {
        base.Awake();
        batPool = new ObjectPool<GameObject>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(bloodsuckerIdleState);
    }
    protected override void Update()
    {
        base.Update();
    }
    public override void AnimationBloodsuckerAttack()
    {
        base.AnimationBloodsuckerAttack();
        batPool.Get();
    }
    private GameObject CreateFunc()
    {
        var bat = Instantiate(batPrefab, transform.position, Quaternion.identity);
        bat.GetComponent<Bat_Controller>().player_Bloodsucker = this;
        bat.GetComponent<Bat_Controller>().batPool = batPool;
        return bat;
    }
    private void ActionOnGet(GameObject bat)
    {
        bat.transform.position = transform.position;
        bat.SetActive(true);
    }
    private void ActionOnRelease(GameObject bat)
    {
        bat.SetActive(false);
    }
    private void ActionOnDestory(GameObject orb)
    {
        Destroy(orb);
    }
}