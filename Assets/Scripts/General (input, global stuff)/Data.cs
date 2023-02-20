using UnityEngine;
using EnumsAndStructs;

public class Data : MonoBehaviour
{
    #region Inspector editable data

    [Header("---Character---")]

    [SerializeField] [Range(0, 100)] float _characterSpeed;
    [SerializeField] [Range(0, 100)] float _jumpForce;
    [SerializeField] [Range(30, 100)] float _jumpForceWithTrampoline;
    [SerializeField] [Range(10, 100)] float _thrownRocksSpeed;
    [SerializeField] [Range(0.1f, 5)] float _shootingCooldown;
    [Tooltip("The pointer is the scope of potato that can throw rocks")]
    [SerializeField] [Range(5, 12)] float _pointerRotationSpeed = 7;

    [Header("---Controls---")]

    [SerializeField] [Range(0, 1)] float _jumpCommandMaxAnticipation;

    [Tooltip("Free camera will start to move only if pointer position overcome treshold")]
    [SerializeField] [Range(100, 600)] float _freeCameraMovingTreshold;
    [SerializeField] [Range(10, 70)] float _freeCameraSpeed;
    [SerializeField] [Range(35, 150)] float _lockedCameraSpeed;

    [Header("--- Other ---")]
    [SerializeField] [Range(1, 50)] float _botSpeed;
    [SerializeField] [Range(5, 50)] float _platformFallingSpeed;
    [SerializeField] [Range(1, 50)] float _movingPlatformSpeed;
    [SerializeField] [Range(0, 20)] float _cloudsMinSpeed;
    [SerializeField] [Range(0, 20)] float _cloudsMaxSpeed;


    #endregion

    #region static variables that copy inspector values

    public static float CharacterSpeed { get; private set; }
    public static float JumpForce { get; private set; }
    public static float JumpCommandMaxAnticipation { get; private set; }
    public static float JumpForceWithTrampoline { get; private set; }
    public static float ThrownRocksSpeed { get; private set; }
    public static float ShootingCooldown { get; private set; }
    public static float PointerRotationSpeed { get; private set; }

    public static float FreeCameraMovingTreshold { get; private set; }
    public static float FreeCameraSpeed { get; private set; }
    public static float LockedCameraSpeed { get; private set; }

    public static float BotSpeed { get; private set; }
    public static float PlatformFallingSpeed { get; private set; }
    public static float MovingPlatformSpeed { get; private set; }
    public static float CloudsMinSpeed { get; private set; }
    public static float CloudsMaxSpeed { get; private set; }


    void OnValidate()
    {
        InitializeVariables();
    }

    private void Awake()
    {
        InitializeVariables();
    }

    void InitializeVariables()
    {
        CharacterSpeed = _characterSpeed;
        JumpForce = _jumpForce;
        JumpCommandMaxAnticipation = _jumpCommandMaxAnticipation;
        JumpForceWithTrampoline = _jumpForceWithTrampoline;
        ThrownRocksSpeed = _thrownRocksSpeed;
        ShootingCooldown = _shootingCooldown;
        PointerRotationSpeed = _pointerRotationSpeed;

        BotSpeed = _botSpeed;

        PlatformFallingSpeed = _platformFallingSpeed;

        LockedCameraSpeed = _lockedCameraSpeed;
        FreeCameraSpeed = _freeCameraSpeed;
        FreeCameraMovingTreshold = _freeCameraMovingTreshold;
        MovingPlatformSpeed = _movingPlatformSpeed;
        CloudsMaxSpeed = _cloudsMaxSpeed;
        CloudsMinSpeed = _cloudsMinSpeed;
    }
    #endregion

    #region Global variables

    static public ProfileData activeProfile;

    static public bool isGamePaused { get; private set; } = false; 
    static public Phases currentPhase { get; private set; } = Phases.preparation;

    static public uint actualTick = 0;

    static public byte maxSaveFiles = 3;

    //used during tutorial
    static public bool canPlayerPlay { get; private set; } = true;
    static public bool canPlayerManuallySwitchPhase { get; private set; } = true;
    static public bool playerCanOnlyChooseCharacters { get; private set; } = false;
    #endregion

    static public void SetCurrentPhase(Phases value)
    {
        currentPhase = value;
        FindObjectOfType<InputManager>().EnableOrDisableCommands();
    }

    static public void SetIsGamePaused(bool value)
    {
        isGamePaused = value;
        FindObjectOfType<InputManager>().EnableOrDisableCommands();
    }

    static public void SetCanPlayerPlay(bool value)
    {
        canPlayerPlay = value;
        FindObjectOfType<InputManager>().EnableOrDisableCommands();
    }

    static public void SetCanPlayerManuallySwitchPhase(bool value)
    {
        canPlayerManuallySwitchPhase = value;
        FindObjectOfType<InputManager>().EnableOrDisableCommands();
    }

    static public void SetPlayerCanOnlyChooseCharacters(bool value)
    {
        playerCanOnlyChooseCharacters = value;
        FindObjectOfType<InputManager>().EnableOrDisableCommands();
    }

    static public void OnChangingScene()
    {
        isGamePaused = false;
        Time.timeScale = 1;
        currentPhase = Phases.preparation;
        actualTick = 0;
        canPlayerPlay = true;
        canPlayerManuallySwitchPhase = true;
        playerCanOnlyChooseCharacters = false;
       
        InputManager inputManager =  FindObjectOfType<InputManager>();
        if(inputManager != null)
        {
            inputManager.EnableOrDisableCommands();
        }
    }

    void FixedUpdate()
    {
        if (!isGamePaused && currentPhase == Phases.recording)
        {
            actualTick++;
        }
    }
}