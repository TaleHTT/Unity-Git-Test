using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class IceOrb_Controller : MonoBehaviour
{
    public float timer;
    public float moveSpeed;
    public ObjectPool<GameObject> orbPool;
    
    [HideInInspector] public float damage;
    public Vector3 attckDir;
    [HideInInspector] public float coolDownTimer;
    [HideInInspector] public bool isFaceLeft = true;
    [HideInInspector] public GameObject attackTarget;
    public List<GameObject> attackDetect;
    protected virtual void OnEnable()
    {
        coolDownTimer = timer;
    }
    protected virtual void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 180 + Mathf.Atan2(attckDir.y, attckDir.x) * Mathf.Rad2Deg);
        coolDownTimer -= Time.deltaTime;
        if(coolDownTimer < 0)
        {
            coolDownTimer = timer;
            orbPool.Release(gameObject);
            attackDetect.Clear();
        }
    }
    private void FixedUpdate()
    {
        transform.position += attckDir * Time.fixedDeltaTime * moveSpeed;
    }
    public void MoveDir() => attckDir = (attackTarget.transform.position - transform.position).normalized;
    public void AttackTarget()
    {
        float distance = Mathf.Infinity;
        for (int i = 0; i < attackDetect.Count; i++)
        {
            if (distance > Vector2.Distance(transform.position, attackDetect[i].transform.position))
            {
                distance = Vector2.Distance(transform.position, attackDetect[i].transform.position);
                attackTarget = attackDetect[i];
            }
        }
    }
}