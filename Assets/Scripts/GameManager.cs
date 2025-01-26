using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public int Score { get; private set; }
    public float GameTime { get; private set; }
    public int Wave { get; private set; } = 1;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        GameTime += Time.deltaTime;
        
        // Increase wave every minute
        Wave = 1 + Mathf.FloorToInt(GameTime / 60f);
    }

    public void AddScore(int amount)
    {
        Score += amount;
        Debug.Log($"Score: {Score}");
    }
} 