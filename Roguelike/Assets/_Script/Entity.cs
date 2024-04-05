using Pathfinding;
using UnityEngine;

/// <summary>
/// 总体基类
/// </summary>
public class Entity : MonoBehaviour, IDamageEffectable
{
    [Header("Attack info")]
    public LayerMask whatIsEnemy;

    public float detectTimer;
    public CharacterStats stats;
    public Seeker seeker { get; private set; }
    public Animator anim { get; private set; }
    public CapsuleCollider2D cd { get; private set; }
    public Rigidbody2D rb { get; private set; }
    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<CharacterStats>();
        anim = GetComponentInChildren<Animator>();
        cd = GetComponent<CapsuleCollider2D>();
    }
    protected virtual void Update()
    {

    }
    public void SetVelocity(float x, float y)
    {
        rb.velocity = new Vector2(x, y);
    }

    public virtual void DamageEffect()
    {
        
    }
}
