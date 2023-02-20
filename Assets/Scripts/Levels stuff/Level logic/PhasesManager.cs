using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;
using EnumsAndStructs;

public class PhasesManager : SerializedMonoBehaviour
{
    CharacterSelector characterSelector;
    LevelData levelData;

    [SerializeField] IObserver[] phasesManagerObservers = new IObserver[0];
    [SerializeField] GameObject transition;
    [SerializeField] GameObject btnClearAllRecordings;

    static public PhasesManager instance;

    bool wasLevel2TutorialShown = false;

    #region Public methods

    public void OnSendingCommandToCharacter()
    {
        if (Data.currentPhase == Phases.preparation)
        {
            StartRecordingPhase();
        }
    }

    public void OnPressingSwitchPhase()
    {
        if (Data.canPlayerManuallySwitchPhase)
        {
            SwitchPhase();
        }
    }

    public void SwitchPhase()
    {
        if (Data.currentPhase == Phases.recording)
        {
            Timer.instance.StopTimer();
            StartCoroutine(StartResettingPhase());
        }
        else if (Data.currentPhase == Phases.preparation)
        {
            StartRecordingPhase();
        }
    }

    public void OnTimeLeftEnd()
    {
        if (Data.currentPhase == Phases.recording)
        {
            StartCoroutine(StartResettingPhase());
        }
    }

    public void OnWinning()
    {
        Data.SetCurrentPhase(Phases.win);
    }
    #endregion

    #region Private methods

    void Awake()
    {
        GenerateInstance();
    }

    void Start()
    {
        Initialize();
        Data.SetCurrentPhase(Phases.preparation);

    }

    void GenerateInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("There should be only one PhasesManager.cs in every scene! ", gameObject);
            Debug.Break();
        }
    }

    void Initialize()
    {
        characterSelector = CharacterSelector.instance;
        levelData = LevelData.instance;
    }

    IEnumerator StartResettingPhase()
    {
        Data.actualTick = 0;

        transition.SetActive(true);
        Data.SetCurrentPhase(Phases.resetting);
        AudioManager.instance.PlaySound(AudioClipsNames.levelReset);
        NotifyObservers();
        levelData.ResetLevel();
        ResetEveryObjectInLevel();

        yield return new WaitForSeconds(2);

        transition.SetActive(false);
        btnClearAllRecordings.SetActive(true);
        StartPreparationPhase();
    }

    void StartRecordingPhase()
    {
        characterSelector.OnStartingRecordingPhase();
        MakeEveryNotControlledCharacterStartPlaying();
        Data.SetCurrentPhase(Phases.recording);
        Timer.instance.StartTimer();

        NotifyObservers();
    }

    void StartPreparationPhase()
    {
        //if is level 2
        if (!wasLevel2TutorialShown && UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 3)
        {
            wasLevel2TutorialShown = true;
            FindObjectOfType<TutorialManager>().ShowMessage(GameTexts.GetTranslatedText(textsNames.tutorialMessage_ButtonsInTheTopRightCorner));
        }

        Data.SetCurrentPhase(Phases.preparation);
        NotifyObservers();
        characterSelector.OnStartingPreparationPhase();
    }

    void MakeEveryNotControlledCharacterStartPlaying()
    {
        for (byte i = 0; i < levelData.characters.Length; i++)
        {
            if (characterSelector.selectedCharacter.indexInLevel != levelData.characters[i].indexInLevel)
            {
                levelData.characters[i].actionsPlayer.StartPlaying();
            }
        }
    }

    void ResetEveryObjectInLevel()
    {
        for (byte i = 0; i < levelData.characters.Length; i++)
        {
            levelData.characters[i].actionsPlayer.StopPlaying();
        }
    }

    void NotifyObservers()
    {
        for (byte i = 0; i < phasesManagerObservers.Length; i++)
        {
            phasesManagerObservers[i].Notify();
        }
    }

    #endregion
}
