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
}


