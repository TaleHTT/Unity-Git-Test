using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Controller of all the enemy spawn logic in a level
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    /// <summary>
    /// all the game is run with lots of wave, this class is to accomadate all the wave
    /// </summary>
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        /// <summary>
        /// List of enemies for this wave
        /// </summary>
        public List<EnemyGroup> enemyGroups;
        /// <summary>
        /// total number of enemies to spawn in this wave
        /// </summary>
        public int waveQuota;
        /// <summary>
        /// The number of enemies already spawned in this wave
        /// </summary>
        public int spawnCount;
        public float spawnInterval;
    }

    /// <summary>
    /// the class is to accomodate enemies data in a wave with one type of enemy
    /// </summary>
    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        /// <summary>
        /// The number of enemies to spawn of this EnemyGroup
        /// </summary>
        public int enemyCount;
        /// <summary>
        /// The number of enemies already spawned in this wave
        /// </summary>
        public int spawnCount;
        public GameObject enemyPrefab;
    }



    /// <summary>
    /// A List of all the waves in the game
    /// </summary>
    public List<Wave> waves;
    /// <summary>
    /// The Index of the current wave, start from zero
    /// </summary>
    public int currentWaveCount;

    private Transform player;

    /// <summary>
    /// Timer us to determine when to spawn the next enemy
    /// </summary>
    [Header("Spawner Attributes")]
    float spawnTimer;
    /// <summary>
    /// The interval between each wave
    /// </summary>
    public float waveInterval;

    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.FindGameObjectWithTag("Player") == null)
        {
            Debug.Log("is null");
        }
        player = GameObject.FindGameObjectWithTag("Player").transform;
        CalculateWaveQuota();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the wave has ended and the next wave should start
        if(currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0)
        {
            StartCoroutine(BeginNextWave());
        }

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }
    }

    IEnumerator BeginNextWave()
    {
        //wait for 'waveInterval' seconds before starting the next wave 
        yield return new WaitForSeconds(waveInterval);

        //If there are more waves to start after the current wave, move on to the next wave
        if(currentWaveCount < waves.Count - 1)
        {
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }

    public void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups) 
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }
        waves[currentWaveCount].waveQuota = currentWaveQuota;
        //Debug.Log(currentWaveQuota);
    }

    public void SpawnEnemies()
    {
        //Check if the mnimum number of enemies in the wave have been spawned
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota)
        {
            //Spawn each type of enemy until the quota is filled
            foreach(var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                //Check if the minimum number of enemies of this type have been spawned
                if(enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    //write the spawn enemy detail in here
                    Vector2 spawnPosition = new Vector2(player.transform.position.x + Random.Range(-10, 10), player.transform.position.y + Random.Range(-10, 10));
                    Instantiate(enemyGroup.enemyPrefab, spawnPosition, Quaternion.identity);

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                }
            }
        }
    }
    
}
