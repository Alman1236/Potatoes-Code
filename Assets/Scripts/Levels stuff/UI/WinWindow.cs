using UnityEngine;
using EnumsAndStructs;

public class WinWindow : MonoBehaviour
{
    public void OnEnable()
    {
        AudioManager.instance.PlaySound(AudioClipsNames.victory);
    }

    public void OnClickPlayNextLevel()
    {
        AudioManager.instance.PlaySound(AudioClipsNames.buttonClick);
        ScenesLoader.instance.LoadNextLevel();
    }

    public void OnClickBackToMenu()
    {
        AudioManager.instance.PlaySound(AudioClipsNames.buttonClick);
        ScenesLoader.instance.LoadMainMenu();
    }

    public void OnClickPlayAgain()
    {
        AudioManager.instance.PlaySound(AudioClipsNames.buttonClick);
        ScenesLoader.instance.ReloadLevel();
    }

    public void OnPointerEnterButton()
    {
        AudioManager.instance.PlaySound(AudioClipsNames.mouseOverButton);
    }
}
