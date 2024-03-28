using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Orb_Controller : MonoBehaviour
{
    public ObjectPool<GameObject> pool;
    [Tooltip("�ƶ��ٶ�")]
    public float moveSpeed;
    [Tooltip("�˺�")]
    public float damage;
    [Tooltip("����timer����ʸ�Զ�����")]
    public float timer;
    [Tooltip("��ը��Χ")]
    public float explosionRadius;
    [Tooltip("�Ƿ���ʵ��ը��Χ")]
    public bool drawTheBorderOrNot;
    public List<Transform> attackDetects;
    public float attackRadius { get; private set; } = Mathf.Infinity;
    public Transform attackTarget { get; private set; }
    public Vector3 arrowDir { get; private set; }
    public SpriteRenderer sr { get; private set; }
    public Rigidbody2D rb { get; private set; }
    protected virtual void Awake()
    {
        List<Transform> attackDetects = new List<Transform>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    protected virtual void Start()
    {
        arrowDir = (attackTarget.position - transform.position).normalized;
    }
    protected virtual void Update()
    {
        transform.Translate(arrowDir * moveSpeed * Time.deltaTime);
        timer -= Time.deltaTime;
        if (timer < 0)
            pool.Release(gameObject);
    }
    protected virtual void OnDestroy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hit in colliders)
        {
            if (hit.GetComponent<CharacterStats>() != null)
            {
                hit.GetComponent<CharacterStats>().remoteTakeDamage(damage);
            }
        }
    }
    public void AttackLogic()
    {
        float distance = Mathf.Infinity;
        for (int i = 0; i < attackDetects.Count; i++)
        {
            if (distance > Vector3.Distance(attackDetects[i].transform.position, transform.position))
            {
                distance = Vector3.Distance(attackDetects[i].transform.position, transform.position);
                attackTarget = attackDetects[i].transform;
            }
        }
    }
    public void OnDrawGizmos()
    {
        if(!drawTheBorderOrNot)
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
