using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    public float initialAttackSpeed { get; private set; }
    public PlayerBase player { get; private set; }
    public PlayerStateMachine stateMachine { get; private set; }
    public bool triggerCalled;
    public string animBoolName;
    public float stateTimer;


    public PlayerState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animboolName;
    }
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        player.EnemyDetect();
    }
    public virtual void Enter()
    {
        initialAttackSpeed = player.anim.speed;
        player.anim.SetBool(animBoolName, true);
        triggerCalled = false;
    }
    public virtual void Exit()
    {
        player.anim.speed = initialAttackSpeed;
        player.anim.SetBool(animBoolName, false);
    }
    public void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
