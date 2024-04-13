using System.Collections;
using UnityEngine;

/// <summary>
/// 总体基类
/// </summary>
public class Base : MonoBehaviour
{
    private float timer;

    public int amountOfHit;

    public int layersOfBleeding;

    [Tooltip("是否受击")]
    public bool isHit;

    public float detectTimer;

    public CharacterStats stats;
    public Animator anim { get; private set; }
    public CapsuleCollider2D cd { get; private set; }
    protected virtual void Awake()
    {
        timer = SkillManger.instance.archer_Skill.persistentTimer;
    }
    protected virtual void Start()
    {
        cd = GetComponent<CapsuleCollider2D>();
        stats = GetComponent<CharacterStats>();
        anim = GetComponentInChildren<Animator>();
    }
    protected virtual void Update()
    {
        isHit = false;
        if (amountOfHit == 2)
        {
            amountOfHit = 0;
            layersOfBleeding++;
            SkillManger.instance.archer_Skill.persistentTimer = timer;
        }
        if(layersOfBleeding > 0)
        {
                SkillManger.instance.archer_Skill.persistentTimer -= Time.deltaTime;
            if (SkillManger.instance.archer_Skill.persistentTimer < 0)
            {
                layersOfBleeding = 0;
                return;
            }
            else
            {
                StartCoroutine(BleedingDamage());
            }
        }
    }
    public virtual void DamageEffect()
    {

    }
    public IEnumerator BleedingDamage()
    {
        yield return new WaitForSeconds(1);

        stats.AuthenticTakeDamage(stats.bleedingDamage * layersOfBleeding);
    }
}
