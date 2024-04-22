using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BirdTotem_Controller : MonoBehaviour
{
    private float timer;
    public float damage {  get; set; }
    public float Hp {  get; set; }
    public float moveSpeedAdd { get; set; }
    public float attackSpeedAdd { get; set; }
    public List<GameObject> playerDetect {  get; set; }
    public Dictionary<GameObject, float> moveSpeed = new Dictionary<GameObject, float>();
    public Dictionary<GameObject, float> attackSpeed = new Dictionary<GameObject, float>();
    public ObjectPool<GameObject> birdTotemPool {  get; set; }
    private void OnEnable()
    {
        timer = DataManager.instance.shaman_Skill_Data.skill_2_duraiton;
    }
    private void Awake()
    {
        PlayerDetect();
    }
    private void Start()
    {
 
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 || Hp <= 0)
            birdTotemPool.Release(gameObject);
        AddMoveSpeed();
        AddAttackSpeed();
    }

    private void AddAttackSpeed()
    {
        for (int i = 0; i < playerDetect.Count; i++)
        {
            PlayerStats player = playerDetect[i].GetComponent<PlayerStats>();
            if (Vector2.Distance(transform.position, playerDetect[i].transform.position) <= DataManager.instance.shaman_Skill_Data.skill_2_radius)
            {
                if (attackSpeed.TryGetValue(playerDetect[i], out float value))
                {
                    continue;
                }
                else
                {
                    float baseValue = player.attackSpeed.GetValue();
                    attackSpeed.Add(playerDetect[i], player.attackSpeed.GetValue());
                    player.attackSpeed.AddModfiers(baseValue * DataManager.instance.shaman_Skill_Data.extraAddAttackSpeed);
                }
            }
            else
            {
                if (attackSpeed.TryGetValue(playerDetect[i], out float value))
                {
                    player.attackSpeed.RemoveModfiers(value * DataManager.instance.shaman_Skill_Data.extraAddAttackSpeed);
                }
            }
        }
    }

    private void AddMoveSpeed()
    {
        for (int i = 0; i < playerDetect.Count; i++)
        {
            PlayerStats player = playerDetect[i].GetComponent<PlayerStats>();
            if (Vector2.Distance(transform.position, playerDetect[i].transform.position) <= DataManager.instance.shaman_Skill_Data.skill_2_radius)
            {
                if (moveSpeed.TryGetValue(playerDetect[i], out float value))
                {
                    continue;
                }
                else
                {
                    float baseValue = player.moveSpeed.GetValue();
                    moveSpeed.Add(playerDetect[i], player.moveSpeed.GetValue());
                    player.moveSpeed.AddModfiers(baseValue * DataManager.instance.shaman_Skill_Data.extraAddMoveSpeed);
                }
            }
            else
            {
                if (moveSpeed.TryGetValue(playerDetect[i], out float value))
                {
                    player.moveSpeed.RemoveModfiers(value * DataManager.instance.shaman_Skill_Data.extraAddMoveSpeed);
                }
            }
        }
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
    public void PlayerDetect()
    {
        playerDetect = new List<GameObject>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.shaman_Skill_Data.skill_2_radius);
        foreach(var hit in colliders)
        {
            if (hit.GetComponent<PlayerBase>() != null)
                playerDetect.Add(hit.gameObject);
        }
    }
}