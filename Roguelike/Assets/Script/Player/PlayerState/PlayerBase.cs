using Cinemachine.Utility;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBase : Entity
{
    //public Vector3 playerAutoPathTarget;
    //public int currentIndex;
    //public int targetPointIndex = 0;
    //public List<Vector3> pathPointList;
    //public GameObject arrowhead;
    [Tooltip("死亡后，经过timer秒后销毁")]
    public float timer;
    [Tooltip("是否死亡")]
    public bool isDead;
    [Tooltip("是否显示攻击范围")]
    public bool drawTheBorderOrNot;
    public List<GameObject> enemyDetects;
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
        if(isDead)
        {
            StartCoroutine(DeadDestroy(timer));
        }
        stateMachine.currentState.Update();
        //MoveDir();
    }
    public void OnDrawGizmos()
    {
        if (!drawTheBorderOrNot)
            return;
        Gizmos.DrawWireSphere(transform.position, stats.attackRadius.GetValue());
    }
    public void DamageEffect()
    {
        Debug.Log("I am damage");
    }
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    public virtual void AnimationArcherAttack()
    {

    }
    IEnumerator DeadDestroy(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(this.gameObject);
    }
    public void AttackLogic()
    {
        if (enemyDetects.Count >= 3)
        {
            for (int i = 1; i < enemyDetects.Count - 1; i++)
            {
                stats.DoDamage((Vector2.Distance(transform.position, enemyDetects[i].transform.position) > Vector2.Distance(transform.position, enemyDetects[i + 1].transform.position)) ? ((Vector2.Distance(transform.position, enemyDetects[i].transform.position) > Vector2.Distance(transform.position, enemyDetects[i - 1].transform.position)) ? enemyDetects[i].GetComponent<EnemyStats>() : enemyDetects[i - 1].GetComponent<EnemyStats>()) : ((Vector2.Distance(transform.position, enemyDetects[i + 1].transform.position) > Vector2.Distance(transform.position, enemyDetects[i - 1].transform.position)) ? enemyDetects[i + 1].GetComponent<EnemyStats>() : enemyDetects[i - 1].GetComponent<EnemyStats>()));
            }
        }
        else if (enemyDetects.Count == 2)
        {
            for (int i = 0; i < enemyDetects.Count - 1; i++)
            {
                if (Vector2.Distance(transform.position, enemyDetects[i].transform.position) >
                    Vector2.Distance(transform.position, enemyDetects[i + 1].transform.position))
                {
                    enemyDetects[i].GetComponent<EnemyStats>();
                    stats.DoDamage(enemyDetects[i].GetComponent<EnemyStats>());

                }
                else
                {
                    enemyDetects[i + 1].GetComponent<EnemyStats>();
                    stats.DoDamage(enemyDetects[i].GetComponent<EnemyStats>());
                }
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
    //public void AutoPath()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        playerAutoPathTarget = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
    //        GeneratePath(playerAutoPathTarget);
    //    }
    //    if (pathPointList == null || pathPointList.Count == 0)
    //    {
    //        GeneratePath(playerAutoPathTarget);
    //    }
    //    else if (Vector2.Distance(transform.position, pathPointList[currentIndex]) <= .1f)
    //    {
    //        currentIndex++;
    //        if (currentIndex >= pathPointList.Count)
    //            GeneratePath(playerAutoPathTarget);
    //    }
    //}
    //public void GeneratePath(Vector3 target)
    //{
    //    currentIndex = 0;
    //    seeker.StartPath(transform.position, target, Path =>
    //    {
    //        pathPointList = Path.vectorPath;
    //    });
    //}
    //public virtual void MoveDir()
    //{
    //    Vector2 mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    Vector2 arrowheadposition = new Vector2(arrowhead.transform.position.x, arrowhead.transform.position.y);
    //    float angle = WhatAngle(mouseposition, arrowheadposition);
    //    arrowhead.transform.rotation = Quaternion.Euler(0, 0, angle);
    //}
    //public float WhatAngle(Vector2 a, Vector2 b)
    //{
    //    return Mathf.Atan2(a.y - b.y, a.x - b.x)*Mathf.Rad2Deg;
    //}
}

