using UnityEngine;
using UnityEngine.Pool;

public class BirdTotem_Controller : MonoBehaviour
{
    private float timer;
    public float damage {  get; set; }
    public float Hp {  get; set; }
    public ObjectPool<GameObject> birdTotemPool {  get; set; }
    private void OnEnable()
    {
        timer = DataManager.instance.shaman_Skill_Data.skill_2_duraiton;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 || Hp <= 0)
            birdTotemPool.Release(gameObject);
    }
    private void OnDisable()
    {
        if (SkillManger.instance.shaman_Skill.isHave_X_Equipment == true)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.shaman_Skill_Data.skill_2_radius);
            foreach (var hit in colliders)
            {
                if (hit.GetComponent<EnemyBase>() != null)
                    hit.GetComponent<EnemyStats>().TakeDamage(damage);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<PlayerBase>().stats.moveSpeed.AddModfiers(collision.GetComponent<PlayerBase>().stats.moveSpeed.GetValue() * DataManager.instance.shaman_Skill_Data.extraAddMoveSpeed);
        collision.GetComponent<PlayerBase>().stats.attackSpeed.AddModfiers(collision.GetComponent<PlayerBase>().stats.attackSpeed.GetValue() * DataManager.instance.shaman_Skill_Data.extraAddAttackSpeed);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<PlayerBase>().stats.moveSpeed.RemoveModfiers(collision.GetComponent<PlayerBase>().stats.moveSpeed.GetValue() * DataManager.instance.shaman_Skill_Data.extraAddMoveSpeed);
        collision.GetComponent<PlayerBase>().stats.attackSpeed.RemoveModfiers(collision.GetComponent<PlayerBase>().stats.attackSpeed.GetValue() * DataManager.instance.shaman_Skill_Data.extraAddAttackSpeed);
    }
}