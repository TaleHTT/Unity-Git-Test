using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Player_Priest : PlayerBase
{
    [Tooltip("’Ê…À‘§÷∆ÃÂ")]
    public GameObject authenticPerfab;

    private ObjectPool<GameObject> authenticPool;
    [HideInInspector] public List<GameObject> playerDetects;

    public PlayerPriestIdleState priestIdleState { get; private set; }
    public PlayerPriestDeadState priestDeadState { get; private set; }
    public PlayerPriestAttackState priestAttackState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        authenticPool = new ObjectPool<GameObject>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
        priestIdleState = new PlayerPriestIdleState(this, stateMachine, "Idle", this);
        priestDeadState = new PlayerPriestDeadState(this, stateMachine, "Dead", this);
        priestAttackState = new PlayerPriestAttackState(this, stateMachine, "Attack", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(priestIdleState);
    }
    protected override void Update()
    {
        base.Update();
        if (stats.currentHealth <= 0 && isDead == false)
            stateMachine.ChangeState(priestDeadState);
    }
    public override void AnimationPriestAttack()
    {
        base.AnimationPriestAttack();
        authenticPool.Get();
    }
    private GameObject CreateFunc()
    {
        var orb = Instantiate(authenticPerfab, transform.position, Quaternion.identity);
        orb.GetComponent<Authentic_Controller>().authenticPool = authenticPool;
        return orb;
    }
    private void ActionOnGet(GameObject orb)
    {
        orb.transform.position = transform.position;
        orb.SetActive(true);
    }
    private void ActionOnRelease(GameObject orb)
    {
        orb.SetActive(false);
    }
    private void ActionOnDestory(GameObject orb)
    {
        Destroy(orb);
    }
}
