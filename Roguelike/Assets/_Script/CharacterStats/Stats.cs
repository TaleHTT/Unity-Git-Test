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
        float finalValue = baseValue;
        foreach (float modfier in modfiers)
            finalValue += modfier;

        return finalValue;
    }
    public float AddArmor(float percentage)
    {
        float finaArmor = GetValue();
        finaArmor = (1 + percentage) * finaArmor;
        return finaArmor;
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
