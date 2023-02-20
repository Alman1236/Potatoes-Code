using System.Collections;
using UnityEngine;
using TMPro;
using EnumsAndStructs;

public class TutorialManager : MonoBehaviour, IObserver
{
    [SerializeField] GameObject pnlTutorial;
    [SerializeField] TextMeshProUGUI tmpTutorial, tmpTutorialCommands;
    [SerializeField] GameObject tmpSkip;

    textsNames lastShownMessage;
    bool canPlayerSkip = false;

    [SerializeField] bool doFirstTutorialThisLevel = false;
    [SerializeField] GameObject selectACharacterToContinueText;

    bool displayedRecordingPhaseMessage = false;
    bool displayedSecondPreparationPhaseMessage = false;
    bool isTutorialStopped = false;

    private void Start()
    {
        if (doFirstTutorialThisLevel)
        {
            Data.SetCanPlayerManuallySwitchPhase(false);
            Data.SetPlayerCanOnlyChooseCharacters(true);

            lastShownMessage = textsNames.tutorialMessage_Welcome;
            StartCoroutine(ShowTutorialMessageAfterDelay(2, GameTexts.GetTranslatedText(textsNames.tutorialMessage_Welcome)));
        }
        else
        {
            lastShownMessage = textsNames.mainMenu_btnPlay;
        }

        byte whichLevelIsThis = (byte)(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex - 1);

        if (whichLevelIsThis == 4)
        {
            StartCoroutine(ShowTutorialMessageAfterDelay(2, GameTexts.GetTranslatedText(textsNames.tutorialMessage_FreeCameraButton)));
        }
        else if(whichLevelIsThis == 17)
        {
            StartCoroutine(ShowTutorialMessageAfterDelay(2, GameTexts.GetTranslatedText(textsNames.tutorialMessage_StarCharacter)));
        }
        else if(whichLevelIsThis == 34)
        {
            StartCoroutine(ShowTutorialMessageAfterDelay(2, GameTexts.GetTranslatedText(textsNames.tutorialMessage_FreezingPotato)));
        }
        else if(whichLevelIsThis == 38)
        {
            StartCoroutine(ShowTutorialMessageAfterDelay(2, GameTexts.GetTranslatedText(textsNames.tutorialMessage_Shooting)));
        }
    }

    IEnumerator ShowTutorialMessageAfterDelay(float delay, string message)
    {
        Data.SetCanPlayerPlay(false);
        yield return new WaitForSecondsRealtime(delay);

        ShowMessage(message);
    }

    public void ShowMessage(string text)
    {
        if (!isTutorialStopped)
        {
            Data.SetCanPlayerPlay(false);

            canPlayerSkip = false;
            tmpSkip.SetActive(false);
            StartCoroutine(ResetCanPlayerSkip());

            tmpTutorial.text = text;
            tmpTutorialCommands.text = GameTexts.GetTranslatedText(textsNames.tutorialCommands_Skip);
            pnlTutorial.SetActive(true);
            AudioManager.instance.PlaySound(AudioClipsNames.tutorialMessage);
        }
    }

    IEnumerator ResetCanPlayerSkip()
    {
        yield return new WaitForSecondsRealtime(3.5f);
        canPlayerSkip = true;
        tmpSkip.SetActive(true);
    }

