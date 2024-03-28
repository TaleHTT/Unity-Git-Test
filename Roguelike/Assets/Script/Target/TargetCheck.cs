using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCheck : MonoBehaviour
{
    public static TargetCheck instance;
    public List<GameObject> playerCheck;
    public List<GameObject> bossCheck;
    public bool isPass;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }
    private void Update()
    {
        if(playerCheck.Count <= 0)
        {
            isPass = false;
        }
        if(bossCheck.Count <= 0)
        {
            isPass = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            playerCheck.Add(collision.gameObject);
        if(collision.gameObject.layer == LayerMask.NameToLayer("Boss"))
            bossCheck.Add(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            playerCheck.Remove(collision.gameObject);
        if(collision.gameObject.layer == LayerMask.NameToLayer("Boss"))
            bossCheck.Remove(collision.gameObject);
    }
}
