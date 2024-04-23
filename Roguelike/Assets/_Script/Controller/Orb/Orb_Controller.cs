using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Orb_Controller : MonoBehaviour
{
    [Tooltip("燃烧圈预制体")]
    public GameObject burningRingsPrefab;
    public ObjectPool<GameObject> burningRingsPool;
    public Transform burningTransform {  get; set; }
    public Caster_Skill_Controller caster_Skill_Controller {  get; set; }
    public bool isStrengthen;
    public float strengthExplosionDamage {  get; set; }
    public int numberOfPenetrations;
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
    private void Awake()
    {
        burningRingsPool = new ObjectPool<GameObject>(CreateburningRingsFunc, ActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
    }
    protected virtual void OnEnable()
    {
        coolDownTimer = timer;
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
    private GameObject CreateburningRingsFunc()
    {
        var _object = Instantiate(burningRingsPrefab, burningTransform.position, Quaternion.identity);
        _object.GetComponent<BurningRings_Controller>().burningRingsPool = burningRingsPool;
        return _object;
    }
    private void ActionOnGet(GameObject _object)
    {
        _object.transform.position = burningTransform.position;
        _object.SetActive(true);
    }
    private void ActionOnRelease(GameObject _object)
    {
        _object.SetActive(false);
    }
    private void ActionOnDestory(GameObject _object)
    {
        Destroy(_object);
    }
}
