using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private InputSystem inputSystem;
    private Vector2 direction;
    private Transform playerPos;
    public static float detectionRadius;
    [SerializeField] private float speed;
    private float runSpeed;
    private float screamSpeed;
    [SerializeField] private PlayerData playerData;
    private float newDetectionRange;

    private Animator anim;

    public static event Action OnCaught;
    public static event Action<bool> OnWalk;

    private void OnEnable()
    {
        inputSystem.Player.Enable();
        PotionDetection.GetSpeedPotion += GetSpeedPotion;
        PotionDetection.GetVisionPotion += GetVisionPotion;
    }

    private void OnDisable()
    {
        inputSystem.Player.Disable();
        PotionDetection.GetSpeedPotion -= GetSpeedPotion;
        PotionDetection.GetVisionPotion -= GetVisionPotion;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
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

        if(direction != Vector2.zero)
        {
            OnWalk?.Invoke(true);
            anim.SetTrigger("Walking");
        }
        else
        {
            OnWalk?.Invoke(false);
        }

        if(inputSystem.Player.Run.ReadValue<float>() == 1f)
        {
            speed = runSpeed;
            anim.SetTrigger("Walking");
        }
        else if(MicDetection.soundVolume >= 2f)
        {
            speed = screamSpeed;
            detectionRadius = newDetectionRange + 3f;
            anim.SetTrigger("Walking");
        }
        else
        {
            speed = playerData.hero_speed;
            detectionRadius = newDetectionRange;
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

    private void GetVisionPotion(float effect, float duration)
    {
        newDetectionRange += effect;
        StartCoroutine(ResetVisionStatus(effect, duration));
    }

    IEnumerator ResetVisionStatus(float effect, float duration)
    {
        yield return new WaitForSeconds(duration);
        newDetectionRange -= effect;
    }

    private void GetSpeedPotion(float effect, float duration)
    {
        runSpeed += effect;
        screamSpeed += effect;
        StartCoroutine(ResetSpeedStatus(effect, duration));
    }

    IEnumerator ResetSpeedStatus(float effect, float duration)
    {
        yield return new WaitForSeconds(duration);
        runSpeed -= effect;
        screamSpeed -= effect;
    }
}
