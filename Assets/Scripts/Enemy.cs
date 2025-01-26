using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f; // Starting with 1 unit per second
    [SerializeField] private int experienceValue = 10;
    [SerializeField] private int damage = 10;
    private Transform player;
    private Rigidbody2D rb;
    private ExperienceManager experienceManager;

    void Start()
    {
        // Find player by tag instead of storing the transform
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            experienceManager = playerObject.GetComponent<ExperienceManager>();
        }
        else
        {
            Debug.LogError("Enemy: Player not found! Make sure Player has 'Player' tag.");
        }

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        // Configure Rigidbody2D
        rb.gravityScale = 0;
        rb.isKinematic = true;
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Re-find player if lost reference
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
                experienceManager = playerObject.GetComponent<ExperienceManager>();
            }
            return;
        }

        // Move towards current player position
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(
            transform.position, 
            player.position, 
            moveSpeed * Time.deltaTime
        );
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            // Make sure ScoreManager exists
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(10);
                Debug.Log($"Score added. Current score: {ScoreManager.Instance.CurrentScore}");
            }
            else
            {
                Debug.LogError("ScoreManager not found!");
            }

            // Add experience
            if (experienceManager != null)
            {
                experienceManager.AddExperience(experienceValue);
            }
            
            // Destroy bullet and enemy
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
