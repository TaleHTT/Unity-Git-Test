using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CoinSettingScriptableObject", order = 1)]
public class CoinSetting : ScriptableObject
{
    public int SumOfLevels;

    [Serializable]
    public struct CoinAssignWaySection
    {
        public int lowerLevel;
        public int upperLevel;
        public int normalValue;
        public int expertValue;
    }

    public List<CoinAssignWaySection> coinAssignWaySections;

    /// <summary>
    /// 对战斗关卡获取值
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="isNormal"></param>
    public int GetValue(int level, bool isNormal)
    {
        CoinAssignWaySection coinAssignWay = new CoinAssignWaySection();
        foreach (var setting in coinAssignWaySections)
        {
            if(setting.lowerLevel <= level && setting.upperLevel >= level)
            {
                coinAssignWay = setting;
                break;
            }
        }
        if(isNormal)
        {
            return coinAssignWay.normalValue;
        }
        else
        {
            return coinAssignWay.expertValue;
        }
    }
}


