using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 总体基类
/// </summary>
public enum NegativeEffectType
{
    Bleed,
    Hunt,
    Cold,
    Burning
}
public class Base : MonoBehaviour
{
    public List<int> randomNum = new List<int>();
    public Dictionary<int, NegativeEffectType> negativeEffect = new Dictionary<int, NegativeEffectType>();
    public NegativeEffectType negativeEffectType {  get; set; }
    private float houndAttackTimer = 1;
    public float twoHanedAttackTimer = 1;
    public int hit_Assassin;
    public bool isStealth { get; set; }
    public bool isHunting;
    public float markDurationTimer { get; set; }

    [Header("Destroy info")]
    [Tooltip("死亡后，经过timer秒后销毁")]
    public float deadTimer;

    public float timer_Hound_Bleed { get; set; }
    [Tooltip("攻击范围")]
    public float attackRadius;
    [Tooltip("是否死亡")]
    public bool isDead;
    public bool isHit { get; set; }
    public Animator anim { get; set; }
    public int amountOfHit;
    public CapsuleCollider2D cd { get; set; }

    public CharacterStats stats { get; set; }
    public int layersOfBleeding_Hound {  get; set; }
    public float timer_Two_Handed_Saber_Bleed { get; set; }
    public int layersOfBleeding_Two_Handed_Saber;
    protected virtual void Awake()
    {
        cd = GetComponent<CapsuleCollider2D>();
        stats = GetComponent<CharacterStats>();
        anim = GetComponentInChildren<Animator>();
    }
    protected virtual void Start()
    {
        timer_Hound_Bleed = DataManager.instance.hound_Skill_Data.durationTimer;
        timer_Two_Handed_Saber_Bleed = DataManager.instance.two_Handed_Saber_Skill_Data.skill_1_DurationTimer;
    }
    protected virtual void Update()
    {
        if (isDead == true)
        {
            deadTimer -= Time.deltaTime;
            if(deadTimer < 0)
                gameObject.SetActive(false);
            return;
        }
        else
        {
            deadTimer = 3;
            gameObject.SetActive(true);
        }
        HuntingMark();
        Hound_Bleed();
        Two_Handed_Bleed();     
    }
    public void Two_Handed_Bleed()
    {
        if(layersOfBleeding_Two_Handed_Saber > 0)
        {
            timer_Two_Handed_Saber_Bleed -= Time.deltaTime;
            if (timer_Two_Handed_Saber_Bleed <= 0)
            {
                layersOfBleeding_Two_Handed_Saber = 0;
                negativeEffect.Remove(0);
                if (randomNum.Contains(0) == false)
                    randomNum.Remove(0);
                return;
            }
            else
            {
                twoHanedAttackTimer -= Time.deltaTime;
                if (twoHanedAttackTimer < 0)
                {
                    if (negativeEffect.TryGetValue(0, out NegativeEffectType value))
                    {
                        value = NegativeEffectType.Bleed;
                    }
                    else
                    {
                        if (randomNum.Contains(0) == false)
                            randomNum.Add(0);
                        negativeEffect.Add(0, NegativeEffectType.Bleed);
                    }
                    stats.AuthenticTakeDamage(DataManager.instance.hound_Skill_Data.bleedDamage * layersOfBleeding_Two_Handed_Saber);
                    twoHanedAttackTimer = 1;
                }
            }
        }
    }

    public void Hound_Bleed()
    {
        if (amountOfHit >= 2)
        {
            amountOfHit = 0;
            layersOfBleeding_Hound++;
            timer_Hound_Bleed = DataManager.instance.hound_Skill_Data.durationTimer;
        }
        if (layersOfBleeding_Hound > 0)
        {
            timer_Hound_Bleed -= Time.deltaTime;
            if (timer_Hound_Bleed <= 0)
            {
                layersOfBleeding_Hound = 0;
                negativeEffect.Remove(0);
                if (randomNum.Contains(0) == false)
                    randomNum.Remove(0);
                return;
            }
            else
            {
                houndAttackTimer -= Time.deltaTime;
                if (houndAttackTimer < 0)
                {
                    if (negativeEffect.TryGetValue(0, out NegativeEffectType value))
                    {
                        value = NegativeEffectType.Bleed;
                    }
                    else
                    {
                        if(randomNum.Contains(0) == false)
                            randomNum.Add(0);
                        negativeEffect.Add(0, NegativeEffectType.Bleed);
                    }
                    stats.AuthenticTakeDamage(DataManager.instance.hound_Skill_Data.bleedDamage * layersOfBleeding_Hound);
                    houndAttackTimer = 1;
                }
            }
        }
    }
    public void HuntingMark()
    {
        if (hit_Assassin >= 3)
        {
            hit_Assassin = 0;
            isHunting = true;
            markDurationTimer = DataManager.instance.assassin_Skill_Data.skill_1_durationTimer;
        }
        if(isHunting == true)
        {
            Debug.Log("1");
            if(negativeEffect.TryGetValue(1, out NegativeEffectType value))
            {
                value = NegativeEffectType.Hunt;
            }
            else
            {
                negativeEffect.Add(1, NegativeEffectType.Hunt);
                randomNum.Add(1);
            }
            markDurationTimer -= Time.deltaTime;
            //stats.woundedMultiplier.AddModfiers(stats.woundedMultiplier.GetValue() * DataManager.instance.assassin_Skill_Data.extraAddWoundedMultiplier);
        }
        if(markDurationTimer <= 0)
        {
            //stats.woundedMultiplier.RemoveModfiers(stats.woundedMultiplier.GetValue() * DataManager.instance.assassin_Skill_Data.extraAddWoundedMultiplier);
            isHunting = false;
            negativeEffect.Remove(1);
            if (randomNum.Contains(1) == false)
                randomNum.Remove(1);
        }
    }
    public virtual void DamageEffect()
    {

    }
    public IEnumerator TwoHandedSaberBleedDamage()
    {
        yield return new WaitForSeconds(1);

        stats.AuthenticTakeDamage(DataManager.instance.two_Handed_Saber_Skill_Data.bleedDamage * layersOfBleeding_Two_Handed_Saber);
    }
}
