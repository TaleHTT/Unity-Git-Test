public class PlayerAssassinAttackState : PlayerAssassinGroundState
{
    Player_Assassin player_Assassin;
    public PlayerAssassinAttackState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Assassin player_Assassin) : base(player, stateMachine, animboolName)
    {
        this.player_Assassin = player_Assassin;
    }

    public override void Enter()
    {
        base.Enter();
        if (player_Assassin.isStealth == true)
        {
            player_Assassin.assassinateTarget.GetComponent<EnemyStats>().TakeDamage(player_Assassin.stats.damage.GetValue() * 3);
            if (player_Assassin.assassinateTarget.GetComponent<EnemyBase>().isDead && SkillManger.instance.assassin_Skill.isHave_X_Equipment)
            {
                player.stats.currentHealth += player.stats.damage.GetValue() * (1 + DataManager.instance.assassin_Skill_Data.extraAddHp);
                player_Assassin.assassin_Skill_Controller.num_KillEnemy++;
            }
            player_Assassin.assassinateTarget.GetComponent<EnemyBase>().hit_Assassin++;
            if (player_Assassin.assassinateTarget.GetComponent<EnemyBase>().isHunting)
                player_Assassin.assassinateTarget.GetComponent<EnemyBase>().markDurationTimer = DataManager.instance.assassin_Skill_Data.skill_2_durationTimer;
            if (player_Assassin.assassinateTarget.GetComponent<EnemyBase>().isDead == true)
                stateMachine.ChangeState(player_Assassin.assassinStealthIdleState);
        }
        player_Assassin.isStealth = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (player_Assassin.enemyDetects.Count <= 0)
            stateMachine.ChangeState(player_Assassin.assassinIdleState);
    }
}