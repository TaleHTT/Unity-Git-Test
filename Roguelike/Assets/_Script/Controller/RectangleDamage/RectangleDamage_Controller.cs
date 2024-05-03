using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class RectangleDamage_Controller : MonoBehaviour
{
    public ObjectPool<GameObject> rectanglePool;
    
    [HideInInspector] public float timer;
    [HideInInspector] public float damage;
    [HideInInspector] public int currentNumOfAttack;
    [HideInInspector] public const int maxAttackNum = 3;
    [HideInInspector] public List<GameObject> attackTargets;
    protected virtual void OnEnable()
    {
        timer = 1;
        currentNumOfAttack = 0;
    }
    protected virtual void Awake()
    {
        attackTargets = new List<GameObject>();
    }
    protected virtual void Start()
    {
        transform.localScale = new Vector2(DataManager.instance.bloodsucker_Skill_Data.length, DataManager.instance.bloodsucker_Skill_Data.width);
    }
    protected virtual void Update()
    {

    }
}