using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopBotSkill2 : MonoBehaviour
{
    public GameObject boss;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (boss.GetComponent<SpriteRenderer>().flipX == false)
            {
                Debug.Log("Ke Atas");
                Boss.moveSpeedVer = 10;
            }
            
        }
        
        
    }
}
