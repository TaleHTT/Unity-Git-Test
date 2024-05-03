public class PlayerTwoHandedSaberIdleState : PlayerState
{
    Player_TwoHandedSaber player_TwoHandedSaber;
    public PlayerTwoHandedSaberIdleState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_TwoHandedSaber player_TwoHandedSaber) : base(player, stateMachine, animboolName)
    {
        this.player_TwoHandedSaber = player_TwoHandedSaber;
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
        if (stateTimer < 0)
            player_TwoHandedSaber.two_Handed_Saber_Skill_Controller.numOfAttacks = 0;
        if (player_TwoHandedSaber.enemyDetects.Count > 0 && player_TwoHandedSaber.stats.isUseSkill == false)
            stateMachine.ChangeState(player_TwoHandedSaber.twoHandedSaberAttackState);
    }
}