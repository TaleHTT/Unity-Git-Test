using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPosition : MonoBehaviour
{
    public float timer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(FinishTarget(timer));
        }
    }
    IEnumerator FinishTarget(float timer)
    {
        yield return new WaitForSeconds(timer);
        GameObject.Find("Canvas").GetComponentInChildren<UI_Fade_Screen>().FadeOut();
    }
}
