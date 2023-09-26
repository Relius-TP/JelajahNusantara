using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float detectionRadius = 3f;

    private Rigidbody2D rb;

    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        Vector2 velocity = movement * moveSpeed;

        rb.velocity = velocity;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy")) // Ubah "Enemy" dengan tag yang sesuai untuk musuh Anda.
            {
                // Musuh terdeteksi. Lakukan sesuatu, misalnya serang musuh atau tampilkan pesan.
                Debug.Log("Musuh terdeteksi!");
            }
        }




        if (Input.GetKey(KeyCode.LeftShift)){
            if (moveHorizontal != 0 || moveVertical != 0)
            {
                moveSpeed = 10;
                detectionRadius = 6f;
            }
        }
        else
        {
            moveSpeed = 5;
            detectionRadius = 3f;
        }
    }

    private void OnDrawGizmos()
    {
        // Menggambar lingkaran deteksi pada Scene View saat objek dipilih.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
