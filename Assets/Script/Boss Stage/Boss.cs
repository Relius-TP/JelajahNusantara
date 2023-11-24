using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    //statistik banaspati
    private Transform banaspati;
    private Rigidbody2D rb;
    public static float moveSpeedVer = 100f;
    public static float moveSpeedHor = 5f;
    public float movementThreshold = 0.1f;
    [SerializeField]private int loopCount;
    
    private int bossHp = 200;
    public static bool isTakingDamage = false;

    private Vector2 spawn;
    public GameObject qte;

    //skill 1
    private Vector2 positionSkill1;
    public GameObject checkSkill1Left;
    public GameObject checkSkill1Right;
    [SerializeField] private Vector2 skill1Left;
    [SerializeField] private Vector2 skill1Right;
    public GameObject skill1checker;

    //skill2
    private Vector2 positionSkill2;
    [SerializeField] private Vector2 skill2Up;
    [SerializeField] private Vector2 skill2Down;
    [SerializeField]private bool skill2UpActive;
    [SerializeField]private bool skill2DownActive;

    public float bossHealth = 200.0f;
    public Image bossHealt;
    public GameObject winScene;

    private void OnEnable()
    {
        SkillCheckQTEBossController.GiveDamage += TakeDamage;
    }

    private void OnDisable()
    {
        SkillCheckQTEBossController.GiveDamage -= TakeDamage;
    }

    void Start()
    {
        Time.timeScale = 1;
        banaspati = this.transform;
        rb = GetComponent<Rigidbody2D>();

        spawn = new Vector2(0, 0);

        positionSkill1 = new Vector2(24, -13);
        skill1Left = new Vector2(-24, -13);
        skill1Right = new Vector2(24, -13);

        positionSkill2 = new Vector2(24, -14);
        skill2Up = new Vector2(0, 10);
        skill2Down = new Vector2(0, -14);
        skill2UpActive = false;
        skill2DownActive = false;
    }
    void Update()
    {
        if (bossHp >= 100)
        {
            if (loopCount <= 3)
            {
                Invoke("Skill1Active", 5);
                StartCoroutine(Skill1());
            }
            else if (loopCount == 4)
            {
                StartCoroutine(Stun());
            }
        }
        else if(bossHp < 100)
        {
            skill1checker.SetActive(false);
            if (loopCount <= 3)
            {
                StartCoroutine(Skill2());
            }
            else if (loopCount == 4)
            {
                StartCoroutine(Stun());
            }
        }

        if (bossHealth <= 0)
        {
            winScene.SetActive(true);
            Time.timeScale = 0;
        }
    }

    IEnumerator Skill1()
    {
        if (banaspati.transform.position.x == skill1Left.x && banaspati.transform.position.y == skill1Left.y)
        {
            yield return new WaitForSeconds(1f);
            Skill1Right();
        }
        else if (banaspati.transform.position.x == skill1Right.x && banaspati.transform.position.y == skill1Right.y)
        {
            yield return new WaitForSeconds(1f);
            Skill1Left();
        }
    }
    
    void Skill1Active()
    {
        if (banaspati.position.x !=  positionSkill1.x && banaspati.position.y != positionSkill1.y)
        {
            banaspati.transform.position = Vector2.MoveTowards(transform.position, positionSkill1, moveSpeedHor * Time.deltaTime);
            moveSpeedHor = 45;
        }
    }
    void Skill1Left()
    {
        banaspati.transform.position = Vector2.MoveTowards(transform.position, skill1Left, moveSpeedHor * Time.deltaTime);
        moveSpeedHor = 75;
        GetComponent<SpriteRenderer>().flipX = true;
    }
    void Skill1Right()
    {
        banaspati.transform.position = Vector2.MoveTowards(transform.position, skill1Right, moveSpeedHor * Time.deltaTime);
        moveSpeedHor = 75;
        GetComponent<SpriteRenderer>().flipX = false;
    }



    IEnumerator Skill2()
    {
        if (banaspati.transform.position.y == skill2Down.y)
        {
            yield return new WaitForSeconds(0.35f);
            moveSpeedVer = 100;
            Skill2Up();
            if (banaspati.transform.position.y == skill2Up.y)
            {
                skill2UpActive = true;
                skill2DownActive = false;
            }
        }
        else if (banaspati.transform.position.y == skill2Up.y)
        {
            yield return new WaitForSeconds(0.35f);
            moveSpeedVer = 100;
            Skill2Down();
            if (banaspati.transform.position.y == skill2Down.y)
            {
                skill2UpActive = false;
                skill2DownActive = true;
            }
            
        }
        else if (skill2UpActive == false && skill2DownActive == false)
        {
            yield return new WaitForSeconds(0.1f);
            moveSpeedHor = 45;
            banaspati.transform.position = Vector2.MoveTowards(transform.position, positionSkill2, moveSpeedHor * Time.deltaTime);
            if (banaspati.transform.position.x == positionSkill2.x && banaspati.transform.position.y == positionSkill2.y)
            {
                moveSpeedVer = 0;
                skill2UpActive = true;
            }
            
        }
       
    }
    void Skill2Up()
    {
        if (banaspati.GetComponent<SpriteRenderer>().flipX == false)
        {
            banaspati.transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x - 0.2f, 10), moveSpeedVer * Time.deltaTime);
        }
        else if(banaspati.GetComponent<SpriteRenderer>().flipX == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + 0.2f, 10), moveSpeedVer * Time.deltaTime);
        }
        
    }

    void Skill2Down()
    {
        if (banaspati.GetComponent<SpriteRenderer>().flipX == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x - 0.2f, -14), moveSpeedVer * Time.deltaTime);
        }
        else if (banaspati.GetComponent<SpriteRenderer>().flipX == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + 0.2f, -14), moveSpeedVer * Time.deltaTime);
        }
    }


    IEnumerator Stun()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            QTEActive();
            yield return new WaitForSeconds(5f);
            SkillCheckQTEBossController.bossStun = false;
            if (bossHp >= 100)
            {
                qte.SetActive(false);
                moveSpeedHor = 5;
                banaspati.transform.position = Vector2.MoveTowards(transform.position, spawn, moveSpeedHor * Time.deltaTime);
                yield return new WaitForSeconds(7f);
                if (banaspati.transform.position.x == spawn.x && banaspati.transform.position.y == spawn.y)
                {
                    loopCount = 0;
                    banaspati.transform.position = Vector2.MoveTowards(transform.position, positionSkill1, moveSpeedHor * Time.deltaTime);
                    moveSpeedHor = 45;
                }
            }
            else if (bossHp < 100)
            {
                qte.SetActive(false);
                moveSpeedHor = 5;
                loopCount = 0;
                banaspati.transform.position = Vector2.MoveTowards(transform.position, spawn, moveSpeedHor * Time.deltaTime);
                yield return new WaitForSeconds(7f);
                if (banaspati.transform.position.x == spawn.x && banaspati.transform.position.y == spawn.y)
                {
                    skill2UpActive = false;
                    skill2DownActive = false;
                }
            }
        }
        else
        {
            yield return new WaitForSeconds(5f);
            SkillCheckQTEBossController.bossStun = false;
            if (bossHp >= 100)
            {
                qte.SetActive(false);
                moveSpeedHor = 5;
                banaspati.transform.position = Vector2.MoveTowards(transform.position, spawn, moveSpeedHor * Time.deltaTime);
                yield return new WaitForSeconds(7f);
                if (banaspati.transform.position.x == spawn.x && banaspati.transform.position.y == spawn.y)
                {
                    loopCount = 0;
                    banaspati.transform.position = Vector2.MoveTowards(transform.position, positionSkill1, moveSpeedHor * Time.deltaTime);
                    moveSpeedHor = 45;
                }
            }
            else if (bossHp < 100)
            {
                qte.SetActive(false);
                moveSpeedHor = 5;
                loopCount = 0;
                banaspati.transform.position = Vector2.MoveTowards(transform.position, spawn, moveSpeedHor * Time.deltaTime);
                yield return new WaitForSeconds(7f);
                if (banaspati.transform.position.x == spawn.x && banaspati.transform.position.y == spawn.y)
                {
                    skill2UpActive = false;
                    skill2DownActive = false;
                }
            }
        }
    }

    void QTEActive()
    {
        qte.SetActive(true);
        SkillCheckQTEBossController.bossStun = true;
        PlayerOnBosStage.moveSpeed = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Skill1Left")
        {
            loopCount += 1;
        }
        if (collision.gameObject.tag == "Skill1Right")
        {
            loopCount += 1;
        }
        if (collision.gameObject.tag == "LeftWall")
        {
            GetComponent<SpriteRenderer>().flipX = true;
            loopCount += 1;
        }
        if (collision.gameObject.tag == "RightWall")
        {
            GetComponent<SpriteRenderer>().flipX = false;
            loopCount += 1;
        }
    }


    public void TakeDamage(float damage)
    {
        bossHealth -= damage;
        bossHealt.fillAmount = bossHealth / 200f;
    }

}