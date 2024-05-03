using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ParasitismBatAttack_Controller : MonoBehaviour
{
    public float moveSpeed;
    public ObjectPool<GameObject> parasitismBatPool;
    
    [HideInInspector] public List<GameObject> attackDetect;
    [HideInInspector] public GameObject cloestTarget;
    [HideInInspector] public bool isBack;
    protected virtual void OnEnable()
    {
        isBack = false;
    }
    protected virtual void Update()
    {

    }
}