using UnityEngine;
using EnumsAndStructs;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;


public class Options : MonoBehaviour
{
    static public Languages languageSet;
    [SerializeField] Translator translator;

    [SerializeField] TMP_Dropdown resolutionsDropdown, screenModeDropdown, framerateCapDropdown, languageDropdown;

    [HideInInspector] static public float effectsVolume = 0.5f;
    [SerializeField] Scrollbar effectsVolumeScrollBar;

    [HideInInspector] static public float musicVolume = 0.5f;
    [SerializeField] Scrollbar musicVolumeScrollBar;

    const string effectsVolumeSavedValue = "effects volume";
    const string musicVolumeSavedValue = "music volume";

    const string resolutionSavedValue = "selected resolution";
    const string screenModeSavedValue = "selected screenmode";
    const string languageSavedValue = "selected language";
    const string framerateCapSavedValue = "selected framerate";

    private void OnEnable()
    {
        SetResolutionOptions();

        LoadAndUpdateVolumeBars();
        LoadScreenOptions();

        UpdateShownResolution();

        SetLanguage();
        SetFramerateCap();
    }



    void LoadAndUpdateVolumeBars()
    {
        if (PlayerPrefs.HasKey(effectsVolumeSavedValue))
        {
            effectsVolume = PlayerPrefs.GetFloat(effectsVolumeSavedValue);
        }

        effectsVolumeScrollBar.value = effectsVolume;

        if (PlayerPrefs.HasKey(musicVolumeSavedValue))
        {
            musicVolume = PlayerPrefs.GetFloat(musicVolumeSavedValue);
        }

        musicVolumeScrollBar.value = musicVolume;
    }

    void LoadScreenOptions()
    {
        bool wantFullscreen = true;
        screenModeDropdown.value = 0;

        if (PlayerPrefs.HasKey(screenModeSavedValue) && PlayerPrefs.GetInt(screenModeSavedValue) != 0)
        {
            wantFullscreen = false;
            screenModeDropdown.value = 1;
        }

        if (PlayerPrefs.HasKey(resolutionSavedValue))
        {
            int resolutionIndex = PlayerPrefs.GetInt(resolutionSavedValue);

            if (resolutionIndex > resolutionsDropdown.options.Count)
            {
                Screen.SetResolution(1920, 1080, wantFullscreen);
            }
            else
            {
                SetResolution(resolutionIndex);
            }
        }
        else
        {
            Screen.SetResolution(1920, 1080, wantFullscreen);
        }
    }

    void SetResolutionOptions()
    {
        List<TMP_Dropdown.OptionData> resolutions = new List<TMP_Dropdown.OptionData>();

        for (byte i = 0; i < Screen.resolutions.Length; i++)
        {
            TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData(Screen.resolutions[i].ToString());


            resolutions.Add(optionData);

        }

        resolutionsDropdown.AddOptions(resolutions);
    }

    void UpdateShownResolution()
    {
        byte i = 0;
        bool flag = false;
        do
        {
            if (Screen.currentResolution.width == Screen.resolutions[i].width
              && Screen.currentResolution.height == Screen.resolutions[i].height)
            {
                flag = true;
                resolutionsDropdown.value = i;
            }

            i++;
        } while (i < resolutionsDropdown.options.Count && !flag);
    }

    public void OnChangingLanguage(int value)
    {
        //IMPORTANT: this works as long the options in the language dropdown are in the same of order of the enumerator values

        if (languageSet != (Languages)value)
        {
            languageSet = (Languages)value;
            translator.OnChangingLanguage();
            PlayerPrefs.SetInt(languageSavedValue, value);
            PlayerPrefs.Save();

            if (languageDropdown.value != value)
            {
                languageDropdown.value = value;
            }

            ChangeDropdownsTranslations();
            UpdateIngameUiTranslation();
        }
    }

    void UpdateIngameUiTranslation()
    {
        InGameUIManager ui = FindObjectOfType<InGameUIManager>();
        
        if (ui != null)
        {
            ui.OnChangingLanguage();
        }
    }

    void ChangeDropdownsTranslations()
    {
        framerateCapDropdown.options[0].text = GameTexts.GetTranslatedText(textsNames.options_uncappedFramerate);
        screenModeDropdown.options[0].text = GameTexts.GetTranslatedText(textsNames.options_fullScreenMode);
        screenModeDropdown.options[1].text = GameTexts.GetTranslatedText(textsNames.options_windowedMode);

        if (PlayerPrefs.HasKey(screenModeSavedValue))
        {
            SetScreenMode(PlayerPrefs.GetInt(screenModeSavedValue));
        }
        else
        {
            SetScreenMode(0);
        }

        SetFramerateCap();
    }

    public void SetLanguage()
    {
        if (PlayerPrefs.HasKey(languageSavedValue))
        {
            OnChangingLanguage(PlayerPrefs.GetInt(languageSavedValue));
        }
        else if (Application.systemLanguage != SystemLanguage.English) //english is set by default
        {
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Italian:
                    OnChangingLanguage((int)Languages.italian);
                    break;
            }
        }
    }

    public void SetResolution(int value)
    {
        if (screenModeDropdown.value == 0)
        {
            Screen.SetResolution(Screen.resolutions[value].width, Screen.resolutions[value].height, true);
        }
        else
        {
            Screen.SetResolution(Screen.resolutions[value].width, Screen.resolutions[value].height, false);
        }

        PlayerPrefs.SetInt(resolutionSavedValue, value);
        PlayerPrefs.Save();
    }

    public void SetScreenMode(int value)
    {
        if (value == 0)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }

        screenModeDropdown.captionText.text = screenModeDropdown.options[value].text;
        PlayerPrefs.SetInt(screenModeSavedValue, value);
        PlayerPrefs.Save();
    }

    void SetFramerateCap()
    {
        if (PlayerPrefs.HasKey(framerateCapSavedValue))
        {
            ChangeFramerateCap(PlayerPrefs.GetInt(framerateCapSavedValue));
        }
        else
        {
            ChangeFramerateCap(3);
        }
    }

    public void ChangeFramerateCap(int value)
    {
        if (value == 0)
        {
            Application.targetFrameRate = 30;
        }
        else if (value == 1)
        {
            Application.targetFrameRate = 60;
        }
        else if (value == 2)
        {
            Application.targetFrameRate = 144;
        }
        else if (value == 3)
        {
            Application.targetFrameRate = 300;
        }
        else
        {
            Debug.LogError("invalid code");
        }

        framerateCapDropdown.value = value;
        framerateCapDropdown.captionText.text = framerateCapDropdown.options[value].text;
        PlayerPrefs.SetInt(framerateCapSavedValue, value);
        PlayerPrefs.Save();
    }

    public void ChangeEffectsVolume(float value)
    {
        effectsVolume = value;
        PlayerPrefs.SetFloat(effectsVolumeSavedValue, value);
        PlayerPrefs.Save();
    }

    public void ChangeMusicVolume(float value)
    {
        musicVolume = value;
        PlayerPrefs.SetFloat(musicVolumeSavedValue, value);
        PlayerPrefs.Save();

        MusicManager music = FindObjectOfType<MusicManager>();

        if (music != null)
        {
            music.OnVolumeChanged(value);
        }
    }
}
