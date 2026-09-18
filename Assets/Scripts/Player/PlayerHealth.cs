/*
 * PlayerHealth.cs
 * Sistema de vida do player.
 * Gerencia HP, recebimento de dano e morte.
 */

using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHP = 5;
    private int currentHP;

    [Header("Invincibility")]
    [SerializeField] private float invincibilityDuration = 1f;
    private float invincibilityTimer = 0f;
    private bool isInvincible = false;

    // Components
    private SpriteRenderer spriteRenderer;

    public int CurrentHP => currentHP;
    public int MaxHP => maxHP;
    public bool IsInvincible => isInvincible;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        currentHP = maxHP;
    }

    void Update()
    {
        // Invincibility timer
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;

            // Blink effect during invincibility
            spriteRenderer.enabled = Mathf.Sin(Time.time * 20f) > 0f;

            if (invincibilityTimer <= 0f)
            {
                isInvincible = false;
                spriteRenderer.enabled = true;
            }
        }
    }

    public void TakeDamage(int amount)
    {
        if (isInvincible) return;

        currentHP -= amount;
        Debug.Log($"Player took {amount} damage! HP: {currentHP}/{maxHP}");

        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
            return;
        }

        // Start invincibility frames
        isInvincible = true;
        invincibilityTimer = invincibilityDuration;
    }

    public void Heal(int amount)
    {
        currentHP = Mathf.Min(currentHP + amount, maxHP);
        Debug.Log($"Player healed {amount}! HP: {currentHP}/{maxHP}");
    }

    private void Die()
    {
        Debug.Log("Player died!");
        // TODO: Fase posterior — tela de game over, respawn, etc.
        // Por enquanto, desabilita o player
        gameObject.SetActive(false);
    }
}
