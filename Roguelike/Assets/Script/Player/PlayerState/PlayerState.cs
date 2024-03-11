using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    public float initialAttackSpeed { get; private set; }
    public PlayerBase player { get; private set; }
    public PlayerStateMachine stateMachine { get; private set; }
    public bool triggerCalled;
    string animBoolName;

    public PlayerState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animboolName;
    }
    public virtual void Update()
    {

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
    public void EnemyDetect()
    {
        player.detectTimer -= Time.deltaTime;
        if (player.detectTimer > 0)
        {
            player.detectTimer = 1;
            return;
        }
        player.enemyDetects = new List<GameObject>();
        var colliders = Physics2D.OverlapCircleAll(player.transform.position, player.stats.attackRadius.GetValue(), player.whatIsEnemy);
        foreach (var enemy in colliders)
        {
            player.enemyDetects.Add(enemy.gameObject);
        }
    }
}
