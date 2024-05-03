using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Orb_Controller : MonoBehaviour
{
    [HideInInspector] public bool isFaceLeft = true;
    public float num;
    [Tooltip("燃烧圈预制体")]
    public GameObject burningRingsPrefab;
    public ObjectPool<GameObject> burningRingsPool;
    [HideInInspector] public Transform burningTransform;
    public bool isStrengthen;
    [HideInInspector]public float strengthExplosionDamage;
    public ObjectPool<GameObject> orbPool;
    [Tooltip("移动速度")]
    public float moveSpeed;
    [Tooltip("伤害")]
    public float damage;
    [Tooltip("经过timer秒后箭矢自动销毁")]
    public float timer;
    private float coolDownTimer;
    [Tooltip("爆炸范围")]
    public float explosionRadius;
    [Tooltip("是否现实爆炸范围")]
    public bool drawTheBorderOrNot;
    public List<Transform> attackDetects;
    public float attackRadius { get; private set; } = Mathf.Infinity;
    public Transform attackTarget { get; private set; }
    public Vector3 arrowDir { get; private set; }
    public float cdDefaultRadius;
    protected virtual void Awake()
    {
       
    }
    protected virtual void OnEnable()
    {
        coolDownTimer = timer;
    }
    protected virtual void Start()
    {
        
    }
    protected virtual void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 180 + Mathf.Atan2(arrowDir.y, arrowDir.x) * Mathf.Rad2Deg);
        coolDownTimer -= Time.deltaTime;
        if (coolDownTimer < 0)
        {
            coolDownTimer = timer;
            orbPool.Release(gameObject);
            attackDetects.Clear();
        }
    }
    private void FixedUpdate()
    {
        transform.position += arrowDir * Time.fixedDeltaTime * moveSpeed;
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
    public void ActionOnGet(GameObject _object)
    {
        _object.transform.position = burningTransform.position;
        _object.SetActive(true);
    }
    public void ActionOnRelease(GameObject _object)
    {
        _object.SetActive(false);
    }
    public void ActionOnDestory(GameObject _object)
    {
        Destroy(_object);
    }
}
