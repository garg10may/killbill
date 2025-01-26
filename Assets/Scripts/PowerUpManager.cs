using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [System.Serializable]
    public class PowerUpStats
    {
        public float fireRateMultiplier = 1f;
        public float bulletSpeedMultiplier = 1f;
        public float moveSpeedMultiplier = 1f;
        public int bulletDamage = 1;
    }

    public PowerUpStats currentStats = new PowerUpStats();
    
    public void ApplyPowerUp(PowerUpType type)
    {
        switch (type)
        {
            case PowerUpType.FireRate:
                currentStats.fireRateMultiplier *= 0.9f; // 10% faster firing
                break;
            case PowerUpType.BulletSpeed:
                currentStats.bulletSpeedMultiplier *= 1.2f; // 20% faster bullets
                break;
            case PowerUpType.MoveSpeed:
                currentStats.moveSpeedMultiplier *= 1.1f; // 10% faster movement
                break;
            case PowerUpType.Damage:
                currentStats.bulletDamage += 1; // +1 damage
                break;
        }
        
        // Update all relevant components
        UpdatePlayerComponents();
    }

    void UpdatePlayerComponents()
    {
        PlayerShooting shooting = GetComponent<PlayerShooting>();
        PlayerController movement = GetComponent<PlayerController>();
        
        if (shooting != null)
        {
            shooting.UpdateStats(currentStats);
        }
        
        if (movement != null)
        {
            movement.UpdateStats(currentStats);
        }
    }
}

public enum PowerUpType
{
    FireRate,
    BulletSpeed,
    MoveSpeed,
    Damage
} 