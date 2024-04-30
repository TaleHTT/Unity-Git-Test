using UnityEngine;

public class Player_ParasitismBatDefens_Controller : ParasitismBatDefens_Controller
{
    public Player_Bloodsucker_Skill_Controller player_Bloodsucker_Skill_Controller;
    protected override void Update()
    {
        base.Update();
        if (player_Bloodsucker_Skill_Controller.duration <= 0 || player_Bloodsucker_Skill_Controller.player_Bloodsucker.stats.defensNum <= 0)
        {
            parasitismBatDefensPool.Release(gameObject);
        }
        angle += Time.deltaTime * 150;
        transform.position = player_Bloodsucker_Skill_Controller.transform.position;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}