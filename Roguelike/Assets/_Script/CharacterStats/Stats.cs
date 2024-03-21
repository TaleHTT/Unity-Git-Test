using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Stats
{
    [Tooltip("������ֵ")]
    public float baseValue;
    [Tooltip("�ڻ�����ֵ������")]
    public List<float> modfiers;
    public float GetValue()
    {
        float finalvalue = baseValue;
        foreach (float modfier in modfiers)
            finalvalue += modfier;

        return finalvalue;
    }
    public void AddModfiers(float modfier)
    {
        modfiers.Add(modfier);
    }
    public void RemoveModfiers(float modfier)
    {
        modfiers.Remove(modfier);
    }
}
