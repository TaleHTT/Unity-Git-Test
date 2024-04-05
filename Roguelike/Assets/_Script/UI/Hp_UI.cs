using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp_UI : MonoBehaviour
{
    public Slider slider;
    public CharacterStats stats;
    private void Start()
    {
        slider = GetComponentInChildren<Slider>();
        stats = GetComponentInParent<CharacterStats>();
    }
    private void Update()
    {
        UpdateHpUI();
    }
    public void UpdateHpUI()
    {
        slider.maxValue = stats.maxHp.GetValue();
        slider.value = stats.currentHealth;
    }

}
