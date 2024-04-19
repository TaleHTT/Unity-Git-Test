using UnityEngine;
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public int enemyCount;
    public Enemy_Saber enemy_Saber {  get; set; }
    public Enemy_Archer enemy_Archer { get; set; }
    public Enemy_Caster enemy_Caster { get; set; }
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        enemy_Saber = GetComponent<Enemy_Saber>();
        enemy_Archer = GetComponent<Enemy_Archer>();
        enemy_Caster = GetComponent<Enemy_Caster>();
    }
}
