public class PlayerPriestAttackState : PlayerState
{
    Player_Priest player_Priest;
    private int howPlayerMaxHp;
    public PlayerPriestAttackState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Priest player_Priest) : base(player, stateMachine, animboolName)
    {
        this.player_Priest = player_Priest;
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
        player.anim.speed = player.stats.attackSpeed.GetValue() + defaultAttackSpeed;
        for (int i = 0; i < player_Priest.playerDetects.Count; i++)
        {
            if (player_Priest.playerDetects[i].GetComponent<CharacterStats>().currentHealth == player_Priest.playerDetects[i].GetComponent<CharacterStats>().maxHp.GetValue())
                howPlayerMaxHp++;
        }
        if (howPlayerMaxHp == PlayerManager.instance.playerCount)
            stateMachine.ChangeState(player_Priest.priestIdleState);
    }
}
