using UnityEngine;
using UnityEngine.Pool;

public class BurningRings_Controller : MonoBehaviour
{
    public ObjectPool<GameObject> burningRingsPool;

    [HideInInspector] public float timer;
    [HideInInspector] public float burningDamage;
    [HideInInspector] public float damageTimer = 1;
    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        transform.localScale *= DataManager.instance.caster_Skill_Data.skill_1_explodeRadius;
        timer = DataManager.instance.caster_Skill_Data.duration;
    }
    protected virtual void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            burningRingsPool.Release(gameObject);
            return;
        }
        damageTimer -= Time.deltaTime;
    }
}