using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bat_Controller : MonoBehaviour
{
    public float timer;
    public float damage;
    public float moveSpeed;
    public float explodeRadius;
    
    private float coolDownTimer;
    [HideInInspector] public Vector2 attackDir;
    [HideInInspector] public GameObject attackTarget;
    [HideInInspector] public List<GameObject> attackDetects;
    [HideInInspector] public ObjectPool<GameObject> batPool;
    protected virtual void OnEnable()
    {
        coolDownTimer = timer;
    }
    protected virtual void Update()
    {
        transform.Translate(attackDir * moveSpeed * Time.deltaTime);
        coolDownTimer -= Time.deltaTime;
        if (coolDownTimer < 0)
        {
            coolDownTimer = timer;
            batPool.Release(gameObject);
            attackDetects.Clear();
        }
    }
    public void AttackDir() => attackDir = (attackTarget.transform.position - transform.position).normalized;
}