using UnityEngine;

public class PlayerTwoHandedSaberStormBladesState : PlayerState
{
    Player_TwoHandedSaber player_TwoHandedSaber;
    public PlayerTwoHandedSaberStormBladesState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_TwoHandedSaber player_TwoHandedSaber) : base(player, stateMachine, animboolName)
    {
        this.player_TwoHandedSaber = player_TwoHandedSaber;
    }

    public override void Enter()
    {
        base.Enter();
        player_TwoHandedSaber.isAttack = true;
        player_TwoHandedSaber.stats.isUseSkill = true;
        player_TwoHandedSaber.cdTimer = DataManager.instance.two_Handed_Saber_Skill_Data.CD;
        stateTimer = DataManager.instance.two_Handed_Saber_Skill_Data.skill_2_DurationTimer;
    }

    public override void Exit()
    {
        base.Exit();
        player_TwoHandedSaber.isAttack = false;
        player_TwoHandedSaber.stats.isUseSkill = false;
    }

    public override void Update()
    {
        base.Update();
        player_TwoHandedSaber.isDead = false;
        if (player_TwoHandedSaber.stats.currentHealth <= 1)
            player_TwoHandedSaber.stats.currentHealth = 1;
    }
}
