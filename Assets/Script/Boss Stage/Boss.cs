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
    
    private int bossHp = 100;

    private Vector2 positionSkill1;
    [SerializeField] private Vector2 skill1Left;
    [SerializeField] private Vector2 skill1Right;

    private Vector2 positionSkill2;



    void Start()
    {
        banaspati = this.transform;
        rb = GetComponent<Rigidbody2D>();

        positionSkill1 = new Vector2(31, -11);
        skill1Left = new Vector2(-31, -11);
        skill1Right = new Vector2(31, -11);

        //positionSkill2 = new Vector2(30, 11);

    }
    void Update()
    {
        Skill1Active();
        if (loopCount <= 3)
        {
            Skill1();
        }
        else
        {
            Stun();
        }
        Invoke("MaxCount", 5);


    }

    void MaxCount()
    {
        loopCount = 4;
    }

    bool isMoving()
    {
        return rb.velocity.magnitude > movementThreshold;
    }
    
    void Skill1()
    {
        if (banaspati.transform.position.x == skill1Left.x && banaspati.transform.position.y == skill1Left.y)
        {
            Invoke("Skill1Right", 1);
        }
        if (banaspati.transform.position.x == skill1Right.x && banaspati.transform.position.y == skill1Right.y)
        {
            Invoke("Skill1Left", 1);
        }
    }
    
    void Skill1Active()
    {
        if (banaspati.position.x !=  positionSkill1.x && banaspati.position.y != positionSkill1.y && !isMoving())
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

    void Stun()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {

        }
    }
}