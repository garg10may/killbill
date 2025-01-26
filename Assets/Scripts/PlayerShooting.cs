using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate = 0.2f; // Adjusted for automatic firing
    [SerializeField] private float bulletSpeed = 10f;
    
    private float nextFireTime;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        
        if (bulletPrefab == null)
        {
            Debug.LogError("PlayerShooting: Bullet Prefab not assigned!");
        }
    }

    void Update()
    {
        // Automatic firing with fire rate limit
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || mainCamera == null) return;

        // Get mouse position using new Input System
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));
        mouseWorldPosition.z = 0f;

        // Calculate direction from player to mouse
        Vector2 direction = (mouseWorldPosition - transform.position).normalized;

        // Calculate rotation to face mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        // Spawn bullet
        GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
        
        // Get or add Rigidbody2D to bullet
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = bullet.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
        }

        // Set bullet velocity
        rb.linearVelocity = direction * bulletSpeed;
    }

    public void UpdateStats(PowerUpManager.PowerUpStats stats)
    {
        fireRate = 0.2f * stats.fireRateMultiplier;
        bulletSpeed = 10f * stats.bulletSpeedMultiplier;
    }
} 