using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private InputSystem inputSystem;
    private Vector2 direction;
    private Transform playerPos;
    public static float detectionRadius;
    [SerializeField] private float speed;

    public static event Action OnCaught;

    private void OnEnable()
    {
        inputSystem.Player.Enable();
    }

    private void OnDisable()
    {
        inputSystem.Player.Disable();
    }

    private void Awake()
    {
        playerPos = GetComponent<Transform>();
        inputSystem = new InputSystem();
    }

    private void Update()
    {
        direction = inputSystem.Player.Movement.ReadValue<Vector2>();

        if(inputSystem.Player.Run.ReadValue<float>() == 1f)
        {
            speed = 7;
        }
        else if(MicDetection.soundVolume >= 2f)
        {
            speed = 10;
            detectionRadius = 6f;
        }
        else
        {
            speed = 5;
            detectionRadius = 3f;
        }

        playerPos.Translate(speed * Time.deltaTime * direction);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            OnCaught?.Invoke();
        }
    }
}
