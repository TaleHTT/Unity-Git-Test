﻿using UnityEngine;
[CreateAssetMenu(fileName = "IceCaster_Skill", menuName = "Skill/IceCaster_Skill")]
public class IceCaster_Skill_Data : ScriptableObject
{
    public float skill_1_DamageBaseValue;
    public float skill_2_DamageBaseValue;
    public float skill_X_DamageBaseValue;
    [Range(0, 10)] public float skill_1_ExtraAddDamage;
    [Range(0, 10)] public float skill_2_ExtraAddDamage;
    [Range(0, 10)] public float skill_X_ExtraAddDamage;
    [Range(0, 10)] public float RemoveMoveSpeed;
    public float skill_X_Duration;
    public float skill_1_durationTimer;
    public float skill_1_radius;
    public float skill_2_radius;
    public float skill_X_radius;
    public float skill_1_CD;
    public float skill_2_CD;
    public float skill_X_CD;
    public float skill_1_timer;
    public float skill_X_timer;
}