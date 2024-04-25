using UnityEngine;
using UnityEngine.Pool;

public class Boss : BossBase
{
    public int num;
    public float timer;
    public int playerNum;
    public float sprintSpeed;
    public bool isFinishSkill;

    [Range(0, 1)] public float extraAddDamage;

    public float angle;
    public ObjectPool<GameObject> pool;
    public ObjectPool<GameObject> houndPool;
    public GameObject semicirclePerfab;
    public GameObject hound;

    public BossDeadState deadState { get; private set; }
    public BossIdleState idleState { get; private set; }
    public BossSkill_1_State skill_1_State { get; private set; }
    public BossSkill_2_State skill_2_State { get; private set; }
    public BossSkill_3_State skill_3_State { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        timer = 3;
        pool = new ObjectPool<GameObject>(createFunc, actionOnGet, actionOnRelease, actionOnDestory, true, 10, 1000);
        houndPool = new ObjectPool<GameObject>(createHoundFunc, actionOnGet, actionOnRelease, actionOnDestory, true, 10, 1000);
        deadState = new BossDeadState(this, stateMachine, "Dead", this);
        idleState = new BossIdleState(this, stateMachine, "Idle", this);
        skill_1_State = new BossSkill_1_State(this, stateMachine, "Skill_1", this);
        skill_2_State = new BossSkill_2_State(this, stateMachine, "Skill_2", this);
        skill_3_State = new BossSkill_3_State(this, stateMachine, "Skill_3", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(skill_1_State);

    }
    protected override void Update()
    {
        base.Update();
        float value = stats.currentHealth / stats.maxHp.GetValue();
        if(value == 0.25f || value == 0.5f || value == 0.75f)
        {
            timer -= Time.deltaTime;
            if(timer < 0)
            {
                timer = 3;
                stats.isUnconquered = false;
            }
            stats.isUnconquered = true;
            timer_Cold = 0;
            markDurationTimer = 0;
            layersOfBleeding_Hound = 0;
            layersOfBleeding_Two_Handed_Saber = 0;
            for (int i = 0; i < playerNum * 3; i++)
            {
                houndPool.Get();
            }
        }
    }
    private GameObject createHoundFunc()
    {
        float a = Random.Range(-1, 2);
        float b = Random.Range(-1, 2);
        var objects = Instantiate(semicirclePerfab, new Vector2(transform.position.x + a, transform.position.y + b), Quaternion.identity);
        objects.GetComponent<Semicircle_Controller>().pool = pool;
        return objects;
    }
    private GameObject createFunc()
    {
        var objects = Instantiate(semicirclePerfab, transform.position, Quaternion.Euler(0, 0, angle));
        objects.GetComponent<Semicircle_Controller>().pool = pool;
        objects.GetComponent<Semicircle_Controller>().damage = stats.damage.GetValue() * extraAddDamage;
        return objects;
    }
    private void actionHoundOnGet(GameObject objects)
    {
        float a = Random.Range(-1, 2);
        float b = Random.Range(-1, 2);
        objects.transform.position = new Vector2(transform.position.x + a, transform.position.y + b);
        objects.SetActive(true);
    }
    private void actionOnGet(GameObject objects)
    {
        objects.transform.position = transform.position;
        objects.transform.rotation = Quaternion.Euler(0, 0, angle);
        objects.SetActive(true);
    }
    private void actionOnRelease(GameObject objects)
    {
        objects.SetActive(false);
    }
    private void actionOnDestory(GameObject objects)
    {
        Destroy(objects);
    }
}