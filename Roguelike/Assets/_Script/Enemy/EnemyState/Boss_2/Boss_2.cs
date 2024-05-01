using UnityEngine;
using UnityEngine.Pool;

public class Boss_2 : BossBase
{
    public int num;

    [Range(0, 1)] public float extraAddDamage;

    public ObjectPool<GameObject> pool;
    public GameObject arrowPrefab;
    public Boss_2_DeadState deadState { get; private set; }
    public Boss_2_IdleState idleState { get; private set; }
    public Boss_2_Skill_1_State skill_1_State { get; private set; }
    public Boss_2_Skill_2_State skill_2_State { get; private set; }
    public Boss_2_Skill_3_State skill_3_State { get; private set; }
    public Boss_2_Skill_4_State skill_4_State { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        pool = new ObjectPool<GameObject>(createFunc, actionOnGet, actionOnRelease, actionOnDestory, true, 10, 1000);
        deadState = new Boss_2_DeadState(this, stateMachine, "Dead", this);
        idleState = new Boss_2_IdleState(this, stateMachine, "Idle", this);
        skill_1_State = new Boss_2_Skill_1_State(this, stateMachine, "Skill_1", this);
        skill_2_State = new Boss_2_Skill_2_State(this, stateMachine, "Skill_2", this);
        skill_3_State = new Boss_2_Skill_3_State(this, stateMachine, "Skill_3", this);
        skill_4_State = new Boss_2_Skill_4_State(this, stateMachine, "Skill_4", this);
    }
    protected override void Update()
    {
        base.Update();
        float value = stats.currentHealth / stats.maxHp.GetValue();
        if (value == 0.25f || value == 0.5f || value == 0.75f)
        {
            timer_Cold = 0;
            markDurationTimer = 0;
            layersOfBleeding_Hound = 0;
            layersOfBleeding_Two_Handed_Saber = 0;
            for (int i = 0; i < 3; i++)
            {
                
            }
        }
    }
    private GameObject createFunc()
    {
        var orb = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        orb.GetComponent<Arrow_Controller>().damage = stats.damage.GetValue() * (1 + extraAddDamage);
        orb.GetComponent<Arrow_Controller>().pool = pool;
        return orb;
    }
    private void actionOnGet(GameObject orb)
    {
        orb.transform.position = transform.position;
        orb.SetActive(true);
    }
    private void actionOnRelease(GameObject orb)
    {
        orb.SetActive(false);
    }
    private void actionOnDestory(GameObject orb)
    {
        Destroy(orb);
    }
}