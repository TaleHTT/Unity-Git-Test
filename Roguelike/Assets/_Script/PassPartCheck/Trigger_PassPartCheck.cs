using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_PassPartCheck : MonoBehaviour
{
    [Tooltip("¹Ø¿¨Ê±¼ä")]
    public float timer;
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0 || PlayerTeamManager.Instance.currentPlayerNum <= 0)
            EntityEventSystem.instance.Trigger_FailPassPart();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            EntityEventSystem.instance.Trigger_SuccessPassPart();
    }
}
