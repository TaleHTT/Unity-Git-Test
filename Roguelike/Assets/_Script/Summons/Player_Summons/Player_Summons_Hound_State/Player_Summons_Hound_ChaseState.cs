using UnityEngine;

public class Player_Summons_Hound_ChaseState : Player_Summons_State
{
    private Vector3 target;
    public Player_Summons_Hound_ChaseState(Player_Summons_Hound summons_Hound, Player_Summons_StateMachine stateMachine, string animBoolName) : base(summons_Hound, stateMachine, animBoolName)
    {
    }

    public override void Eixt()
    {
        base.Eixt();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        if (player_Summons_Hound.enemyDetects.Count <= 0)
            stateMachine.ChangeState(player_Summons_Hound.houndIdleState);
        AutoPath();
        if (pathPointList == null)
            return;
        target = pathPointList[currentIndex];
        player_Summons_Hound.transform.position = Vector3.MoveTowards(player_Summons_Hound.transform.position, target, player_Summons_Hound.moveSpeed * Time.deltaTime);
    }
}