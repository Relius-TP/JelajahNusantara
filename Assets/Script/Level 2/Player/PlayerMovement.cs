using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    
    private InputSystem inputSystem;

    private float walkSpeed;
    private float runSpeed = 1;
    private float runSpeedUsingMic = 2;
    private float totalMovementSpeed;
    private float speedPotionEffect;
    private float currentSpeed;
    private float soundValue;

    private bool isRun;

    private Vector2 moveDirection;
    

    private void OnEnable()
    {
        MicDetection.OnSoundValueChanged += GetSoundValue;
        PotionDetection.GetSpeedPotion += OnSpeedPotionEffect;
    }

    private void OnDisable()
    {
        MicDetection.OnSoundValueChanged -= GetSoundValue;
        PotionDetection.GetSpeedPotion -= OnSpeedPotionEffect;
    }

    private void Awake()
    {
        inputSystem = new InputSystem();
        inputSystem.Player.Enable();
    }

    private void Start()
    {
        GetPlayerData();
    }

    private void Update()
    {
        moveDirection = inputSystem.Player.Movement.ReadValue<Vector2>();
        PlayerMove();

        if(inputSystem.Player.Run.ReadValue<float>() > 0) isRun = true;
        else isRun = false;
    }

    private void GetPlayerData()
    {
        walkSpeed = playerData.hero_speed;
    }

    private void PlayerMove()
    {
        totalMovementSpeed = walkSpeed + speedPotionEffect;

        if (moveDirection != Vector2.zero && isRun && soundValue < 0.5f)
        {
            currentSpeed = totalMovementSpeed + runSpeed;
        }
        else if (moveDirection != Vector2.zero && !isRun && soundValue < 0.5f)
        {
            currentSpeed = totalMovementSpeed;
        }
        else if(moveDirection != Vector2.zero && soundValue > 0.5f)
        {
            currentSpeed = totalMovementSpeed + runSpeedUsingMic;
        }
        transform.Translate(currentSpeed * Time.deltaTime * new Vector2(moveDirection.x, moveDirection.y));
    }

    private void GetSoundValue(float newSoundValue)
    {
        soundValue = newSoundValue;
    }

    private void OnSpeedPotionEffect(float value, float duration)
    {
        speedPotionEffect = value;
        StartCoroutine(SpeedPotionDuration(duration));
    }

    IEnumerator SpeedPotionDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        speedPotionEffect = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.PlayerGotCaught();
        }
    }
}
