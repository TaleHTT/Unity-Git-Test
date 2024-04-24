using UnityEngine;

public class PlayerState
{
    public PlayerBase player { get; private set; }
    public PlayerStateMachine stateMachine { get; private set; }
    public float stateTimer {  get; set; }
    public float defaultAttackSpeed { get; private set; }
    public bool triggerCalled { get; set; }
    private string animBoolName;
    public PlayerState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animboolName;
    }
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }
    public virtual void Enter()
    {
        defaultAttackSpeed = player.anim.speed;
        player.anim.SetBool(animBoolName, true);
        triggerCalled = false;
    }
    public virtual void Exit()
    {
        player.anim.speed = defaultAttackSpeed;
        player.anim.SetBool(animBoolName, false);
    }
    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
    public void DeadLogic()
    {
        player.attackRadius = 0;
        player.cd.enabled = false;
        player.enemyDetects.Clear();
        player.anim.SetBool("Attack", false);
        player.isDead = true;
        PlayerTeamManager.Instance.currentPlayerNum--;
    }
}
