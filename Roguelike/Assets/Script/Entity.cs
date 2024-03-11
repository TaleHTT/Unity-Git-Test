using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 总体基类
/// </summary>
public class Entity : MonoBehaviour
{
    [Header("Attack info")]
    public LayerMask whatIsEnemy;

    public float detectTimer;
    public Rigidbody2D rb;
    public Animator anim;
    public CharacterStats stats;
    public CapsuleCollider2D cd;
    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
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
}
