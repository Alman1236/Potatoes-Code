using UnityEngine;
using UnityEngine.UI;
using EnumsAndStructs;
using TMPro;
using UnityEngine.SceneManagement;

public class InGameUIManager : MonoBehaviour, IObserver
{
    [SerializeField] GameObject pnlInfo;
    [SerializeField] GameObject tmpRToStart, tmpRToReset;

    [Header("----Phase image----")]
    [SerializeField] Image imgPhase;

    [SerializeField] Sprite imgPreparationPhase, imgResettingPhase, imgRecordingPhase;

    [Header("----Phase text----")]
    [SerializeField] TextMeshProUGUI tmpPhase;
    [SerializeField] Color tmpPhaseColorDuringPreparationPhase = new Color(1, 0.97f, 0.71f);
    [SerializeField] Color tmpPhaseColorDuringRewindPhase = new Color(1, 1, 1);
    [SerializeField] Color tmpPhaseColorDuringRecordingPhase = new Color(0.38f, 1, 0.27f);


    [Header("----Buttons----")]
    [SerializeField] GameObject btnClearAllRecordings;
    [SerializeField] GameObject btnClearSelectedCharacterRecording;
    [SerializeField] GameObject showMapButton;
    [SerializeField] Button btnSkipLevel;

    void Start()
    {
        tmpPhase.text = GameTexts.GetTranslatedText(textsNames.inGameUi_preparationPhase);

        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            tmpRToStart.SetActive(true);
        }
    }

    public void OnClickBtnClearAllRecordings()
    {
        AudioManager.instance.PlaySound(AudioClipsNames.buttonClick);

        pnlInfo.SetActive(false);

        for (byte i = 0; i < LevelData.instance.characters.Length; i++)
        {
            LevelData.instance.characters[i].actionsRecorder.ClearRecordedActions();
        }

        btnClearAllRecordings.SetActive(false);
        btnClearSelectedCharacterRecording.SetActive(false);

        pnlInfo.SetActive(false);
    }

    public void OnClickBtnClearRecording()
    {
        AudioManager.instance.PlaySound(AudioClipsNames.buttonClick);

        pnlInfo.SetActive(false);
        CharacterSelector.instance.selectedCharacter.actionsRecorder.ClearRecordedActions();

        TryToTurnOffClearAllButton();

        TurnOffBtnClearSelectedCharacter();

        pnlInfo.SetActive(false);
    }

    public void OnPointerEnterBtnClearRecording()
    {
        pnlInfo.SetActive(true);
        OnPointerEnterButton();
        pnlInfo.GetComponentInChildren<TextMeshProUGUI>().text = GameTexts.GetTranslatedText(textsNames.pnlInfo_clearSelectedCharacterRecording);
    }

    public void OnPointerEnterBtnClearAllRecordings()
    {
        pnlInfo.SetActive(true);
        pnlInfo.GetComponentInChildren<TextMeshProUGUI>().text = GameTexts.GetTranslatedText(textsNames.pnlInfo_clearAllRecordings);
        OnPointerEnterButton();
    }

    public void OnPointerEnterButton()
    {
        AudioManager.instance.PlaySound(AudioClipsNames.mouseOverButton);
    }

    public void OnPointerEnterSkipLevel()
    {
        if (btnSkipLevel.interactable)
        {
            AudioManager.instance.PlaySound(AudioClipsNames.mouseOverButton);
        }
    }

    public void OnPointerExitButton()
    {
        pnlInfo.SetActive(false);
    }

    public void TryToTurnOffClearAllButton()
    {
        bool oneCharacterHasRecordedBehaviour = false;
        byte i = 0;

        do
        {
            if (LevelData.instance.characters[i].actionsRecorder.hasRecordedBehaviour)
            {
                oneCharacterHasRecordedBehaviour = true;
                Debug.Log("charcter: " + LevelData.instance.characters[i].gameObject.name + " has recorded behaviour");
            }

            i++;
        } while (i < LevelData.instance.characters.Length && !oneCharacterHasRecordedBehaviour);

        if (!oneCharacterHasRecordedBehaviour)
        {
            btnClearAllRecordings.SetActive(false);
        }
    }

    public void OnSelectingCharacter(byte selectedCharacterIndex)
    {

    }

    public void OnSelectedCharacterDeath()
    {
        tmpRToReset.SetActive(true);
    }

    void IObserver.Notify()
    {
        if (Data.currentPhase == Phases.preparation)
        {
            if (!showMapButton.GetComponent<ButtonShowMap>().HideBtnShowMap)
            {
                showMapButton.SetActive(true);
            }

            tmpRToReset.SetActive(false);

            if (SceneManager.GetActiveScene().buildIndex != 1)
            {
                tmpRToStart.SetActive(true);
            }

            tmpPhase.text = GameTexts.GetTranslatedText(textsNames.inGameUi_preparationPhase);
            tmpPhase.color = tmpPhaseColorDuringPreparationPhase;
        }
        else if (Data.currentPhase == Phases.recording)
        {
            pnlInfo.SetActive(false);

            btnClearAllRecordings.SetActive(false);
            btnClearSelectedCharacterRecording.SetActive(false);
            showMapButton.SetActive(false);

            tmpPhase.text = GameTexts.GetTranslatedText(textsNames.inGameUi_recordingPhase);
            tmpPhase.color = tmpPhaseColorDuringRecordingPhase;
            tmpRToStart.SetActive(false);

            tmpPhase.GetComponent<Animator>().SetBool("isPulsing", true);
        }
        else
        {
            tmpPhase.text = GameTexts.GetTranslatedText(textsNames.inGameUi_resetting);
            tmpPhase.color = tmpPhaseColorDuringRewindPhase;
            tmpRToReset.SetActive(false);

            tmpPhase.GetComponent<Animator>().SetBool("isPulsing", false);
        }

        UpdateImgPhase();
    }

    public void OnChangingLanguage()
    {
        if (Data.currentPhase == Phases.resetting)
        {
            tmpPhase.text = GameTexts.GetTranslatedText(textsNames.inGameUi_resetting);
        }
        else if (Data.currentPhase == Phases.recording)
        {
            tmpPhase.text = GameTexts.GetTranslatedText(textsNames.inGameUi_recordingPhase);
        }
        else
        {
            tmpPhase.text = GameTexts.GetTranslatedText(textsNames.inGameUi_preparationPhase);
        }
    }

    void UpdateImgPhase()
    {
        if (Data.currentPhase == Phases.preparation)
        {
            imgPhase.sprite = imgPreparationPhase;
        }
        else if (Data.currentPhase == Phases.resetting)
        {
            imgPhase.sprite = imgResettingPhase;
        }
        else if (Data.currentPhase == Phases.recording)
        {
            imgPhase.sprite = imgRecordingPhase;
        }
    }

    public void TurnOffBtnClearSelectedCharacter()
    {
        btnClearSelectedCharacterRecording.SetActive(false);
    }
}
