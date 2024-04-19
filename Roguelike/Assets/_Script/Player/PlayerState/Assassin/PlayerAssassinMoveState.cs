public class PlayerAssassinMoveState : PlayerAssassinGroundState
{
    Player_Assassin player_Assassin;
    public PlayerAssassinMoveState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Assassin player_Assassin) : base(player, stateMachine, animboolName)
    {
        this.player_Assassin = player_Assassin;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (player_Assassin.enemyDetects.Count > 0)
            stateMachine.ChangeState(player_Assassin.assassinAttackState);
    }
}