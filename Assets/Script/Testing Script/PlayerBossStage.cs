using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBossStage : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float health = 100f;

    private Rigidbody2D playerRb;

    private float moveDirection;
    private bool jump;
    private bool isJump = false;

    public static event Action<bool> PlayerDied;
    public static event Action<float, float> OnTakeDamage;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveDirection = Input.GetAxisRaw("Horizontal");
        jump = Input.GetKeyDown(KeyCode.W);
        Movement();
    }

    private void Movement()
    {
        playerRb.velocity = new Vector2(moveDirection * speed, playerRb.velocity.y);

        if (jump && !isJump)
        {
            isJump = true;
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void TakeDamage()
    {
        health -= 5f;
        healthBar.fillAmount = health / 100f;

        if (health <= 0)
        {
            PlayerDied?.Invoke(true);
        }

        OnTakeDamage?.Invoke(2f, .1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            TakeDamage();
        }
    }
}
