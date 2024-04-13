using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Summons_Base : Base
{

    public ObjectPool<GameObject> houndPool;

    public Transform cloestTarget;
    public bool isDead { get; set; }
    public float timer { get; set; }
    public Seeker seeker { get; set; }
    public float chaseRadius { get; set; }
    public float attackRadius {  get; set; }
    public bool drawTheBorderOrNot { get; set; }
    public List<GameObject> attackDetects { get; set; }

    protected override void Awake()
    {
        seeker = GetComponent<Seeker>();
    }
    protected override void Start()
    {
        timer = SkillManger.instance.archer_Skill.persistentTimer;
    }
    protected override void Update()
    {

    }
    public void OnDrawGizmos()
    {
        if (!drawTheBorderOrNot)
            return;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
    public IEnumerator DeadDestroy(float timer)
    {
        yield return new WaitForSeconds(timer);
        houndPool.Release(gameObject);
    }
}
