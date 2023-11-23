using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    public static UnityAction OnCaught;

    public float moveSpeed = 5f;
    public static float detectionRadius = 3f;

    private Rigidbody2D rb;
    public float minSoundValue;
    public float soundValue;
    private Light2D light;
    
    private void Start()
    {
        light = GetComponentInChildren<Light2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //soundValue = MicDetection.soundValue;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        Vector2 velocity = movement * moveSpeed;

        rb.velocity = velocity;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        /*foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Debug.Log("Musuh terdeteksi!");
            }
        }*/

        if (Input.GetKey(KeyCode.LeftShift)){
            if (moveHorizontal != 0 || moveVertical != 0)
            {
                moveSpeed = 10;
                detectionRadius = 6f;
                light.pointLightOuterRadius = 10f;
            }
        }
        else
        {
            moveSpeed = 5;
            detectionRadius = 3f;
            light.pointLightOuterRadius = 3f;
        }

        if (MicDetection.soundVolume >= minSoundValue)
        {
            moveSpeed = 10;
            detectionRadius = 10f;
        }
    }

    private void OnDrawGizmos()
    {
        // Menggambar lingkaran deteksi pada Scene View saat objek dipilih.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            OnCaught?.Invoke();
        }
    }
}
