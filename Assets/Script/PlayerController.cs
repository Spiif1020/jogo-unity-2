using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimento")]
    public float speed = 5f;

    [Header("Pulo")]
    public float jumpForce = 8f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Tiro")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float fireRate = 0.3f;
    private float nextFireTime = 0f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private GameManager gameManager;
    private bool isFacingRight = true;
    private bool isGrounded = false;
    private bool canMove = true;

    // Pulo duplo
    private bool canDoubleJump = false;
    private bool doubleJumpUnlocked = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        if (!canMove)
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("WALK", false);
            return;
        }

        // Verifica chão
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        animator.SetBool("isGrounded", isGrounded);

        if (isGrounded) 
            canDoubleJump = true;

        // ============================
        //       CONTROLES MOBILE
        // ============================
 float horizontal = 0f;

// Mobile
if (MobileControler.leftHeld)  horizontal = -1f;
if (MobileControler.rightHeld) horizontal = 1f;

// Teclado sobrescreve só se estiver apertado
float pcInput = Input.GetAxisRaw("Horizontal");
if (pcInput != 0)
    horizontal = pcInput;

movement.x = horizontal;


        movement.x = horizontal;
        bool isMoving = Mathf.Abs(horizontal) > 0.01f;
        animator.SetBool("WALK", isMoving);

        // Flip
        if (isMoving)
        {
            if (horizontal > 0 && !isFacingRight) Flip();
            if (horizontal < 0 && isFacingRight) Flip();
        }

        // ============================
        //              PULO
        // ============================

        bool jumpInput = Input.GetButtonDown("Jump") || MobileControler .jumpPressed;

        if (jumpInput)
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                animator.SetTrigger("JUMP");
            }
            else if (doubleJumpUnlocked && canDoubleJump)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                animator.SetTrigger("JUMP");
                canDoubleJump = false;
            }

            MobileControler.jumpPressed = false; // reseta para não pular infinitamente
        }
    }

    void FixedUpdate()
    {
        if (!canMove) return;
        rb.linearVelocity = new Vector2(movement.x * speed, rb.linearVelocity.y);
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();

        float direction = isFacingRight ? 1f : -1f;
        rbBullet.linearVelocity = new Vector2(bulletSpeed * direction, 0f);

        Vector3 scale = bullet.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        bullet.transform.localScale = scale;

        animator.SetTrigger("SHOOT");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Buraco"))
        {
            gameManager?.PerderJogo();
            canMove = false;
        }

        if (other.CompareTag("Bandeira"))
        {
            gameManager?.VencerJogo();
            canMove = false;
        }

        if (other.CompareTag("PowerUp"))
        {
            doubleJumpUnlocked = true;
            Destroy(other.gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
