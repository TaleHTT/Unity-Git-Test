using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerOccupation
{
    Saber,
    Archer,
    Caster,
    Priest
}
public class PlayerBase : Entity
{
    public PlayerOccupation occupation;
    [Tooltip("死亡后，经过timer秒后销毁")]
    public float timer;
    [Tooltip("是否死亡")]
    public bool isDead;
    [Tooltip("是否显示攻击范围")]
    public bool drawTheBorderOrNot;
    public Transform closetEnemy;
    public List<GameObject> enemyDetects;
    public bool canBreakAwayFromTheTeam = false;
    public PlayerStateMachine stateMachine { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();
    }
    protected override void Start()
    {
        base.Start();
        isDead = false;
    }

    protected override void Update()
    {
        base.Update();
        AttackLogic();
        if (isDead)
            StartCoroutine(DeadDestroy(timer));

        stateMachine.currentState.Update();
    }
    public void OnDrawGizmos()
    {
        if (!drawTheBorderOrNot)
            return;
        Gizmos.DrawWireSphere(transform.position, stats.attackRadius.GetValue());
    }
    public override void DamageEffect()
    {
        base.DamageEffect();
    }
    IEnumerator DeadDestroy(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(this.gameObject);
    }
    public void AttackLogic()
    {
        float distance = Mathf.Infinity;
        for (int i = 0; i < enemyDetects.Count; i++)
        {
            if (distance > Vector3.Distance(enemyDetects[i].transform.position, transform.position))
            {
                distance = Vector3.Distance(enemyDetects[i].transform.position, transform.position);
                closetEnemy = enemyDetects[i].transform;
            }
        }
    }
    public void EnemyDetect()
    {
        detectTimer -= Time.deltaTime;
        if (detectTimer > 0)
        {
            detectTimer = 1;
            return;
        }
        enemyDetects = new List<GameObject>();
        var colliders = Physics2D.OverlapCircleAll(transform.position, stats.attackRadius.GetValue(), whatIsEnemy);
        foreach (var enemy in colliders)
        {
            enemyDetects.Add(enemy.gameObject);
        }
    }
    public virtual void AnimationArcherAttack()
    {

    }
    public virtual void AnimationCasterAttack()
    {

    }
}

