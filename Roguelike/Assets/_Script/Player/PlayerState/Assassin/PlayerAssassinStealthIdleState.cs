using UnityEngine;

public class PlayerAssassinStealthIdleState : PlayerState
{
    float value;
    float timer;
    Player_Assassin player_Assassin;
    public PlayerAssassinStealthIdleState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Assassin player_Assassin) : base(player, stateMachine, animboolName)
    {
        this.player_Assassin = player_Assassin;
    }

    public override void Enter()
    {
        base.Enter();
        value = player_Assassin.stats.moveSpeed.GetValue();
        player_Assassin.isStrengthen = true;
        player_Assassin.isStealth = true;
        player_Assassin.durationTimer = DataManager.instance.assassin_Skill_Data.skill_1_durationTimer;
        player_Assassin.stats.moveSpeed.baseValue += value * DataManager.instance.assassin_Skill_Data.extraMoveSpeed;
    }

    public override void Exit()
    {
        base.Exit();
        player_Assassin.stats.moveSpeed.baseValue -= value * DataManager.instance.assassin_Skill_Data.extraMoveSpeed;
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
        if (Input.GetMouseButton(0) && player_Assassin.isTest == false)
            stateMachine.ChangeState(player_Assassin.assassinMoveState);
        if (player_Assassin.enemyDetects.Count > 0)
            stateMachine.ChangeState(player_Assassin.assassinAttackState);
    }
}
