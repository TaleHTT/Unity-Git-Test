using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class IceOrb_Controller : MonoBehaviour
{
    [HideInInspector] public bool isFaceLeft = true;
    public float moveSpeed;
    public ObjectPool<GameObject> orbPool;
    
    [HideInInspector] public float timer;
    [HideInInspector] public float damage;
    [HideInInspector] public Vector2 attckDir;
    [HideInInspector] public float coolDownTimer;
    [HideInInspector] public GameObject attackTarget;
    [HideInInspector] public List<GameObject> attackDetect;
    protected virtual void OnEnable()
    {
        coolDownTimer = timer;
    }
    protected virtual void Update()
    {
        if (transform.position.x < 0 && !isFaceLeft)
        {
            Filp();
        }
        else if (transform.position.x > 0 && isFaceLeft)
        {
            Filp();
        }
        coolDownTimer -= Time.deltaTime;
        if(coolDownTimer < 0)
        {
            orbPool.Release(gameObject);
            attackDetect.Clear();
        }
        transform.Translate(attckDir * moveSpeed * Time.deltaTime);
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
    public void Filp()
    {
        isFaceLeft = !isFaceLeft;
        transform.Rotate(0, 180, 0);
    }
}