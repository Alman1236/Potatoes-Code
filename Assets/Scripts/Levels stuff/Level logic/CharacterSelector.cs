using UnityEngine;
using EnumsAndStructs;

public class CharacterSelector : MonoBehaviour
{
    LevelData level;
    public Character selectedCharacter { get; private set; }
    byte selectedCharacterIndex = 0;
    CameraBehaviour cameraBehaviour;

    InGameUIManager inGameUI;
    TutorialManager tutorial;
    [SerializeField] GameObject selectedCharacterPointer;
    [SerializeField] GameObject btnClearSelectedCharacterRecording;

    static public CharacterSelector instance;

    void Awake()
    {
        inGameUI = FindObjectOfType<InGameUIManager>();
        tutorial = FindObjectOfType<TutorialManager>();
        GenerateInstance();
    }

    void Start()
    {
        Initialize();
    }

    void GenerateInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("There should be only one CharacterSelector.cs in every scene! ", gameObject);
            Debug.Break();
        }
    }

    void Initialize()
    {
        level = LevelData.instance;
        cameraBehaviour = Camera.main.GetComponent<CameraBehaviour>();
        SelectNextCharacter();
    }

    public void SelectNextCharacter()
    {

        if (isSelectedLastCharacter())
        {
            SelectCharacter(index: 0);
        }
        else
        {
            SelectCharacter(index: (byte)(selectedCharacterIndex + 1));
        }
    }

    public void ReselectCharacter()
    { 
        SelectCharacter(selectedCharacterIndex);
    }

    public void OnClickCharacter(byte index)
    {
        SelectCharacter(index);
    }

    void SelectCharacter(byte index)
    {
        MovePointer(characterPos: level.characters[index].transform.position);
        cameraBehaviour.SetFollowedCharacter(level.characters[index].transform);

        selectedCharacter = level.characters[index];
        selectedCharacterIndex = index;
        level.characters[index].animationsManager.DoAnimation(Animations.selected);
        AudioManager.instance.PlaySound(AudioClipsNames.characterSelection);

        inGameUI.OnSelectingCharacter(index);
        tutorial.OnSelectingCharacter();

        TryToTurnOnButtonClearCharacterBehaviour();
    }

    void TryToTurnOnButtonClearCharacterBehaviour()
    {
        if (selectedCharacter.actionsRecorder.hasRecordedBehaviour)
        {
            btnClearSelectedCharacterRecording.SetActive(true);
            InputManager input = FindObjectOfType<InputManager>();

            if (input != null)
                input.OnSelectCharacter();
        }
        else
        {
            btnClearSelectedCharacterRecording.SetActive(false);
        }
    }

    void MovePointer(Vector3 characterPos)
    {
        selectedCharacterPointer.transform.position = characterPos + new Vector3(0, 2, 0);
    }

    bool isSelectedLastCharacter()
    {
        if (selectedCharacterIndex == level.characters.Length - 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OnStartingRecordingPhase()
    {
        selectedCharacter.GetComponent<Rigidbody2D>().gravityScale = 10;
        selectedCharacter.actionsRecorder.ClearRecordedActions();
        selectedCharacter.hilightingEffect.SetActive(true);

        if (selectedCharacter.linker.isLinkedToAnotherCharacter)
        {
            if (selectedCharacter.linker.linkedCharacter.actionsRecorder.hasRecordedBehaviour)
            {
                selectedCharacter.linker.linkedCharacter.actionsRecorder.ClearRecordedActions();
            }
        }

        selectedCharacterPointer.SetActive(false);
    }

    public void OnStartingPreparationPhase()
    {
        selectedCharacterPointer.SetActive(true);
        SelectCharacter(selectedCharacterIndex);
    }
}
