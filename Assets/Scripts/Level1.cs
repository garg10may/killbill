using UnityEngine;
using UnityEngine.InputSystem;

public class Level1 : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private float detectionRange = 10f;

    private GameObject spawnedPlayer;
    private GameObject spawnedEnemy;
    private Camera mainCamera;
    private GameObject currentTarget;
    private bool isMovingRight = true;

    private void Start()
    {
        mainCamera = Camera.main;
        Debug.Log("Starting spawns...");
        InvokeRepeating("SpawnPlayer", 0f, 2f);
        InvokeRepeating("SpawnEnemy", 0f, 2f);
    }

    private void SpawnPlayer()
    {
        // Spawn at left side, with slight vertical variation
        Vector3 spawnPosition = new Vector3(-6f, Random.Range(-3f, 3f), 0);
        spawnedPlayer = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        spawnedPlayer.tag = "Player";
        Debug.Log($"Player spawned at {spawnPosition}");
    }

    private void SpawnEnemy()
    {
        // Spawn at right side, with slight vertical variation
        Vector3 spawnPosition = new Vector3(6f, Random.Range(-3f, 3f), 0);
        spawnedEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        spawnedEnemy.tag = "Enemy";
        Debug.Log($"Enemy spawned at {spawnPosition}");
    }

    private void FixedUpdate()
    {
        // PLAYER LOGIC
        if (spawnedPlayer != null)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            
            if (enemies.Length > 0)
            {
                // Find nearest enemy
                GameObject nearestEnemy = null;
                float nearestDistance = float.MaxValue;
                
                foreach (GameObject enemy in enemies)
                {
                    float distance = Vector2.Distance(spawnedPlayer.transform.position, enemy.transform.position);
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestEnemy = enemy;
                    }
                }

                if (nearestEnemy != null)
                {
                    // Move directly towards nearest enemy
                    Vector3 direction = (nearestEnemy.transform.position - spawnedPlayer.transform.position).normalized;
                    spawnedPlayer.transform.position += direction * moveSpeed * Time.fixedDeltaTime;
                }
            }
            else
            {
                // Move right towards enemy base
                spawnedPlayer.transform.position += Vector3.right * moveSpeed * Time.fixedDeltaTime;
            }
        }

        // ENEMY LOGIC
        if (spawnedEnemy != null)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            
            if (players.Length > 0)
            {
                // Find nearest player
                GameObject nearestPlayer = null;
                float nearestDistance = float.MaxValue;
                
                foreach (GameObject player in players)
                {
                    float distance = Vector2.Distance(spawnedEnemy.transform.position, player.transform.position);
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestPlayer = player;
                    }
                }

                if (nearestPlayer != null)
                {
                    // Move directly towards nearest player
                    Vector3 direction = (nearestPlayer.transform.position - spawnedEnemy.transform.position).normalized;
                    spawnedEnemy.transform.position += direction * moveSpeed * Time.fixedDeltaTime;
                }
            }
            else
            {
                // Move left towards player base
                spawnedEnemy.transform.position += Vector3.left * moveSpeed * Time.fixedDeltaTime;
            }
        }
    }
} 