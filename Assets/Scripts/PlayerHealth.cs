using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float invincibilityTime = 0.5f;
    [SerializeField] private GameObject floatingTextPrefab;
    private int currentHealth;
    private float invincibilityTimer = 0f;
    private SpriteRenderer spriteRenderer;
    private bool isInvincible = false;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            // Flash the player sprite
            spriteRenderer.enabled = Time.time % 0.2f < 0.1f;
            
            if (invincibilityTimer <= 0)
            {
                isInvincible = false;
                spriteRenderer.enabled = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !isInvincible)
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHealth -= damage;
        
        // Show floating damage text
        if (floatingTextPrefab != null)
        {
            FloatingText.Create(floatingTextPrefab, transform.position, 
                $"-{damage}", Color.red);
        }

        Debug.Log($"Player Health: {currentHealth}");

        // Trigger invincibility
        isInvincible = true;
        invincibilityTimer = invincibilityTime;

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        
        // Reset current score but keep high score
        ScoreManager.Instance.ResetCurrentScore();
        
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
} 