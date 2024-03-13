using UnityEngine;

public enum EquipmentType
{
    ChangeValue,
    AddSkill
}
[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/equipment")]
public class ItemData : ScriptableObject
{
    public EquipmentType equipmentType;
    public string itemName;
    public Sprite icon;
}