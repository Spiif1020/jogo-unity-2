
using UnityEngine;
using System.Collections;

public class plataformacaindo : MonoBehaviour
{
     [Header("Configurações")]
    public float fallDelay = 0.5f;       // tempo antes de cair
    public float respawnDelay = 3f;      // tempo antes de voltar
    public float gravityScale = 2f;      // intensidade da queda

    private Rigidbody2D rb;
    private Vector3 startPos;            // posição original
    private Quaternion startRotation;    // rotação original
    private bool isFalling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;           // fica parada até o player encostar
        startPos = transform.position;   // salva posição inicial
        startRotation = transform.rotation;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isFalling)
        {
            isFalling = true;
            StartCoroutine(FallAndRespawn());
        }
    }

    IEnumerator FallAndRespawn()
    {
        // opcional: tremidinha antes de cair
        yield return StartCoroutine(ShakeBeforeFall());

        yield return new WaitForSeconds(fallDelay);
        rb.isKinematic = false;
        rb.gravityScale = gravityScale;

        yield return new WaitForSeconds(respawnDelay);
        // resetar a plataforma
        rb.isKinematic = true;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.gravityScale = 0f;
        transform.position = startPos;
        transform.rotation = startRotation;

        isFalling = false;
    }

    IEnumerator ShakeBeforeFall()
    {
        Vector3 originalPos = transform.position;
        float elapsed = 0f;

        while (elapsed < fallDelay)
        {
            float x = Random.Range(-0.05f, 0.05f);
            transform.position = new Vector3(originalPos.x + x, originalPos.y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;
    }
}
