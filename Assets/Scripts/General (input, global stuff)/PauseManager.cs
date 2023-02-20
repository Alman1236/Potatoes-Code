using UnityEngine;
using EnumsAndStructs;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pnlPause;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] Button btnSkipLevel;
    [SerializeField] GameObject imgLockOnSkipLevel;

    public void OnClickResume()
    {
        AudioManager.instance.PlaySound(AudioClipsNames.buttonClick);
        ResumeGame();
    }

    public void OnClickOptions()
    {
        AudioManager.instance.PlaySound(AudioClipsNames.buttonClick);
        optionsMenu.SetActive(true);
    }

    public void OnClickBackFromOptions()
    {
        AudioManager.instance.PlaySound(AudioClipsNames.buttonClick);
        optionsMenu.SetActive(false);
    }

    public void OnClickMainMenu()
    {
        AudioManager.instance.PlaySound(AudioClipsNames.buttonClick);
        ScenesLoader.instance.LoadMainMenu();

        Data.activeProfile.UpdateTimePlayed(Time.timeSinceLevelLoad);
    }

    public void PauseOrResumeGame()
    {
        if (Data.isGamePaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();   
        }

    }

    public void OnClickSkipLevel()
    {
        ScenesLoader.instance.LoadNextLevel();
        Data.activeProfile.UpdateTimePlayed(Time.timeSinceLevelLoad);
    }

    void UpdateSkipLevelButton()
    {
        byte whichLevelIsThis = (byte)(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex - 1);

        if(whichLevelIsThis != 49 && Data.activeProfile.CanPlayLevel((byte)(whichLevelIsThis + 1)))
        {
            btnSkipLevel.interactable = true;
            btnSkipLevel.image.color = Color.white;
            imgLockOnSkipLevel.SetActive(false);

        }
        else
        {
            btnSkipLevel.interactable = false;
            btnSkipLevel.image.color = new Color(0.66f,0.66f,0.66f);
            imgLockOnSkipLevel.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        Data.SetIsGamePaused(false);
        Time.timeScale = 1;

        pnlPause.SetActive(false);
        optionsMenu.SetActive(false);
    }

    void PauseGame()
    {
        Data.SetIsGamePaused(true);
        Time.timeScale = 0;

        pnlPause.SetActive(true);
        UpdateSkipLevelButton();
    }
}
