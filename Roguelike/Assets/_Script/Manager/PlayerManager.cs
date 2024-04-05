using UnityEngine;
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
}
