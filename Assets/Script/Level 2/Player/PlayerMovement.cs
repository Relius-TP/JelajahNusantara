using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    
    private InputSystem inputSystem;

    private float walkSpeed;
    private float runSpeed = 1;
    private float runSpeedUsingMic = 2;
    private float currentSpeed;
    private float soundValue;

    private bool isRun;

    private Vector2 moveDirection;

    private void OnEnable()
    {
        MicDetection.OnSoundValueChanged += GetSoundValue;
    }

    private void OnDisable()
    {
        MicDetection.OnSoundValueChanged -= GetSoundValue;
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
        if (moveDirection != Vector2.zero && isRun && soundValue < 0.5f)
        {
            currentSpeed = walkSpeed + runSpeed;
        }
        else if (moveDirection != Vector2.zero && !isRun && soundValue < 0.5f)
        {
            currentSpeed = walkSpeed;
        }
        else if(moveDirection != Vector2.zero && soundValue > 0.5f)
        {
            currentSpeed = walkSpeed + runSpeedUsingMic;
        }
        transform.Translate(currentSpeed * Time.deltaTime * new Vector2(moveDirection.x, moveDirection.y));
    }

    private void GetSoundValue(float newSoundValue)
    {
        soundValue = newSoundValue;
    }
}
