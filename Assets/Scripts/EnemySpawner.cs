using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnRadius = 6f;
    [SerializeField] private float baseSpawnRate = 0.1f; 
    [SerializeField] private int maxEnemies = 2; // Add maximum enemy limit
    
    private float nextSpawnTime;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Only spawn if we're under the max enemy count
        if (Time.time >= nextSpawnTime && GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemies)
        {
            SpawnEnemy();
            float currentSpawnRate = baseSpawnRate / (1f + GameManager.Instance.Wave * 0.1f);
            nextSpawnTime = Time.time + currentSpawnRate;
        }
    }

    void SpawnEnemy()
    {
        // Get random point on circle around player
        Vector2 randomPoint = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawnPosition = player.position + new Vector3(randomPoint.x, randomPoint.y, 0);
        
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
} 