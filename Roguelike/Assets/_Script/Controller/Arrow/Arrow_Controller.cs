using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.Pool;

public class Arrow_Controller : MonoBehaviour
{
    [HideInInspector] public bool isFaceLeft = true;
    public ObjectPool<GameObject> pool;
    [Tooltip("移动速度")]
    public float moveSpeed;
    [Tooltip("伤害")]
    public float damage;
    [Tooltip("经过timer秒后箭矢自动销毁")]
    public float timer;
    private float coolDownTimer;
    public List<Transform> attackDetects;
    public float attackRadius { get; private set; } = Mathf.Infinity;
    public Transform attackTarget { get; private set; }
    public Vector3 arrowDir { get; private set; }
    protected virtual void OnEnable()
    {
        List<Transform> attackDetects = new List<Transform>();
        coolDownTimer = timer;
    }

    protected virtual void Update()
    {
        if(transform.position.x < 0 && !isFaceLeft)
        {
            Filp();
        }
        else if(transform.position.x > 0 && isFaceLeft)
        {
            Filp();
        }
        transform.Translate(arrowDir * moveSpeed * Time.deltaTime);
        coolDownTimer -= Time.deltaTime;
        if (coolDownTimer < 0)
        {
            coolDownTimer = timer;
            pool.Release(gameObject);
        }
    }
    public void ArrowDir() => arrowDir = (attackTarget.position - transform.position).normalized;
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
    public void Filp()
    {
        isFaceLeft = !isFaceLeft;
        transform.Rotate(0, 180, 0);
    }
}
