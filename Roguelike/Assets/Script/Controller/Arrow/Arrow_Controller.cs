using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Controller : MonoBehaviour
{
    [Tooltip("�ƶ��ٶ�")]
    public float moveSpeed;
    [Tooltip("�˺�")]
    public float damage;
    [Tooltip("����timer����ʸ�Զ�����")]
    public float timer;
    public List<Transform> attackDetects;
    public float attackRadius { get; private set; } = Mathf.Infinity;
    public Transform attackTarget {  get; private set; }
    public Vector3 arrowDir { get; private set; }
    protected virtual void Start()
    {
        arrowDir = (attackTarget.position - transform.position).normalized;
    }
    protected virtual void Update()
    {
        transform.Translate(arrowDir * moveSpeed * Time.deltaTime);
        timer -= Time.deltaTime;
        if (timer < 0)
            Destroy(gameObject);
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
}
