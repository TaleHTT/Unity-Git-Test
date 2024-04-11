using UnityEngine;
using UnityEngine.Pool;

public class Archer_Skill_Controller : MonoBehaviour
{
    [SerializeField] private GameObject Summon_Hound_Prefab;

    private ObjectPool<GameObject> pool;

    Player_Archer player_Archer;
    private void Awake()
    {
        player_Archer = GetComponent<Player_Archer>();
        pool = new ObjectPool<GameObject>(createFunc, actionOnGet, actionOnRelease, actionOnDestory, true, 10, 1000);
    }
    private void Update()
    {
        if(SkillManger.instance.archer_Skill.coolDownTimer == 0)
        {
            pool.Get();
        }
    }
    private void CreatMultipleArrows()
    {
        //player_Archer.arrowPerfab.GetComponent<Arrow_Controller>().arrowDir
    }
    private GameObject createFunc()
    {
        var orb = Instantiate(Summon_Hound_Prefab, transform.position, Quaternion.identity);
        orb.GetComponent<Orb_Controller>().pool = pool;
        return orb;
    }
    private void actionOnGet(GameObject orb)
    {
        orb.transform.position = transform.position;
        orb.SetActive(true);
    }
    private void actionOnRelease(GameObject orb)
    {
        orb.SetActive(false);
    }
    private void actionOnDestory(GameObject orb)
    {
        Destroy(orb);
    }
}
