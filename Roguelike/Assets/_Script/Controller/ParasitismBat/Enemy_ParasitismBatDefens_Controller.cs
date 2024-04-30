using UnityEngine;

public class Enemy_ParasitismBatDefens_Controller : ParasitismBatDefens_Controller
{
    public Enemy_Bloodsucker_Skill_Controller enemy_Bloodsucker_Skill_Controller;
    protected override void Update()
    {
        base.Update();
        if (enemy_Bloodsucker_Skill_Controller.duration <= 0 || enemy_Bloodsucker_Skill_Controller.enemy_Bloodsucker.stats.defensNum <= 0)
        {
            parasitismBatDefensPool.Release(gameObject);
        }
        angle += Time.deltaTime * 150;
        transform.position = enemy_Bloodsucker_Skill_Controller.transform.position;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}