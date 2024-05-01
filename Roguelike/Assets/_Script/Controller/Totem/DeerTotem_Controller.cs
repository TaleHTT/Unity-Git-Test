using UnityEngine;
using UnityEngine.Pool;

public class DeerTotem_Controller : MonoBehaviour
{
    [HideInInspector] public float Hp;
    [HideInInspector] public float treat;
    [HideInInspector] public float deadTiemr;
    [HideInInspector] public float timer = 1;
    public ObjectPool<GameObject> deerTotemPool;
    protected virtual void OnEnable()
    {
        deadTiemr = DataManager.instance.shaman_Skill_Data.skill_1_duration;
    }
    protected virtual void Update()
    {
        deadTiemr -= Time.deltaTime;
        if (deadTiemr <= 0 || Hp <= 0)
            deerTotemPool.Release(gameObject);
        timer -= Time.deltaTime;
    }
}