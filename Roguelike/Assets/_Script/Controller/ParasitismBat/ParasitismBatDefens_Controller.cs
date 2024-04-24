using UnityEngine;
using UnityEngine.Pool;

public class ParasitismBatDefens_Controller : MonoBehaviour
{
    public ObjectPool<GameObject> parasitismBatDefensPool {  get; set; }
    public Bloodsucker_Skill_Controller bloodsucker_Skill_Controller { get; set; }
    float angle;
    private void Update()
    {
        if(bloodsucker_Skill_Controller.duration <= 0 || bloodsucker_Skill_Controller.player_Bloodsucker.stats.defensNum <= 0)
        {
            parasitismBatDefensPool.Release(gameObject);
        }
        angle += Time.deltaTime * 150;
        transform.position = bloodsucker_Skill_Controller.transform.position;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}