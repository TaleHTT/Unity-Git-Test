using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Player_Bloodsucker : PlayerBase
{
    public float meleeAttackRadius;
    public float remoteAttackRadius;
    public int position;
    public GameObject batPrefab;
    ObjectPool<GameObject> batPool;
    public PlayerBloodsuckerIdleState bloodsuckerIdleState { get; set; }
    public PlayerBloodsuckerDeadState bloodsuckerDeadState { get; set; }
    public PlayerBloodsuckerAttackState bloodsuckerAttackState { get; set; }
    protected override void Awake()
    {
        base.Awake();
        batPool = new ObjectPool<GameObject>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
        bloodsuckerIdleState = new PlayerBloodsuckerIdleState(this, stateMachine, "Idle", this);
        bloodsuckerAttackState = new PlayerBloodsuckerAttackState(this, stateMachine, "Attack", this);
        bloodsuckerDeadState = new PlayerBloodsuckerDeadState(this, stateMachine, "Dead", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(bloodsuckerIdleState);
    }
    protected override void Update()
    {
        base.Update();
    }
    public override void AnimationBloodsuckerAttack()
    {
        base.AnimationBloodsuckerAttack();
        batPool.Get();
    }
    private GameObject CreateFunc()
    {
        var bat = Instantiate(batPrefab, transform.position, Quaternion.identity);
        bat.GetComponent<Bat_Controller>().player_Bloodsucker = this;
        bat.GetComponent<Bat_Controller>().batPool = batPool;
        bat.GetComponent<Bat_Controller>().damage = stats.damage.GetValue();
        return bat;
    }
    private void ActionOnGet(GameObject bat)
    {
        bat.transform.position = transform.position;
        bat.SetActive(true);
    }
    private void ActionOnRelease(GameObject bat)
    {
        bat.SetActive(false);
    }
    private void ActionOnDestory(GameObject orb)
    {
        Destroy(orb);
    }
    public override void OnDrawGizmos()
    {
        if (!drawTheBorderOrNot)
            return;
        if (position == 0 || position == 1 || position == 2)
        {
            Gizmos.DrawWireSphere(transform.position, meleeAttackRadius);
        }
        else if (position == 3 || position == 4 || position == 5)
        {
            Gizmos.DrawWireSphere(transform.position, remoteAttackRadius);
        }
    }
    public override void EnemyDetect()
    {
        enemyDetects = new List<GameObject>();
        if(position == 0 || position == 1 || position == 2)
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, meleeAttackRadius, whatIsEnemy);
            foreach (var enemy in colliders)
            {
                enemyDetects.Add(enemy.gameObject);
            }
        }
        else
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, remoteAttackRadius, whatIsEnemy);
            foreach (var enemy in colliders)
            {
                enemyDetects.Add(enemy.gameObject);
            }
        }
    }
}