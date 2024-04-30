using Pathfinding;
using UnityEngine;

public class BossBase : Base
{
    public bool isTest;
    public LayerMask whatIsPlayer;
    public GameObject player { get; set; }
    public Seeker seeker { get; private set; }
    public EnemyStateMachine stateMachine { get; set; }
    protected override void Awake()
    {
        base.Awake();
        seeker = GetComponent<Seeker>();
        stateMachine = new EnemyStateMachine();
    }
    protected override void Start()
    {
        base.Start();
        isDead = false;
    }
    protected override void Update()
    {
        stateMachine.currentState.Update();
        if (isDead)
        {
            deadTimer -= Time.deltaTime;
            if (deadTimer < 0)
                gameObject.SetActive(false);
        }
        PlayerDetect();
        HuntingMark();
        Hound_Bleed();
        Two_Handed_Bleed();
        ColdEffect();
    }
    public void PlayerDetect()
    {
        if (isTest)
        {
            float distance = Mathf.Infinity;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Mathf.Infinity, whatIsPlayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (distance > Vector2.Distance(transform.position, colliders[i].transform.position))
                {
                    distance = Vector2.Distance(transform.position, colliders[i].transform.position);
                    player = colliders[i].gameObject;
                }
            }
        }
        else
        {
            player = GameObject.Find("TeamWheel").gameObject;
        }
    }
    public override void DamageEffect()
    {
        base.DamageEffect();
    }

    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

}