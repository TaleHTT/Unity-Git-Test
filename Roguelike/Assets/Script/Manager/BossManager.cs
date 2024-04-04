using UnityEngine;
public class BossManager : MonoBehaviour
{
    public static BossManager instance;
    public int bossCount;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }
}
