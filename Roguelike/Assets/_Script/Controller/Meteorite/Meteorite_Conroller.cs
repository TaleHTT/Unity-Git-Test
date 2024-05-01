using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Meteorite_Conroller : MonoBehaviour
{
    public bool drawTheBorderOrNot;
    public ObjectPool<GameObject> meteoritePool;
    
    [HideInInspector] public float damage;
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public Vector3 attackDir;
    [HideInInspector] public Transform attackTarget;
    [HideInInspector] public List<GameObject> attackDetects;
    protected virtual void OnEnable()
    {

    }
    protected virtual void Awake()
    {

    }
    protected virtual void Update()
    {
        transform.Translate(attackDir * moveSpeed * Time.deltaTime);
        StartCoroutine(DestoryGameObject());
    }
    public IEnumerator DestoryGameObject()
    {
        yield return new WaitForSeconds(5);

        meteoritePool.Release(gameObject);
    }
    public void AttackDir() => attackDir = (attackTarget.position - transform.position).normalized;
    public void AttackLogic()
    {
        float distance = Mathf.Infinity;
        for (int i = 0; i < attackDetects.Count; i++)
        {
            if (distance > Vector3.Distance(attackDetects[i].transform.position, transform.position))
            {
                distance = Vector3.Distance(attackDetects[i].transform.position, transform.position);
                attackTarget = attackDetects[i].gameObject.transform;
            }
        }
    }
    public void OnDrawGizmos()
    {
        if (!drawTheBorderOrNot)
            Gizmos.DrawWireSphere(transform.position, DataManager.instance.caster_Skill_Data.skill_2_explodeRadius);
    }
}