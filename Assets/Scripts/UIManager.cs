using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI References")]
    public Slider healthBar;
    public Slider experienceBar;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI healthText;

    private PlayerHealth playerHealth;
    private ExperienceManager experienceManager;
    private GameManager gameManager;

    void Start()
    {
        // Find references if not already set
        if (playerHealth == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerHealth = player.GetComponent<PlayerHealth>();
                experienceManager = player.GetComponent<ExperienceManager>();
            }
        }

        if (gameManager == null)
        {
            gameManager = GameManager.Instance;
        }

        // Initialize UI values
        UpdateAllUI();
    }

    void Update()
    {
        UpdateAllUI();
    }

    void UpdateAllUI()
    {
        UpdateHealthUI();
        UpdateExperienceUI();
        UpdateScoreUI();
    }

    void UpdateHealthUI()
    {
        if (playerHealth != null && healthBar != null && healthText != null)
        {
            float healthPercent = (float)playerHealth.CurrentHealth / playerHealth.MaxHealth;
            healthBar.value = healthPercent;
            healthText.text = $"{playerHealth.CurrentHealth}/{playerHealth.MaxHealth}";
        }
    }

    void UpdateExperienceUI()
    {
        if (experienceManager != null && experienceBar != null && levelText != null)
        {
            float expPercent = (float)experienceManager.Experience / experienceManager.ExperienceToNextLevel;
            experienceBar.value = expPercent;
            levelText.text = $"Level {experienceManager.CurrentLevel}";
        }
    }

    void UpdateScoreUI()
    {
        if (gameManager != null && scoreText != null)
        {
            scoreText.text = $"Score: {gameManager.Score}";
        }
    }
} 