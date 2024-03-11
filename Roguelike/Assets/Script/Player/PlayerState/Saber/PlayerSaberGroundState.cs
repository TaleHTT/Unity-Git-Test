using UnityEngine;

public class PlayerSaberGroundState : PlayerState
{
    public Vector3 target;
    public Vector3 direction;
    public Quaternion targetRotation;
    public Player_Saber player_Saber;
    public PlayerSaberGroundState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Saber player_Saber) : base(player, stateMachine, animboolName)
    {
        this.player_Saber = player_Saber;
    }

    public override void Enter()
    {
        base.Enter();
        target = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        direction = target - player.transform.position;
        targetRotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg);
    }
    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        EnemyDetect();
        Move();
    }
    public void Move()
    {
        if (player.transform.position != target)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, target, player.stats.moveSpeed.GetValue() * Time.deltaTime);
            if (Input.GetMouseButtonDown(0))
            {
                target = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
                direction = target - player.transform.position;
                player.transform.position = Vector3.MoveTowards(player.transform.position, target, player.stats.moveSpeed.GetValue() * Time.deltaTime);
            }
        }
        else
        {
            stateMachine.ChangeState(player_Saber.saberIdleState);
        }
    }
}
