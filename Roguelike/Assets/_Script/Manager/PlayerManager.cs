using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public int playerCount;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Update()
    {
        playerCount = TeamWheel.Instance.numOfCharacterInTeam;
    }


}
