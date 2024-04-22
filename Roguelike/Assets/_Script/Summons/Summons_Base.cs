using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Summons_Base : MonoBehaviour
{
    public float moveSpeed;
    public float currentHp;
    public float maxHp;
    public float damage;
    public Animator anim {  get; set; }
    public bool isDead { get; set; }
    public float timer { get; set; }
    public float chaseRadius;
    public float attackRadius;
    public Transform cloestTarget;
    public bool drawTheBorderOrNot;
    public Seeker seeker { get; set; }
    public List<GameObject> attackDetects;
    public CapsuleCollider2D cd { get; set; }
    public ObjectPool<GameObject> houndPool {  get; set; }
    private void OnEnable()
    {
        currentHp = maxHp;
    }
    protected virtual void Awake()
    {
        seeker = GetComponent<Seeker>();
        anim = GetComponentInChildren<Animator>();
    }
    protected virtual void Start()
    {

    }
    protected virtual void Update()
    {
        UpdataHp();
        if (currentHp <= 0)
            StartCoroutine(DeadDestroy(timer));
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
