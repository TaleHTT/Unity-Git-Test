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
    [Tooltip("攻击范围")]
    public float attackRadius;

    [Header("Destroy info")]
    [Tooltip("死亡后，经过timer秒后销毁")]
    public float deadTimer;
    [Tooltip("是否死亡")]
    public bool isDead;
    
    public int layerOfCold;
    public bool isFreeze;
    public bool isHitInFreeze;
    
    public bool isHunting;
    private bool isGet = false;
    
    private float value;
    private float coldTimer;
    private float animSpeed;
    private GameObject iceEffect;
    private GameObject huntEffect;
    private float defauatMoveSpeed;
    private float defauatAttaclSpeed;
    private float houndAttackTimer = 1;
    private float twoHanedAttackTimer = 1;

    [HideInInspector] public bool isHit;
    [HideInInspector] public bool isStealth;
    [HideInInspector] public int amountOfHit;
    [HideInInspector] public float timer_Cold;
    [HideInInspector] public int hit_Assassin;
    [HideInInspector] public float timer_Hound_Bleed;
    [HideInInspector] public float markDurationTimer;
    [HideInInspector] public int layersOfBleeding_Hound;
    [HideInInspector] public float timer_Two_Handed_Saber_Bleed;
    [HideInInspector] public int layersOfBleeding_Two_Handed_Saber;
    [HideInInspector] public NegativeEffectType negativeEffectType;
    [HideInInspector] public List<int> randomNum = new List<int>();
    public Dictionary<int, NegativeEffectType> negativeEffect = new Dictionary<int, NegativeEffectType>();

    public Animator anim { get; set; }
    public CapsuleCollider2D cd { get; set; }
    public CharacterStats stats { get; set; }
    protected virtual void Awake()
    {
        timer_Cold = 3;
        coldTimer = 2;
        cd = GetComponent<CapsuleCollider2D>();
        stats = GetComponent<CharacterStats>();
        anim = GetComponentInChildren<Animator>();
        defauatMoveSpeed = stats.moveSpeed.GetValue();
        defauatAttaclSpeed = stats.attackSpeed.GetValue();
    }
    protected virtual void Start()
    {
        iceEffect = GameObjectManager.Instance.iceEffect;
        huntEffect = GameObjectManager.Instance.huntEffect;
        value = stats.woundedMultiplier.GetValue();
        timer_Hound_Bleed = DataManager.instance.hound_Skill_Data.durationTimer;
        timer_Two_Handed_Saber_Bleed = DataManager.instance.two_Handed_Saber_Skill_Data.skill_1_DurationTimer;
    }
    protected virtual void Update()
    {
        if (isDead == true)
        {
            deadTimer -= Time.deltaTime;
            if (deadTimer < 0)
                gameObject.SetActive(false);
            return;
        }
        else
        {
            deadTimer = 3;
            gameObject.SetActive(true);
        }
        if (isFreeze)
        {
            Instantiate(iceEffect, transform.position, Quaternion.identity);
        }
        else
        {
            Destroy(iceEffect);
        }
        if (isHunting)
        {
            Instantiate(huntEffect, new Vector2(transform.position.x - 0.6f, transform.position.y + 1.5f), Quaternion.identity, this.transform);
        }
        ColdEffect();
        HuntingMark();
        Hound_Bleed();
        Two_Handed_Bleed();
    }
    public void Two_Handed_Bleed()
    {
        if (layersOfBleeding_Two_Handed_Saber > 0)
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
                        if (randomNum.Contains(0) == false)
                            randomNum.Add(0);
                        negativeEffect.Add(0, NegativeEffectType.Bleed);
                    }
                    stats.AuthenticTakeDamage(DataManager.instance.hound_Skill_Data.bleedDamage * layersOfBleeding_Hound);
                    houndAttackTimer = 1;
                }
            }
        }
    }
    public void ColdEffect()
    {
        if (layerOfCold >= 1)
        {
            timer_Cold -= Time.deltaTime;
            float removeMoveSpeed = defauatMoveSpeed * 0.25f * layerOfCold;
            float removeAttackSpeed = defauatAttaclSpeed * 0.25f * layerOfCold;
            if (negativeEffect.TryGetValue(0, out NegativeEffectType value))
            {
                value = NegativeEffectType.Cold;
            }
            else
            {
                if (randomNum.Contains(2) == false)
                    randomNum.Add(2);
                negativeEffect.Add(2, NegativeEffectType.Cold);
            }
            if (layerOfCold >= 4)
            {
                isFreeze = true;
                if (isFreeze)
                {
                    coldTimer -= Time.deltaTime;
                    GetAnimSpeed(isGet);
                    isGet = true;
                    Freeze(isFreeze);
                    if (coldTimer > 0)
                    {
                        if (isHitInFreeze == true)
                        {
                            layerOfCold = 0;
                            isFreeze = false;
                            Freeze(isFreeze);
                            stats.moveSpeed.baseValue += removeMoveSpeed;
                            stats.attackSpeed.baseValue += removeAttackSpeed;
                            timer_Cold = 3;
                            coldTimer = 2;
                            return;
                        }
                    }
                    else
                    {
                        layerOfCold = 0;
                        isFreeze = false;
                        Freeze(isFreeze);
                        stats.moveSpeed.baseValue += removeMoveSpeed;
                        stats.attackSpeed.baseValue += removeAttackSpeed;
                        timer_Cold = 3;
                        coldTimer = 2;
                        negativeEffect.Remove(0);
                        if (randomNum.Contains(0) == false)
                            randomNum.Remove(0);
                    }
                }
            };
            if (timer_Cold < 0 && isFreeze == false)
            {
                negativeEffect.Remove(0);
                if (randomNum.Contains(0) == false)
                    randomNum.Remove(0);
                stats.moveSpeed.baseValue += removeMoveSpeed;
                stats.attackSpeed.baseValue += removeAttackSpeed;
                timer_Cold = 3;
            }
            stats.moveSpeed.baseValue = defauatMoveSpeed - removeMoveSpeed;
            stats.attackSpeed.baseValue = defauatAttaclSpeed - removeAttackSpeed;
        }
    }
    public void GetAnimSpeed(bool isGet)
    {
        if (isGet)
        {
            return;
        }
        else
        {
            isGet = true;
            animSpeed = anim.speed;
        }
    }
    public void Freeze(bool isFreeze)
    {
        if (isFreeze == true)
        {
            Transform freezeTransform = transform;
            //gameObject.GetComponent<Skill_Controller>().enabled = false;
            transform.position = freezeTransform.position;
            anim.speed = 0;
        }
        else
        {
            //gameObject.GetComponent<Skill_Controller>().enabled = true;
            anim.speed = animSpeed;
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
        if (isHunting == true)
        {
            markDurationTimer -= Time.deltaTime;
            if (negativeEffect.TryGetValue(1, out NegativeEffectType value))
            {
                value = NegativeEffectType.Hunt;
            }
            else
            {
                negativeEffect.Add(1, NegativeEffectType.Hunt);
                randomNum.Add(1);
            }
            stats.woundedMultiplier.baseValue = this.value * (1 + DataManager.instance.assassin_Skill_Data.extraAddWoundedMultiplier);
        }
        if (markDurationTimer <= 0)
        {
            stats.woundedMultiplier.baseValue = this.value;
            isHunting = false;
            negativeEffect.Remove(1);
            if (randomNum.Contains(1) == false)
                randomNum.Remove(1);
        }
    }
    public virtual void DamageEffect()
    {

    }
}
