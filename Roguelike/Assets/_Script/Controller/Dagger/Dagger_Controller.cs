using UnityEngine;
using UnityEngine.Pool;

public class Dagger_Controller : MonoBehaviour
{
    public ObjectPool<GameObject> daggerPool {  get; set; }
    public float damage {  get; set; }
    public float treat {  get; set; }
    public float moveSpeed;
    public Vector2 moveDir {  get; set; }
    public GameObject target;
    public Assassin_Skill_Controller assassin_Skill_Controller { get; set; }
    private void OnEnable()
    {
        target = null;
    }
    private void Update()
    {
        transform.Translate(moveDir * moveSpeed * Time.deltaTime);
    }
    private void OnDisable()
    {
        if (target.GetComponent<EnemyBase>().isDead)
        {
            if (target.GetComponent<EnemyBase>().isHunting)
                assassin_Skill_Controller.player_Assassin.stateMachine.ChangeState(assassin_Skill_Controller.player_Assassin.assassinStealthIdleState);
            assassin_Skill_Controller.player_Assassin.GetComponent<EnemyStats>().TakeTreat(treat);
            assassin_Skill_Controller.num_KillEnemy++;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        target = collision.gameObject;
        if(target.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (assassin_Skill_Controller.player_Assassin.isStrengthen)
                target.GetComponent<EnemyStats>().TakeDamage(damage * 3);
            else
                target.GetComponent<EnemyStats>().TakeDamage(damage);
            target.GetComponent<EnemyBase>().isHunting = true;
            target.GetComponent<EnemyBase>().markDurationTimer = DataManager.instance.assassin_Skill_Data.skill_3_durationTimer;
        }
    }
}