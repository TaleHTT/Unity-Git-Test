using UnityEngine;
using UnityEngine.Pool;

public class ParasitismBatDefens_Controller : MonoBehaviour
{
    public ObjectPool<GameObject> parasitismBatDefensPool {  get; set; }
    public Bloodsucker_Skill_Controller bloodsucker_Skill_Controller { get; set; }
    float angle;
    float x;
    float y;
    private void OnEnable()
    {
        if (Vector2.Distance(transform.position, bloodsucker_Skill_Controller.transform.position) > 0)
        {
            angle = 0;
        }
        else if(Vector2.Distance(transform.position, bloodsucker_Skill_Controller.transform.position) < 0)
        {
            angle = 180;
        }
    }
    private void Update()
    {
        if (bloodsucker_Skill_Controller.currentDefenNum <= 0 || bloodsucker_Skill_Controller.duration < 0)
            parasitismBatDefensPool.Release(gameObject);
        angle += Time.deltaTime;
        x = bloodsucker_Skill_Controller.transform.position.x + Mathf.Cos(angle * Mathf.Deg2Rad) * 2f;
        y = transform.position.y + Mathf.Sin(angle * Mathf.Deg2Rad) * 2f;

        transform.position = new Vector2(x, y);
    }
}