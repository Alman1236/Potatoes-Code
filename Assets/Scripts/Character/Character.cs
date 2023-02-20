using UnityEngine;

public class Character : MonoBehaviour,IResettable
{
    [HideInInspector] public byte indexInLevel;
    public GameObject hilightingEffect;

    public JumpManager jumpManager { get; private set; }
    public ShootingManager shootingManager { get; private set; }
    public MovementManager movement { get; private set; }
    public ActionsRecorder actionsRecorder { get; private set; }
    public ActionsPlayer actionsPlayer { get; private set; }
    public CharacterDeath deathManager { get; private set; }
    public AnimationsManager animationsManager { get; private set; }
    public CharacterAudioManager audioManager { get; private set; }
    public CharactersLinker linker { get; private set; }
    public FreezingManager freezingManager { get; private set; }

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        jumpManager = GetComponent<JumpManager>();
        shootingManager = GetComponent<ShootingManager>();
        movement = GetComponent<MovementManager>();
        actionsRecorder = GetComponent<ActionsRecorder>();
        actionsPlayer = GetComponent<ActionsPlayer>();
        deathManager = GetComponent<CharacterDeath>();
        animationsManager = GetComponent<AnimationsManager>();
        audioManager = GetComponent<CharacterAudioManager>();
        linker = GetComponent<CharactersLinker>();
        freezingManager = GetComponent<FreezingManager>();
    }

    void IResettable.Reset()
    {
        deathManager.OnResettingCharacter();
        actionsRecorder.OnResettingCharacter();
        freezingManager.OnResettingCharacter();
        animationsManager.OnResettingCharacter();
        shootingManager.OnResettingCharacter();
        jumpManager.OnResettingCharacter();
        movement.OnResettingCharacter();
        hilightingEffect.SetActive(false);
    }
}
