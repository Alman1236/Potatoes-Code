using UnityEngine;
using EnumsAndStructs;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, IObserver
{
    CharacterSelector characterSelector;
    Camera mainCam;
    CameraBehaviour cameraBehaviour;
    PauseManager pauseManager;
    TutorialManager tutorialManager;
    ButtonShowMap btnShowMap;

    [SerializeField] bool thisLevelHasTutorial;
    bool canExploreLevel = false;

    InputActions inputActions;
    InputAction axis;
    InputAction removeMe;

    private void Awake()
    {
        ui = FindObjectOfType<InGameUIManager>().gameObject;

        inputActions = new InputActions();
        axis = inputActions.Gamecommands.Axis;
        axis.Enable();

        IntializeTutorialCommands();
        InitializeCharacterCommands();
        InitializeCharacterSelectorCommands();
        InitializeGameCommands();

        //inputActions.Gamecommands.HideUiREMOVEME.performed += TurnOnOffUI;
        //inputActions.Gamecommands.HideUiREMOVEME.Enable();
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        characterSelector = CharacterSelector.instance;
        mainCam = Camera.main;
        cameraBehaviour = mainCam.GetComponent<CameraBehaviour>();
        pauseManager = FindObjectOfType<PauseManager>();
        tutorialManager = FindObjectOfType<TutorialManager>();

        btnShowMap = FindObjectOfType<ButtonShowMap>();
        if (btnShowMap != null)
        {
            canExploreLevel = true;
            inputActions.Gamecommands.ShowMap.performed += ShowMap;
            inputActions.Gamecommands.ShowMap.Enable();
        }

        if (pauseManager == null)
        {
            Debug.LogError("Pause manager not found!", gameObject);
        }
    }

    void IntializeTutorialCommands()
    {
        if (thisLevelHasTutorial)
        {
            inputActions.Gamecommands.Stoptutorial.performed += StopTutorial;
            inputActions.Gamecommands.Stoptutorial.Enable();

            inputActions.Gamecommands.Skiptutorialmessage.performed += SkipTutorialMessage;
            inputActions.Gamecommands.Skiptutorialmessage.Enable();
        }
    }

    void InitializeCharacterCommands()
    {
        inputActions.Player.Jump.performed += MakeCharacterJump;
        inputActions.Player.Jump.Enable();

        inputActions.Player.Interact.performed += MakeCharacterInteract;
        inputActions.Player.Interact.Enable();

        inputActions.Gamecommands.Switchphase.performed += SwitchPhase;
        inputActions.Gamecommands.Switchphase.Enable();
    }

    void InitializeGameCommands()
    {
        inputActions.Gamecommands.PauseorResumegame.performed += PauseOrResumeGame;
        inputActions.Gamecommands.PauseorResumegame.Enable();

        inputActions.Gamecommands.ClearSelectedCharacter.performed += ClearSelectedCharacter;

        inputActions.Gamecommands.ClearAllCharacters.performed += ClearAllCharacters;

        inputActions.Gamecommands.PlayNext.performed += PlayNextLevel;
        inputActions.Gamecommands.PlayNext.Enable();
    }

    void InitializeCharacterSelectorCommands()
    {
        inputActions.Gamecommands.Switchcharacter.performed += SwitchCharacter;
        inputActions.Gamecommands.Switchcharacter.Enable();

        inputActions.Gamecommands.Selectcharacter.performed += SelectCharacterUnderPointer;
        inputActions.Gamecommands.Selectcharacter.Enable();
    }

    void AnyKeyPressed(InputAction.CallbackContext context)
    {
        Debug.Log(context.control.device);
    }

    GameObject ui;
    void TurnOnOffUI(InputAction.CallbackContext context)
    {
        ui.SetActive(!ui.activeSelf);
    }

    void StopTutorial(InputAction.CallbackContext context)
    {
        tutorialManager.StopTutorial();
    }

    void SkipTutorialMessage(InputAction.CallbackContext context)
    {
        tutorialManager.SkipTutorialMessage();
    }

    void PauseOrResumeGame(InputAction.CallbackContext context)
    {
        pauseManager.PauseOrResumeGame();
    }

    void PlayNextLevel(InputAction.CallbackContext context)
    {
        if (Data.currentPhase == Phases.win)
        {
            ScenesLoader.instance.LoadNextLevel();
        }
    }

    void ClearSelectedCharacter(InputAction.CallbackContext context)
    {
        characterSelector.selectedCharacter.actionsRecorder.ClearRecordedActions();

        InGameUIManager ui = FindObjectOfType<InGameUIManager>();
        if (ui != null)
        {
            ui.TurnOffBtnClearSelectedCharacter();
            ui.TryToTurnOffClearAllButton();
        }

        inputActions.Gamecommands.ClearSelectedCharacter.Disable();
    }

    void ClearAllCharacters(InputAction.CallbackContext context)
    {
        for (byte i = 0; i < LevelData.instance.characters.Length; i++)
        {
            LevelData.instance.characters[i].actionsRecorder.ClearRecordedActions();
        }

        InGameUIManager ui = FindObjectOfType<InGameUIManager>();
        if (ui != null)
        {
            ui.TryToTurnOffClearAllButton();
            ui.TurnOffBtnClearSelectedCharacter();
        }

        inputActions.Gamecommands.ClearAllCharacters.Disable();
    }

    void ShowMap(InputAction.CallbackContext context)
    {
        if (canExploreLevel)
        {
            btnShowMap.ShowOrUnshowMap();
        }
    }


    void MakeCharacterJump(InputAction.CallbackContext context)
    {
        characterSelector.selectedCharacter.jumpManager.TryToJump();
    }

    void MakeCharacterInteract(InputAction.CallbackContext context)
    {
        characterSelector.selectedCharacter.shootingManager.TryToShoot();
        characterSelector.selectedCharacter.freezingManager.FreezeOrUnfreeze();
    }

    void SwitchPhase(InputAction.CallbackContext context)
    {
        PhasesManager.instance.OnPressingSwitchPhase();
    }

    void SwitchCharacter(InputAction.CallbackContext context)
    {
        characterSelector.SelectNextCharacter();
    }

    void SelectCharacterUnderPointer(InputAction.CallbackContext context)
    {
        GameObject clickedCharacter = DetectWhatImClicking();

        if (clickedCharacter != null && clickedCharacter.TryGetComponent(out Character character))
        {
            characterSelector.OnClickCharacter(character.indexInLevel);
        }
    }

    void Update()
    {
        if (Data.currentPhase == Phases.exploringMap)
        {
            cameraBehaviour.MoveFreeCamera(axis.ReadValue<Vector2>());
        }
        else if (!Data.isGamePaused && Data.canPlayerPlay && CanPlayerControlCharacter())
        {
            characterSelector.selectedCharacter.movement.UpdateMovement(axis.ReadValue<Vector2>().x);
            //characterSelector.selectedCharacter.shootingManager.RotatePointer(removeMe.ReadValue<Vector2>().x);
        }

    }

    public void OnSelectCharacter()
    {
        if (characterSelector.selectedCharacter.actionsRecorder.hasRecordedBehaviour)
        {
            inputActions.Gamecommands.ClearSelectedCharacter.Enable();
        }
        else
        {
            if (inputActions.Gamecommands.ClearSelectedCharacter.enabled)
            {
                inputActions.Gamecommands.ClearSelectedCharacter.Disable();
            }
        }
    }

    public void EnableOrDisableCommands()
    {
        if (!Data.isGamePaused && Data.canPlayerPlay && CanPlayerControlCharacter())
        {
            inputActions.Player.Jump.Enable();
            inputActions.Player.Interact.Enable();

            if (Data.canPlayerManuallySwitchPhase)
            {
                inputActions.Gamecommands.Switchphase.Enable();
            }
        }
        else
        {
            inputActions.Player.Jump.Disable();
            inputActions.Player.Interact.Disable();
            inputActions.Gamecommands.Switchphase.Disable();
            inputActions.Gamecommands.ClearAllCharacters.Disable();
            inputActions.Gamecommands.ClearSelectedCharacter.Disable();
            inputActions.Gamecommands.ShowMap.Disable();
        }

        if (thisLevelHasTutorial)
        {
            if (!Data.isGamePaused)
            {
                inputActions.Gamecommands.Stoptutorial.Enable();
                inputActions.Gamecommands.Skiptutorialmessage.Enable();
            }
            else
            {
                inputActions.Gamecommands.Stoptutorial.Disable();
                inputActions.Gamecommands.Skiptutorialmessage.Disable();
            }

        }

        if (Data.currentPhase == Phases.preparation && !Data.isGamePaused && Data.canPlayerPlay)
        {
            inputActions.Gamecommands.Switchcharacter.Enable();
            inputActions.Gamecommands.Selectcharacter.Enable();

            if (DoesACharacterHaveRecordedBehaviour())
            {
                inputActions.Gamecommands.ClearAllCharacters.Enable();
            }

            if (characterSelector != null && characterSelector.selectedCharacter != null && characterSelector.selectedCharacter.actionsRecorder.hasRecordedBehaviour)
            {
                inputActions.Gamecommands.ClearSelectedCharacter.Enable();
            }

            if (canExploreLevel)
            {
                inputActions.Gamecommands.ShowMap.Enable();
            }
        }
        else
        {
            inputActions.Gamecommands.Switchcharacter.Disable();
            inputActions.Gamecommands.Selectcharacter.Disable();
            inputActions.Gamecommands.ClearAllCharacters.Disable();
            inputActions.Gamecommands.ClearSelectedCharacter.Disable();
            inputActions.Gamecommands.ShowMap.Disable();
        }

        if (Data.currentPhase == Phases.exploringMap && !Data.isGamePaused)
        {
            inputActions.Gamecommands.ShowMap.Enable();
        }
    }

    void IObserver.Notify()
    {
        if (Data.currentPhase == Phases.recording)
        {
            inputActions.Gamecommands.ClearAllCharacters.Disable();
            inputActions.Gamecommands.ClearSelectedCharacter.Disable();
        }
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    bool CanPlayerControlCharacter()
    {
        if (!Data.playerCanOnlyChooseCharacters && (Data.currentPhase == Phases.preparation || Data.currentPhase == Phases.recording))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    GameObject DetectWhatImClicking()
    {
        if (isPointerOverUI() == false)
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            RaycastHit2D hit = Physics2D.Raycast(mainCam.ScreenToWorldPoint(mousePos), Vector2.zero);

            if (hit.transform != null)
            {
                return hit.transform.gameObject;
            }

        }

        return null;
    }

    bool isPointerOverUI()
    {
        return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }

    bool DoesACharacterHaveRecordedBehaviour()
    {
        byte i = 0;
        bool doesACharacterHaveRecordedBehaviour = false;

        do
        {
            if (LevelData.instance.characters[i].actionsRecorder.hasRecordedBehaviour == true)
            {
                doesACharacterHaveRecordedBehaviour = true;
            }

            i++;
        } while (i < LevelData.instance.characters.Length && doesACharacterHaveRecordedBehaviour == false);

        return doesACharacterHaveRecordedBehaviour;
    }
}
