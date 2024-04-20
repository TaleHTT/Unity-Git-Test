using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 总体基类
/// </summary>
public class Base : MonoBehaviour
{
    public int hit_Assassin;
    public bool isStealth { get; set; }
    public bool isHunting {  get; set; }
    public float markDurationTimer {  get; set; }

    [Header("Destroy info")]
    [Tooltip("死亡后，经过timer秒后销毁")]
    public float deadTimer;

    private float timer_Hound;
    [Tooltip("攻击范围")]
    public float attackRadius;
    [Tooltip("是否死亡")]
    public bool isDead { get; set; }
    public bool isHit {  get; set; }
    public Animator anim { get; set; }
    public int amountOfHit { get; set; }
    public CapsuleCollider2D cd { get; set; }

    public CharacterStats stats {  get; set; }
    public int layersOfBleeding_Hound {  get; set; }
    public float timer_Two_Handed_Saber {  get; set; }
    public int layersOfBleeding_Two_Handed_Saber {  get; set; }
    protected virtual void Awake()
    {
        timer_Hound = DataManager.instance.hound_Skill_Data.durationTimer;
        timer_Two_Handed_Saber = DataManager.instance.two_Handed_Saber_Skill_Data.skill_1_DurationTimer;
    }
    protected virtual void Start()
    {
        cd = GetComponent<CapsuleCollider2D>();
        stats = GetComponent<CharacterStats>();
        anim = GetComponentInChildren<Animator>();
    }
    protected virtual void Update()
    {
        if (isDead)
        {
            StartCoroutine(DeadDestroy(deadTimer));
            return;
        }
        else
        {
            gameObject.SetActive(true);
        }
        HuntingMark();
        Hound_Bleed();
        Two_Handed_Bleed();
    }

    private void Two_Handed_Bleed()
    {
        if(layersOfBleeding_Two_Handed_Saber > 0)
        {
            if(timer_Two_Handed_Saber < 0)
            {
                layersOfBleeding_Two_Handed_Saber = 0;
                return;
            }
            else
            {
                StartCoroutine(TwoHandedSaberBleedDamage());
            }
        }
    }

    private void Hound_Bleed()
    {
        if (amountOfHit == 2)
        {
            amountOfHit = 0;
            layersOfBleeding_Hound++;
            timer_Hound = DataManager.instance.hound_Skill_Data.durationTimer;
        }
        if (layersOfBleeding_Hound > 0)
        {
            timer_Hound -= Time.deltaTime;
            if (timer_Hound < 0)
            {
                layersOfBleeding_Hound = 0;
                return;
            }
            else
            {
                StartCoroutine(HoundBleedDamage());
            }
        }
    }
    public void HuntingMark()
    {
        if(hit_Assassin == 3)
        {
            hit_Assassin = 0;
            isHunting = true;
            markDurationTimer = DataManager.instance.assassin_Skill_Data.skill_1_durationTimer;
        }
        if(isHunting == true)
        {
            stats.woundedMultiplier.AddModfiers(stats.woundedMultiplier.GetValue() * DataManager.instance.assassin_Skill_Data.extraAddWoundedMultiplier);
            markDurationTimer -= Time.deltaTime;
        }
        else if(markDurationTimer <= 0)
        {
            stats.woundedMultiplier.RemoveModfiers(stats.woundedMultiplier.GetValue() * DataManager.instance.assassin_Skill_Data.extraAddWoundedMultiplier);
            isHunting = false;
        }
    }
    public virtual void DamageEffect()
    {

    }
    public IEnumerator DeadDestroy(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }
    public IEnumerator HoundBleedDamage()
    {
        yield return new WaitForSeconds(1);

        stats.AuthenticTakeDamage(DataManager.instance.hound_Skill_Data.bleedDamage * layersOfBleeding_Hound);
    }
    public IEnumerator TwoHandedSaberBleedDamage()
    {
        yield return new WaitForSeconds(1);

        stats.AuthenticTakeDamage(DataManager.instance.two_Handed_Saber_Skill_Data.bleedDamage * layersOfBleeding_Two_Handed_Saber);
    }
}
