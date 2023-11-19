using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    //statistik banaspati
    private Transform banaspati;
    private Rigidbody2D rb;
    public float moveSpeedVer = 10f;
    public float moveSpeedHor = 5f;
    public float movementThreshold = 0.1f;
    [SerializeField]private int loopCount;
    
    private int bossHp = 200;

    private Vector2 spawn;
    public GameObject qte;

    //skill 1
    private Vector2 positionSkill1;
    public GameObject checkSkill1Left;
    public GameObject checkSkill1Right;
    [SerializeField] private Vector2 skill1Left;
    [SerializeField] private Vector2 skill1Right;

    private Vector2 positionSkill2;


    public float bossHealth = 200.0f;
    public Image bossHealt;
    public GameObject winScene;

    void Start()
    {
        banaspati = this.transform;
        rb = GetComponent<Rigidbody2D>();

        spawn = new Vector2(0, 0);

        positionSkill1 = new Vector2(24, -13);
        skill1Left = new Vector2(-24, -13);
        skill1Right = new Vector2(24, -13);



        //positionSkill2 = new Vector2(30, 11);

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
            else
            {
                StartCoroutine(Stun());
            }
        }
        else
        {
            Skill2Active();
        }

        if (bossHealth == 0)
        {
            winScene.SetActive(true);
            Time.timeScale = 0;
        }

    }
    bool isMoving()
    {
        return rb.velocity.magnitude > movementThreshold;
    }

    IEnumerator Skill1()
    {
        
        if (banaspati.transform.position.x == skill1Left.x && banaspati.transform.position.y == skill1Left.y)
        {
            yield return new WaitForSeconds(1f);
            Skill1Right();
        }
        if (banaspati.transform.position.x == skill1Right.x && banaspati.transform.position.y == skill1Right.y)
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
    }
    void Skill1Right()
    {
        banaspati.transform.position = Vector2.MoveTowards(transform.position, skill1Right, moveSpeedHor * Time.deltaTime);
        moveSpeedHor = 75;
    }


    void Skill2Active()
    {

    }

    IEnumerator Stun()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            QTEActive();
            yield return new WaitForSeconds(5f);
            if (bossHp >= 100)
            {
                qte.SetActive(false);
                moveSpeedHor = 5;
                banaspati.transform.position = Vector2.MoveTowards(transform.position, spawn, moveSpeedHor * Time.deltaTime);
                if (banaspati.transform.position.x == spawn.x && banaspati.transform.position.y == spawn.y)
                {
                    loopCount = 0;
                    banaspati.transform.position = Vector2.MoveTowards(transform.position, positionSkill1, moveSpeedHor * Time.deltaTime);
                    moveSpeedHor = 45;
                }
            }
        }
        else
        {
            yield return new WaitForSeconds(5f);
            if (bossHp >= 100)
            {
                qte.SetActive(false);
                moveSpeedHor = 5;
                banaspati.transform.position = Vector2.MoveTowards(transform.position, spawn, moveSpeedHor * Time.deltaTime);
                if (banaspati.transform.position.x == spawn.x && banaspati.transform.position.y == spawn.y)
                {
                    loopCount = 0;
                    banaspati.transform.position = Vector2.MoveTowards(transform.position, positionSkill1, moveSpeedHor * Time.deltaTime);
                    moveSpeedHor = 45;
                }
            }
        }
    }

    void QTEActive()
    {
        qte.SetActive(true);
        SkillCheckQTEBossController.bossStun = true;
        PlayerOnBosStage.moveSpeed = 0;

        if (SkillCheckQTEBossController.result == 1)
        {
            TakeDamage(10);
            Debug.Log("BOSS Kena 10 Damage");
        }
        else if (SkillCheckQTEBossController.result == 2)
        {
            TakeDamage(20);
            Debug.Log("BOSS Kena 20 Damage");
        }
        else if (SkillCheckQTEBossController.result == 3)
        {
            TakeDamage(30);
            Debug.Log("BOSS Kena 30 Damage");
        }
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
    }

    public void TakeDamage(float damage)
    {
        bossHealth -= damage;
        bossHealt.fillAmount = bossHealth / 100f;
    }
}