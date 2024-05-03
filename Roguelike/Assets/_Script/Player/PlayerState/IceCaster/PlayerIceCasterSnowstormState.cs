using UnityEngine;

public class PlayerIceCasterSnowstormState : PlayerState
{
    float timer;
    float value;
    Player_IceCaster player_IceCaster;
    public PlayerIceCasterSnowstormState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_IceCaster player_IceCaster) : base(player, stateMachine, animboolName)
    {
        this.player_IceCaster = player_IceCaster;
    }

    public override void Enter()
    {
        base.Enter();
        value = player_IceCaster.stats.moveSpeed.GetValue();
        player_IceCaster.stats.moveSpeed.baseValue -= value * DataManager.instance.iceCasterSkill_Data.RemoveMoveSpeed;
        timer = DataManager.instance.iceCasterSkill_Data.skill_X_timer;
        stateTimer = DataManager.instance.iceCasterSkill_Data.skill_X_Duration;
        player_IceCaster.timer = DataManager.instance.iceCasterSkill_Data.skill_X_CD;
    }

    public override void Exit()
    {
        base.Exit();
        player_IceCaster.stats.moveSpeed.baseValue += value * DataManager.instance.iceCasterSkill_Data.RemoveMoveSpeed;
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer <= 0)
            stateMachine.ChangeState(player_IceCaster.iceCasterIdleState);
        else
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                player_IceCaster.TakeDamage();
                timer = DataManager.instance.iceCasterSkill_Data.skill_X_timer;
            }
        }
    }
}
