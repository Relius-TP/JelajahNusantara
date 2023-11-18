using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject spawn;
    public Transform spawnPosition;

    //skill 1
    private Vector2 positionSkill1;
    public GameObject checkSkill1Left;
    public GameObject checkSkill1Right;
    [SerializeField] private Vector2 skill1Left;
    [SerializeField] private Vector2 skill1Right;

    private Vector2 positionSkill2;



    void Start()
    {
        banaspati = this.transform;
        rb = GetComponent<Rigidbody2D>();

        spawnPosition = spawn.transform;

        positionSkill1 = new Vector2(24, -11);
        skill1Left = new Vector2(-24, -11);
        skill1Right = new Vector2(24, -11);



        //positionSkill2 = new Vector2(30, 11);

    }
    void Update()
    {
        if (bossHp >= 100)
        {
            if (loopCount <= 3)
            {
                Skill1Active();
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
        if (bossHp >= 100)
        {
            yield return new WaitForSeconds(5f);
            moveSpeedHor = 5;
            banaspati.transform.position = Vector2.MoveTowards(transform.position, spawnPosition, moveSpeedHor * Time.deltaTime);
            if (banaspati.transform.position.x == spawn.x && banaspati.transform.position.y == spawn.y)
            {
                loopCount = 0;
                banaspati.transform.position = Vector2.MoveTowards(transform.position, positionSkill1, moveSpeedHor * Time.deltaTime);
                moveSpeedHor = 45;
            }
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Kena Damage dari Bos");
        }

        if (collision.gameObject.tag == "Skill1Left")
        {
            loopCount += 1;
        }
        if (collision.gameObject.tag == "Skill1Right")
        {
            loopCount += 1;
        }
    }
}