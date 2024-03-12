using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCheck : MonoBehaviour
{
    public List<GameObject> playerCheck;
    public List<GameObject> enemyCheck;
    public float timer;
    private void Update()
    {
        if(playerCheck.Count == 0 || enemyCheck.Count == 0 || timer < 0)
        {
            GameObject.Find("Canvas").GetComponentInChildren<UI_Fade_Screen>().FadeOut();
        }
        timer -= Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerCheck.Add(collision.gameObject);
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            enemyCheck.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerCheck.Remove(collision.gameObject);
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            enemyCheck.Remove(collision.gameObject);
        }
    }
}
