using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 moveInput;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        
        // Configure Rigidbody2D
        rb.gravityScale = 0;
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        // Get input using the new Input System
        moveInput = new Vector2(
            Keyboard.current.aKey.isPressed ? -1 : (Keyboard.current.dKey.isPressed ? 1 : 0),
            Keyboard.current.sKey.isPressed ? -1 : (Keyboard.current.wKey.isPressed ? 1 : 0)
        );

        // Normalize the input vector to prevent faster diagonal movement
        moveInput = Vector2.ClampMagnitude(moveInput, 1f);
    }

    void FixedUpdate()
    {
        // Move the player using physics
        rb.linearVelocity = moveInput * moveSpeed;
    }

    public void UpdateStats(PowerUpManager.PowerUpStats stats)
    {
        moveSpeed = 5f * stats.moveSpeedMultiplier;
    }
}
