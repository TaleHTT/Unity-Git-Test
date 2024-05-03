using UnityEngine;

public class PlayerSaberDefenseState : PlayerState
{
    Player_Saber player_Saber;
    float value;
    public PlayerSaberDefenseState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Saber player_Saber) : base(player, stateMachine, animboolName)
    {
        this.player_Saber = player_Saber;
    }

    public override void Enter()
    {
        base.Enter();
        player_Saber.isDefense = true;
        stateTimer = DataManager.instance.saber_Skill_Data.persistentTimer;
        value = player_Saber.stats.armor.GetValue() * DataManager.instance.saber_Skill_Data.extraAddArmor;
        player_Saber.stats.armor.baseValue += (value + 1);
    }

    public override void Exit()
    {
        base.Exit();
        player_Saber.stats.armor.baseValue -= (value + 1);
        player_Saber.isDefense = false;
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer <= 0)
            stateMachine.ChangeState(player_Saber.saberIdleState);
    }
}