/*
 * PlayerController.cs
 * Movimentação 2D do player usando Rigidbody2D e Old Input Manager.
 * Fase 1: Movimentação horizontal + Pulo + Double Jump
 * Fase 2+: Ataque, animações e integração com sistema de combate.
 */

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask groundLayer;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 7.5f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    [SerializeField] private int maxJumps = 2; // 1 = normal, 2 = double jump

    [Header("Ground Check")]
    [SerializeField] private Vector2 groundCheckOffset = new Vector2(0f, -0.5f);
    [SerializeField] private Vector2 groundCheckSize = new Vector2(0.4f, 0.1f);

    // Components
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    // State
    private float moveInput;
    private bool isGrounded;
    private int jumpsRemaining;
    private bool facingRight = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // --- Input (Old Input Manager) ---
        moveInput = Input.GetAxis("Horizontal");

        // Flip sprite
        if (moveInput > 0.01f && !facingRight)
            Flip();
        else if (moveInput < -0.01f && facingRight)
            Flip();

        // Jump input
        if (Input.GetButtonDown("Jump") && jumpsRemaining > 0)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        // --- Ground check via OverlapBox ---
        CheckGrounded();

        // --- Move ---
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // --- Better jump feel ---
        // Falling: increase gravity for snappier descent
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.fixedDeltaTime;
        }
        // Short hop: if player releases jump early, fall faster
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.fixedDeltaTime;
        }
    }

    private void CheckGrounded()
    {
        Vector2 checkPos = (Vector2)transform.position + groundCheckOffset;
        Collider2D hit = Physics2D.OverlapBox(checkPos, groundCheckSize, 0f, groundLayer);

        bool wasGrounded = isGrounded;
        isGrounded = hit != null;

        // Reset jumps when landing
        if (isGrounded && !wasGrounded)
        {
            jumpsRemaining = maxJumps;
        }
    }

    private void Jump()
    {
        // Reset vertical velocity for consistent jump height (important for double jump)
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpsRemaining--;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !facingRight;
    }

    // --- Gizmos para visualizar o ground check no Editor ---
    private void OnDrawGizmosSelected()
    {
        Vector2 checkPos = (Vector2)transform.position + groundCheckOffset;
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireCube(checkPos, groundCheckSize);
    }
}
