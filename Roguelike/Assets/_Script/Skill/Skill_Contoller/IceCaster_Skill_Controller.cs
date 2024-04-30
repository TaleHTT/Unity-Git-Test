using System.Collections.Generic;
using UnityEngine;

public class IceCaster_Skill_Controller : Skill_Controller
{
    [HideInInspector] public float timer;
    [HideInInspector] public float skill_1_Tiemr;
    [HideInInspector] public float durationTimer;
    [HideInInspector] public float skill_2_Tiemr;
    [HideInInspector] public GameObject cloestTarget;
    [HideInInspector] public List<GameObject> attackDetect;
    protected virtual void Awake()
    {
        
    }
    protected virtual void Start()
    {
        durationTimer = DataManager.instance.iceCasterSkill_Data.skill_1_durationTimer;
        skill_1_Tiemr = DataManager.instance.iceCasterSkill_Data.skill_1_CD;
        skill_2_Tiemr = DataManager.instance.iceCasterSkill_Data.skill_2_CD;
        timer = DataManager.instance.iceCasterSkill_Data.skill_1_timer;
    }
    protected virtual void Update()
    {
        skill_1_Tiemr -= Time.deltaTime;
        skill_2_Tiemr -= Time.deltaTime;
    }
    public void SectorEnemyTarget()
    {
        float distance = Mathf.Infinity;
        for (int i = 0; i < attackDetect.Count; i++)
        {
            if (distance > Vector2.Distance(transform.position, attackDetect[i].transform.position))
            {
                distance = Vector2.Distance(transform.position, attackDetect[i].transform.position);
                cloestTarget = attackDetect[i];
            }
        }
        if (attackDetect.Count > 0)
        {
            Vector2 attackDir = (transform.position - cloestTarget.transform.position).normalized;

            float x = 1 * Mathf.Cos(Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg * Mathf.Deg2Rad);
            float y = 1 * Mathf.Sin(Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg * Mathf.Deg2Rad);

            float cloestEnemyAngle = Mathf.Atan2(attackDir.y, attackDir.x);

            for (int i = 0; i < attackDetect.Count; i++)
            {
                if (Mathf.Atan2(attackDetect[i].transform.position.y, attackDetect[i].transform.position.x) * Mathf.Rad2Deg > cloestEnemyAngle + 30 || Mathf.Atan2(attackDetect[i].transform.position.y, attackDetect[i].transform.position.x) < cloestEnemyAngle - 30)
                    attackDetect.Remove(attackDetect[i]);
            }
        }
    }
}