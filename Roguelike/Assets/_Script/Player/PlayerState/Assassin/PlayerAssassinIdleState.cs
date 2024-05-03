using UnityEngine;

public class PlayerAssassinIdleState : PlayerState
{
    Player_Assassin player_Assassin;
    float timer = 2f;
    public PlayerAssassinIdleState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Assassin player_Assassin) : base(player, stateMachine, animboolName)
    {
        this.player_Assassin = player_Assassin;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 3;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            Heal();
            timer = 1f;
        }
        if (player_Assassin.enemyDetects.Count > 0)
            stateMachine.ChangeState(player_Assassin.assassinAttackState);
        if (stateTimer <= 0)
            stateMachine.ChangeState(player_Assassin.assassinStealthIdleState);
    }
}