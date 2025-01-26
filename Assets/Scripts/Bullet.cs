using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        // Destroy bullet after 3 seconds if it doesn't hit anything
        Destroy(gameObject, 3f);
    }

}
