using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerOnLobby : MonoBehaviour
{
    [SerializeField]private float speed;
    private float moveHor;
    private float moveVert;
    private Rigidbody2D rb;

    public GameObject cupboardMenu;
    public Button gacha;
    public Button heroSelection;
    public Button cupboardMenuClose;

    public GameObject gachaCanvas;
    public Button gachaClose;

    public GameObject heroCanvas;
    public Button heroClose;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveHor = Input.GetAxisRaw("Horizontal");
        moveVert = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2 (moveHor * speed, moveVert * speed);
        Flip();

        gacha.onClick.AddListener(Gacha);
        heroSelection.onClick.AddListener(HeroSelection);
        cupboardMenuClose.onClick.AddListener (CupboardClose);
        gachaClose.onClick.AddListener(GachaClose);
        heroClose.onClick.AddListener(HeroClose);

        Esc();
    }

    public void Flip()
    {
        if (moveHor < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (moveHor > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Play")
        {
            PlayGame();
        }

        if (collision.gameObject.tag == "Cupboard")
        {
            Debug.Log("Cupboard");
            cupboardMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void Gacha()
    {
        gachaCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    void HeroSelection()
    {
        heroCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    void Esc()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gachaCanvas == true && cupboardMenu == true)
        {
            gachaCanvas.SetActive(false);
            Time.timeScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && heroCanvas == true && cupboardMenu == true)
        {
            heroCanvas.SetActive(false);
            Time.timeScale = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gachaCanvas == false && cupboardMenu == true && heroCanvas == false)
        {
            cupboardMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    void GachaClose()
    {
        gachaCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    void HeroClose()
    {
        heroCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    void CupboardClose()
    {
        cupboardMenu.SetActive(false);
        Time.timeScale = 1;
    }

    void PlayGame()
    {
        SceneManager.LoadScene("Level2");
    }
}
