using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Semicircle_Controller : MonoBehaviour
{
    public bool isCanAttack;
    public float damage {  get; set; }
    public ObjectPool<GameObject> pool;
    List<GameObject> attackTarget = new List<GameObject>();
    private void OnEnable()
    {
        isCanAttack = false;
    }
    private void Update()
    {
        if (isCanAttack)
        {
            for(int i = 0; i < attackTarget.Count; ++i)
            {
                attackTarget[i].GetComponent<PlayerStats>().TakeDamage(damage);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            attackTarget.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            attackTarget.Remove(collision.gameObject);
        }
    }
}