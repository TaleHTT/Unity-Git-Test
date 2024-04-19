using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager instance;
    public Player_Orb_Controller player_Orb_Controller;
    public Enemy_Orb_Controller enemy_Orb_Controller;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }
}