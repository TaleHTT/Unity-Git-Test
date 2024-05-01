public class EnemySaberDefenseState : EnemySaberGroundState
{
    float value;
    public EnemySaberDefenseState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Saber enemy_Saber) : base(enemy, stateMachine, animboolName, enemy_Saber)
    {
    }
    public override void Enter()
    {
        base.Enter();
        enemy_Saber.isDefense = true;
        stateTimer = DataManager.instance.saber_Skill_Data.persistentTimer;
        value = enemy_Saber.stats.armor.GetValue() * DataManager.instance.saber_Skill_Data.extraAddArmor;
        enemy_Saber.stats.armor.baseValue += (value + 1);
    }

    public override void Exit()
    {
        base.Exit();
        enemy_Saber.stats.armor.baseValue -= (value + 1);
        enemy_Saber.isDefense = false;
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer <= 0)
            stateMachine.ChangeState(enemy_Saber.saberIdleState);
    }
}
