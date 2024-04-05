public class PlayerState : IDeadLogciable
{
    public PlayerBase player { get; private set; }
    public PlayerStateMachine stateMachine { get; private set; }
    public float defaultAttackSpeed { get; private set; }
    public bool triggerCalled;
    private string animBoolName;
    public PlayerState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animboolName;
    }
    public virtual void Update()
    {
        player.EnemyDetect();
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
    public void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
    public void DeadLogci()
    {
        player.stats.attackRadius.baseValue = 0;
        player.cd.enabled = false;
        player.enemyDetects.Clear();
        player.anim.SetBool("Attack", false);
        player.isDead = true;
        PlayerManager.instance.playerCount--;
    }
}
