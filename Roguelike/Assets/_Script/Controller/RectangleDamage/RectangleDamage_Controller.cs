using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class RectangleDamage_Controller : MonoBehaviour
{
    public ObjectPool<GameObject> rectanglePool;
    public float damage {  get; set; }
    List<GameObject> attackTargets;
    float timer;
    int currentNumOfAttack;
    const int maxAttackNum = 3;
    private void OnEnable()
    {
        timer = 1;
        currentNumOfAttack = 0;
    }
    private void Awake()
    {
        attackTargets = new List<GameObject>();
    }
    private void Start()
    {
        transform.localScale = new Vector2(DataManager.instance.bloodsucker_Skill_Data.length, DataManager.instance.bloodsucker_Skill_Data.width);
    }
    private void Update()
    {
        if(currentNumOfAttack >= maxAttackNum)
        {
            rectanglePool.Release(gameObject);
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                foreach (var hit in attackTargets)
                {
                    if (hit.GetComponent<EnemyBase>() != null)
                        hit.GetComponent<EnemyStats>().TakeDamage(damage);
                }
                currentNumOfAttack++;
                timer = 1;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            attackTargets.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            attackTargets.Remove(collision.gameObject);
        }
    }
}