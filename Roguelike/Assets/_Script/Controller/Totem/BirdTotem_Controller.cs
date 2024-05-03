using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BirdTotem_Controller : MonoBehaviour
{
    public float ridius;

    [HideInInspector] public float Hp;
    [HideInInspector] public float timer;
    [HideInInspector] public float moveSpeedAdd;
    [HideInInspector] public float attackSpeedAdd;
    
    public ObjectPool<GameObject> birdTotemPool;
    [HideInInspector] public List<GameObject> targetDetect;
    public Dictionary<GameObject, float> moveSpeed = new Dictionary<GameObject, float>();
    public Dictionary<GameObject, float> attackSpeed = new Dictionary<GameObject, float>();
    protected virtual void OnEnable()
    {
        timer = DataManager.instance.shaman_Skill_Data.skill_2_duraiton;
    }
    protected virtual void Awake()
    {
        
    }
    protected virtual void Start()
    {
 
    }
    protected virtual void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 || Hp <= 0)
            birdTotemPool.Release(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ridius);
    }
}