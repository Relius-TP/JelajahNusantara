using UnityEngine;
using UnityEngine.UI;

public class PlayerOnBosStage : MonoBehaviour
{
    private Transform player;
    public static Vector3 playerPosition;
    public static float moveSpeed = 5.0f;
    public float jumpPower = 5.0f;
    private float moveHorizontal;

    private bool isJump = true;

    private Rigidbody2D rb;

    public float health = 100.0f;
    public Image healthBar;

    public GameObject gameOver;

    void Start()
    {
        player = this.transform;
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        Flip();

        rb.velocity = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.W) && isJump)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isJump = false;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 30;
        }
        else
        {
            moveSpeed = 20;
        }

        if (Input.GetKeyDown (KeyCode.RightShift))
        {
            TakeDamage(20);
        }

        if (Input.GetKeyDown (KeyCode.KeypadEnter))
        {
            Healing(5);
        }

        if (Input.GetKeyDown (KeyCode.LeftControl) && gameObject.GetComponent<SpriteRenderer>().flipX == false )
        {
            transform.position = new Vector2(transform.position.x + 2, transform.position.y);
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && gameObject.GetComponent<SpriteRenderer>().flipX == true)
        {
            transform.position = new Vector2(transform.position.x - 2, transform.position.y);
        }

        if (health == 0)
        {
            gameOver.SetActive(true);
            Time.timeScale = 0;
        }




    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = true;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boss")
        {
            TakeDamage(10);
            Debug.Log("Kena Damage dari Bos");
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.fillAmount = health / 100f;
    }

    public void Healing(float heal)
    {
        health += heal;
        health = Mathf.Clamp(health, 0, 100);

        healthBar.fillAmount = health / 100f;
    }
    public void Flip()
    {
        if (moveHorizontal < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (moveHorizontal > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}