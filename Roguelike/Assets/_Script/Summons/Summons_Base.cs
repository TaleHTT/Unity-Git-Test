using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Summons_Base : MonoBehaviour
{
    public float moveSpeed;
    [SerializeField] private float currentHp;
    public float maxHp {  get; set; }
    public float damage {  get; set; }
    public Animator anim {  get; set; }
    public bool isDead { get; set; }
    public float timer { get; set; }
    public Seeker seeker { get; set; }
    public float chaseRadius { get; set; }
    public float attackRadius {  get; set; }
    public CapsuleCollider2D cd { get; set; }
    public Transform cloestTarget { get; set; }
    public bool drawTheBorderOrNot { get; set; }
    public List<GameObject> attackDetects { get; set; }
    public ObjectPool<GameObject> houndPool {  get; set; }

    protected virtual void Awake()
    {
        currentHp = maxHp;
        seeker = GetComponent<Seeker>();
        anim = GetComponentInChildren<Animator>();
    }
    protected virtual void Start()
    {
        timer = SkillManger.instance.archer_Skill.persistentTimer;
    }
    protected virtual void Update()
    {
        UpdataHp();
    }

    private void UpdataHp()
    {
        if (currentHp > maxHp)
            currentHp = maxHp;
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
    public void TakeDamage(float damage)
    {
        currentHp -= damage;
    }
}
