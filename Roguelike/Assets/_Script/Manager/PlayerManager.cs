using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public Player_Saber player_Saber {  get; set; }
    public Player_Caster player_Caster { get; set; }
    public Player_Priest player_Priest { get; set; }
    public Player_Archer player_Archer { get; set; }
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        player_Saber = GetComponent<Player_Saber>();
        player_Archer = GetComponent<Player_Archer>();
        player_Caster = GetComponent<Player_Caster>();
        player_Priest = GetComponent<Player_Priest>();
    }
}
