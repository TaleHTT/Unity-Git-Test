using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Priest_Skill_Controller : MonoBehaviour
{
    public float amountOfHealing;
    public float treatRadius;
    public GameObject treatTarget;
    private List<GameObject> playerDetect;
    public LayerMask whatIsPlayer;
    private const int numberOfRespawns = 1;
    public int numberOfTreatments;

    private void Update()
    {
        PlayerDetect();
        TreatTarget();
    }
    public void RespawnsPlayer()
    {

    }
    public void PlayerDetect()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, treatRadius, whatIsPlayer);
        foreach(var player in colliders)
        {
            if(player.GetComponent<PlayerBase>() != null)
            {
                playerDetect.Add(player.gameObject);
            }
        }
    }
    public void TreatTarget()
    {
        float hp = Mathf.Infinity;
        for(int i = 0; i < playerDetect.Count; i++)
        {
            if(hp > playerDetect[i].GetComponent<PlayerStats>().currentHealth)
            {
                hp = playerDetect[i].GetComponent <PlayerStats>().currentHealth;
                treatTarget = playerDetect[i];
            }
        }
    }
}