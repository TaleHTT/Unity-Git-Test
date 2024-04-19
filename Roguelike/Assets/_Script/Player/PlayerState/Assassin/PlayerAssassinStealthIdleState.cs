using UnityEngine;

public class PlayerAssassinStealthIdleState : PlayerState
{
    Player_Assassin player_Assassin;
    public PlayerAssassinStealthIdleState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Assassin player_Assassin) : base(player, stateMachine, animboolName)
    {
        this.player_Assassin = player_Assassin;
    }

    public override void Enter()
    {
        base.Enter();
        player_Assassin.isStealth = true;
        player_Assassin.durationTimer = DataManager.instance.assassin_Skill_Data.skill_1_durationTimer;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetMouseButton(0))
            stateMachine.ChangeState(player_Assassin.assassinMoveState);
        if (player_Assassin.enemyDetects.Count > 0)
            stateMachine.ChangeState(player_Assassin.assassinAttackState);
    }
}
