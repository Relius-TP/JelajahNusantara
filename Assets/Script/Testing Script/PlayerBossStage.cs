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

    private Animator animator;

    [SerializeField]private AudioClip audioClip;
    [SerializeField]private AudioSource audioSource;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        moveDirection = Input.GetAxisRaw("Horizontal");
        jump = Input.GetKeyDown(KeyCode.W);
        Movement();

        if (Input.GetKeyDown(KeyCode.LeftControl) && gameObject.GetComponent<SpriteRenderer>().flipX == false)
        {
            transform.position = new Vector2(transform.position.x + 2, transform.position.y);
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && gameObject.GetComponent<SpriteRenderer>().flipX == true)
        {
            transform.position = new Vector2(transform.position.x - 2, transform.position.y);
        }
    }

    private void Movement()
    {
        playerRb.velocity = new Vector2(moveDirection * speed, playerRb.velocity.y);
        Flip();

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
    public void Flip()
    {
        if (moveDirection != 0)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
            }
        }

        if (moveDirection < 0)
        {
            animator.SetTrigger("Side");
            animator.SetBool("Idle", false);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (moveDirection > 0)
        {
            animator.SetTrigger("Side");
            animator.SetBool("Idle", false);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (moveDirection == 0)
        {
            animator.SetBool("Idle", true);
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
