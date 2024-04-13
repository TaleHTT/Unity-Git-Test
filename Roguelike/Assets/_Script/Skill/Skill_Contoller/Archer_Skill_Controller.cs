using UnityEngine;
using UnityEngine.Pool;

public class Archer_Skill_Controller : MonoBehaviour
{
    [SerializeField] private GameObject Summon_Hound_Prefab;
    private ObjectPool<GameObject> houndPool;

    public bool isHave_X_Equipment;

    Player_Archer player_Archer;
    private void Awake()
    {
        player_Archer = GetComponent<Player_Archer>();
        houndPool = new ObjectPool<GameObject>(CreateHoundFunc, ActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
    }
    private void Update()
    {
        //if (SkillManger.instance.archer_Skill.coolDownTimer == 0)
        //{
        //    houndPool.Get();
        //}
    }
    private GameObject CreateHoundFunc()
    {
        var hound = Instantiate(Summon_Hound_Prefab, transform.position, Quaternion.identity);
        hound.GetComponent<Summons_Base>().houndPool = houndPool;
        return hound;
    }
    private void ActionOnGet(GameObject objects)
    {
        objects.GetComponent<Summons_Base>().isDead = false;
        objects.transform.position = transform.position;
        objects.SetActive(true);
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
