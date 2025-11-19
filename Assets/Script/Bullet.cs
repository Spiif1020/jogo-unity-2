using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 100000000000000f; // tempo até sumir

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Adapte conforme seu jogo     
        if (other.CompareTag("Inimigo"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}

