using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summons_Base : Base
{
    public Transform cloestTarget;
    public Seeker seeker { get; set; }
    public float chaseRadius { get; set; }
    public float attackRadius {  get; set; }
    public bool drawTheBorderOrNot { get; set; }
    public List<GameObject> attackDetects { get; set; }

    protected override void Awake()
    {
        seeker = GetComponent<Seeker>();
    }
    protected override void Start()
    {

    }
    protected override void Update()
    {

    }
    public void OnDrawGizmos()
    {
        if (!drawTheBorderOrNot)
            return;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
    public IEnumerator DeadDestroy(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}
