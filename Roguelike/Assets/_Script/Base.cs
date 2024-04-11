using UnityEngine;

/// <summary>
/// 总体基类
/// </summary>
public class Base : MonoBehaviour
{
    public float detectTimer;

    public CharacterStats stats;
    public Animator anim { get; private set; }
    public CapsuleCollider2D cd { get; private set; }
    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        cd = GetComponent<CapsuleCollider2D>();
        stats = GetComponent<CharacterStats>();
        anim = GetComponentInChildren<Animator>();
    }
    protected virtual void Update()
    {

    }
    public virtual void DamageEffect()
    {

    }
}
