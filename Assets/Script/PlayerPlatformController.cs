using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformController : MonoBehaviour
{
    private Transform originalParent; // Guarda o parent original do jogador

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plataforma"))
        {
            // Salva o parent original e define a plataforma como parent temporário
            originalParent = transform.parent;
            transform.SetParent(collision.transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plataforma"))
        {
            // Restaura o parent original quando sai da plataforma
            transform.SetParent(originalParent);
        }
    }
}