using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private float speed;

    private Light2D playerLight;

    private InputSystem inputSystem;
    private Vector2 direction;
    private Transform playerPos;
    private float runSpeed;
    private float screamSpeed;
    private float newDetectionRange;
    private float tempSpeed;
    private float tempVision;

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
        playerLight = GetComponentInChildren<Light2D>();
        playerPos = GetComponent<Transform>();
        inputSystem = new InputSystem();
        newDetectionRange = playerData.hero_visionRange;
        speed = playerData.hero_speed;
        runSpeed = speed + 2;
        screamSpeed = speed + 4;
    }

    private void Update()
    {
        direction = inputSystem.Player.Movement.ReadValue<Vector2>();

        if(inputSystem.Player.Run.ReadValue<float>() == 1f)
        {
            speed = runSpeed + tempSpeed;
        }
        else if(MicDetection.soundVolume >= 2f)
        {
            speed = screamSpeed + tempSpeed;
            detectionRadius = newDetectionRange + 3f + tempVision;
        }
        else
        {
            speed = playerData.hero_speed + tempSpeed;
            detectionRadius = newDetectionRange + tempVision;
        }

        playerLight.pointLightOuterRadius = detectionRadius;
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
            GameManager.Instance.PlayerGotCaught();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpeedPotion")){
            Destroy(collision.gameObject);
            tempSpeed = 2f;
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