    public void SkipTutorialMessage()
    {
        if (canPlayerSkip)
        {
            switch (lastShownMessage)
            {
                case textsNames.tutorialMessage_RecordingOverwriting:
                    StopTutorial();
                    break;

                case textsNames.tutorialMessage_Welcome:
                    lastShownMessage = textsNames.tutorialMessage_PreparationPhase;
                    ShowMessage(GameTexts.GetTranslatedText(textsNames.tutorialMessage_PreparationPhase));
                    break;

                case textsNames.tutorialMessage_PreparationPhase:
                    lastShownMessage = textsNames.tutorialMessage_ChooseACharacter;
                    ShowMessage(GameTexts.GetTranslatedText(textsNames.tutorialMessage_ChooseACharacter));
                    break;

                case textsNames.tutorialMessage_SecondPreparationPhase:
                    lastShownMessage = textsNames.tutorialMessage_ReachTheStars;
                    ShowMessage(GameTexts.GetTranslatedText(textsNames.tutorialMessage_ReachTheStars));
                    break;

                //case textsNames.tutorialMessage_ReachTheStars:
                    //lastShownMessage = textsNames.tutorialMessage_RecordingOverwriting;
                    //ShowMessage(GameTexts.GetTranslatedText(textsNames.tutorialMessage_RecordingOverwriting));
                    //break;

                default:  
                    TurnOffPnlTutorial();
                    break;
            }    
        }
    }

    public void OnSelectingCharacter()
    {
        if (!isTutorialStopped && lastShownMessage == textsNames.tutorialMessage_ChooseACharacter)
        {
            canPlayerSkip = false;
            lastShownMessage = textsNames.tutorialMessage_RecordingPhase;
            StartCoroutine(ShowTutorialMessageAfterDelay(1, GameTexts.GetTranslatedText(textsNames.tutorialMessage_RecordingPhase)));
            StartCoroutine(ResetPlayerCanOnlyChooseCharacters(1));
            selectACharacterToContinueText.SetActive(false);
        }
    }

    IEnumerator ResetPlayerCanOnlyChooseCharacters(float delay)
    {
        yield return new WaitForSeconds(delay);
        Data.SetPlayerCanOnlyChooseCharacters(false);
    }

    void IObserver.Notify()
    {
        if(!isTutorialStopped)
        {
            if (Data.currentPhase == Phases.recording && displayedRecordingPhaseMessage == false)
            {
                canPlayerSkip = false;
                displayedRecordingPhaseMessage = true;
                lastShownMessage = textsNames.tutorialMessage_RecordingPhaseEnd;
                StartCoroutine(ShowRecordingPhaseEndTutorial());
            }
            else if (Data.currentPhase == Phases.preparation && displayedSecondPreparationPhaseMessage == false)
            {
                Data.SetCanPlayerPlay(false);
                displayedSecondPreparationPhaseMessage = true;
                lastShownMessage = textsNames.tutorialMessage_SecondPreparationPhase;
                ShowMessage(GameTexts.GetTranslatedText(textsNames.tutorialMessage_SecondPreparationPhase));
            }
        }
    }

    IEnumerator ShowRecordingPhaseEndTutorial()
    {
        yield return new WaitForSecondsRealtime(2.5f);

        Data.SetCanPlayerPlay(false);
        Time.timeScale = 0;
        Data.SetCanPlayerManuallySwitchPhase(true);
        ShowMessage(GameTexts.GetTranslatedText(textsNames.tutorialMessage_RecordingPhaseEnd));
    }

    void TurnOffPnlTutorial()
    {
        pnlTutorial.SetActive(false);
        Data.SetCanPlayerPlay(true);
        Time.timeScale = 1;

        if(lastShownMessage == textsNames.tutorialMessage_ChooseACharacter && !isTutorialStopped)
        {
            selectACharacterToContinueText.SetActive(true);
            selectACharacterToContinueText.GetComponent<TextMeshProUGUI>().text = GameTexts.GetTranslatedText(textsNames.tutorialMessage_ChooseACharacterToContinue);
        }

        StopAllCoroutines();
    }

    public void StopTutorial()
    {
        if(canPlayerSkip && pnlTutorial.activeSelf)
        {
            isTutorialStopped = true;
            Data.SetCanPlayerManuallySwitchPhase(true);
            Data.SetPlayerCanOnlyChooseCharacters(false);
            AudioManager.instance.PlaySound(AudioClipsNames.tutorialMessage);
            TurnOffPnlTutorial();

            if(selectACharacterToContinueText != null)
            {
                selectACharacterToContinueText.SetActive(false);
            }
        }
    }
}
