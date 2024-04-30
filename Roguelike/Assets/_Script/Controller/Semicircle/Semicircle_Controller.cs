using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Semicircle_Controller : MonoBehaviour
{
    public bool isCanAttack;
    public int angle;
    private int a;
    public float damage {  get; set; }
    public ObjectPool<GameObject> pool;
    List<GameObject> attackTarget = new List<GameObject>();
    private void OnEnable()
    {
        isCanAttack = false;
        a = Random.Range(0, 2);
    }
    private void Awake()
    {
        if (a == 0)
            angle = 0;
        else if (a == 1)
            angle = 180;
    }
    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    private void Update()
    {
        if (isCanAttack)
        {
            for(int i = 0; i < attackTarget.Count; ++i)
            {
                attackTarget[i].GetComponent<PlayerStats>().TakeDamage(damage);
                attackTarget[i].GetComponent<PlayerBase>().isHit = true;
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