using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Orb_Controller : MonoBehaviour
{
    public bool isStrengthen;
    public float strengthExplosionRadius;
    public float strengthExplosionDamage;
    public int numberOfPenetrations;
    public ObjectPool<GameObject> orbPool;
    public ObjectPool<GameObject> burningRingsPool;
    [Tooltip("�ƶ��ٶ�")]
    public float moveSpeed;
    [Tooltip("�˺�")]
    public float damage;
    [Tooltip("����timer����ʸ�Զ�����")]
    public float timer;
    private float coolDownTimer;
    [Tooltip("��ը��Χ")]
    public float explosionRadius;
    [Tooltip("�Ƿ���ʵ��ը��Χ")]
    public bool drawTheBorderOrNot;
    public List<Transform> attackDetects;
    public float attackRadius { get; private set; } = Mathf.Infinity;
    public Transform attackTarget { get; private set; }
    public Vector3 arrowDir { get; private set; }
    public Vector2 defaultScale;
    public float cdDefaultRadius;
    private void Awake()
    {
        defaultScale = transform.localScale;
    }
    protected virtual void OnEnable()
    {
        List<Transform> attackDetects = new List<Transform>();
    }
    protected virtual void Update()
    {
        transform.Translate(arrowDir * moveSpeed * Time.deltaTime);
        coolDownTimer -= Time.deltaTime;
        if (coolDownTimer < 0)
        {
            coolDownTimer = timer;
            orbPool.Release(gameObject);
            attackDetects.Clear();
        }
    }
    public void AttackDir() => arrowDir = (attackTarget.position - transform.position).normalized;
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
        if (!drawTheBorderOrNot)
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
