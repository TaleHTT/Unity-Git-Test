using UnityEngine;
using UnityEngine.UI;

public class Hp_UI : MonoBehaviour
{
    public Slider slider;
    public CharacterStats stats;
    public Summons_Base summons_Base;
    private void Start()
    {
        slider = GetComponentInChildren<Slider>();
        stats = GetComponentInParent<CharacterStats>();
        summons_Base = GetComponentInParent<Summons_Base>();
    }
    private void Update()
    {
        UpdateHpUI();
    }
    public void UpdateHpUI()
    {
        if(summons_Base != null)
        {
            slider.maxValue = summons_Base.maxHp;
        }
        else
        {
            slider.maxValue = stats.maxHp.GetValue();
        }
        if(summons_Base != null)
        {
            slider.value = summons_Base.currentHp;
        }
        else
        {
            slider.value = stats.currentHealth;
        }
    }

}
