using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Assassin_Skill_Controller : MonoBehaviour
{
    public int num_KillEnemy {  get; set; }
    private ObjectPool<GameObject> daggerPool;
    public GameObject daggerPrefab;
    List<GameObject> huntTarget;
    Player_Assassin player_Assassin;
    private int bulletNum;
    public int BulletNum
    {
        get
        {
            return bulletNum;
        }
        set
        {
            bulletNum = 8 * player_Assassin.stats.level;
            if(bulletNum >= 24)
            {
                bulletNum = 24;
            }
        }
    }
    private int angle;
    public float Angle
    {
        get
        {
            return angle;
        }
        set
        {
            angle = 360 / BulletNum;
        }
    }

    private void Awake()
    {
        player_Assassin = GetComponent<Player_Assassin>();
        daggerPool = new ObjectPool<GameObject>(CreateDaggerFunc, ActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
    }
    private void Update()
    {
        if(SkillManger.instance.assassin_Skill.isHave_X_Equipment)
            if (num_KillEnemy == 3)
                for(float i = 0; i < 360 ; i += Angle)
                    daggerPool.Get();

        if (huntTarget.Count > 0)
            player_Assassin.stats.attackSpeed.AddModfiers(player_Assassin.stats.attackSpeed.GetValue() * DataManager.instance.assassin_Skill_Data.extraAddAttackSpeed);
        else
            player_Assassin.stats.attackSpeed.RemoveModfiers(player_Assassin.stats.attackSpeed.GetValue() * DataManager.instance.assassin_Skill_Data.extraAddAttackSpeed);
    }
    public void FlushedMark()
    {
        for (int i = 0; i < huntTarget.Count; i++)
        {
            if (huntTarget[i].GetComponent<EnemyBase>().isDead)
            {
                player_Assassin.stateMachine.ChangeState(player_Assassin.assassinStealthIdleState);
            }
        }
    }
    public void HuntingDectect()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Mathf.Infinity);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyBase>() != null)
            {
                if (hit.GetComponent<EnemyBase>().isHunting == true)
                    huntTarget.Add(hit.gameObject);
            }
        }
    }
    private GameObject CreateDaggerFunc()
    {
        for (float i = 0f; i < 360; i += Angle)
        {
            float x = 1 * Mathf.Cos(i * Mathf.Deg2Rad);
            float y = 1 * Mathf.Sin(i * Mathf.Deg2Rad);

            var objects = Instantiate(daggerPrefab, transform.position, Quaternion.identity);
            objects.transform.position = new Vector2(x + transform.position.x, y + transform.position.y);
            objects.GetComponent<Dagger_Controller>().moveDir = objects.transform.position - transform.position;
            objects.GetComponent<Dagger_Controller>().daggerPool = daggerPool;
            objects.GetComponent<Dagger_Controller>().damage = player_Assassin.stats.damage.GetValue() * (1 + DataManager.instance.assassin_Skill_Data.extraAddDamage);
            return objects;
        }
        return null;
    }
    private void ActionOnGet(GameObject objects)
    {
        for(float i = 0f; i < 360; i += Angle)
        {
            float x = 1 * Mathf.Cos(i * Mathf.Deg2Rad);
            float y = 1 * Mathf.Sin(i * Mathf.Deg2Rad);

            objects.transform.position = new Vector3(x + transform.position.x, y + transform.position.y);
            objects.SetActive(true);
        }
    }
    private void ActionOnRelease(GameObject objects)
    {
        objects.SetActive(false);
    }
    private void ActionOnDestory(GameObject objects)
    {
        Destroy(objects);
    }
}