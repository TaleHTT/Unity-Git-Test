using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class DeerTotem_Controller : MonoBehaviour
{
    private float tiemr;
    public float damage { get; set; }
    public float Hp { get; set; }
    public float treat { get; set; }
    public ObjectPool<GameObject> deerTotemPool { get; set; }
    private void OnEnable()
    {
        tiemr = DataManager.instance.shaman_Skill_Data.skill_1_duration;
    }
    private void Update()
    {
        StartCoroutine(ClearNegativesAndTreat());
        tiemr -= Time.deltaTime;
        if (tiemr <= 0 || Hp <= 0)
            deerTotemPool.Release(gameObject);
    }
    private void OnDisable()
    {
        if (SkillManger.instance.shaman_Skill.isHave_X_Equipment == true)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.shaman_Skill_Data.skill_1_radius);
            foreach (var hit in colliders)
            {
                if (hit.GetComponent<EnemyBase>() != null)
                    hit.GetComponent<EnemyStats>().TakeDamage(damage);
            }
        }
    }
    public void Treat()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.shaman_Skill_Data.skill_1_radius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<PlayerBase>() != null)
            {
                hit.GetComponent<PlayerStats>().TakeTreat(treat);
            }
        }
    }
    public IEnumerator ClearNegativesAndTreat()
    {
        yield return new WaitForSeconds(1);
        Treat();
    }
}