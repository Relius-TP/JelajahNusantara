using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private float speed;

    private Light2D playerLight;
    private Animator animator;

    private InputSystem inputSystem;
    private Vector2 direction;
    private Rigidbody2D playerRb;
    private float runSpeed;
    private float screamSpeed;
    private float newDetectionRange;
    private float tempSpeed;
    private float tempVision;
    private bool isCollide = false;

    public static float detectionRadius;

    public static event Action<bool> OnWalk;

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
        animator = GetComponent<Animator>();
        playerLight = GetComponentInChildren<Light2D>();
        playerRb = GetComponent<Rigidbody2D>();
        inputSystem = new InputSystem();
        newDetectionRange = playerData.hero_visionRange;
        speed = playerData.hero_speed;
        runSpeed = speed + 1;
        screamSpeed = speed + 3;
    }

    private void Update()
    {
        direction = inputSystem.Player.Movement.ReadValue<Vector2>().normalized;

        Move();

        playerLight.pointLightOuterRadius = detectionRadius;
        playerRb.velocity = new Vector2(direction.x * speed, direction.y * speed);
    }

    private void Move()
    {
        if (inputSystem.Player.Run.ReadValue<float>() == 1f)
        {
            speed = runSpeed + tempSpeed;
        }
        else if (MicDetection.soundVolume >= 2f)
        {
            speed = screamSpeed + tempSpeed;
            detectionRadius = newDetectionRange + 3f + tempVision;
        }
        else if (direction != Vector2.zero)
        {
            speed = playerData.hero_speed + tempSpeed;
            detectionRadius = newDetectionRange + tempVision;
        }
        else
        {
            speed = playerData.hero_speed + tempSpeed;
            detectionRadius = newDetectionRange + tempVision;
        }
    }

    //private void PlayAnimation(string animationPrefix)
    //{
    //    if (direction.y > 0)
    //    {
    //        animator.SetTrigger(animationPrefix + "Up");
    //    }
    //    else if (direction.y < 0)
    //    {
    //        animator.SetTrigger(animationPrefix + "Down");
    //    }
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isCollide)
        {
            isCollide = true;
            GameManager.Instance.PlayerGotCaught();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && isCollide)
        {
            isCollide = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpeedPotion")){
            Destroy(collision.gameObject);
            tempSpeed = 3f;
            StartCoroutine(EffectTimeSpeed(5f));
        }

        if (collision.CompareTag("VisionPotion"))
        {
            Destroy(collision.gameObject);
            tempVision = 2f;
            StartCoroutine(EffectTimeVision(5f));
        }
    }

    IEnumerator EffectTimeSpeed(float time)
    {
        yield return new WaitForSeconds(time);
        tempSpeed = 0;
    }

    IEnumerator EffectTimeVision(float time)
    {
        yield return new WaitForSeconds(time);
        tempVision = 0;
    }
}
