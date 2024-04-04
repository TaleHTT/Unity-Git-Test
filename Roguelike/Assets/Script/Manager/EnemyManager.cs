using UnityEngine;
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public int enemyCount;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }
}
