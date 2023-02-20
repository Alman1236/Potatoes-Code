using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EnumsAndStructs;

public class MainMenuTranslator : Translator
{
    [SerializeField]
    TextMeshProUGUI tmpBack1, tmpBack2, tmpBack3, tmpPlay, tmpOptions, tmpQuit, tmpLanguage,
        tmpResolution, tmpEffectsAudio, tmpMusicAudio, tmpScreenMode, tmpFramerateCap, tmpConfirm,
        tmpCancel, tmpPlayLevel, tmpOptionsTitle, tmpCredits;

    override protected void Awake()
    {
        textsInTheScene.Add(tmpBack1, textsNames.mainMenu_btnBack);
        textsInTheScene.Add(tmpBack2, textsNames.mainMenu_btnBack);
        textsInTheScene.Add(tmpBack3, textsNames.mainMenu_btnBack);
        textsInTheScene.Add(tmpPlay, textsNames.mainMenu_btnPlay);
        textsInTheScene.Add(tmpOptions, textsNames.mainMenu_btnOptions);
        textsInTheScene.Add(tmpQuit, textsNames.mainMenu_btnQuit);
        textsInTheScene.Add(tmpLanguage, textsNames.options_language);
        textsInTheScene.Add(tmpResolution, textsNames.options_resolution);
        textsInTheScene.Add(tmpEffectsAudio, textsNames.options_effectsVolume);
        textsInTheScene.Add(tmpMusicAudio, textsNames.options_musicVolume);
        textsInTheScene.Add(tmpScreenMode, textsNames.options_screenMode);
        textsInTheScene.Add(tmpFramerateCap, textsNames.options_framerateCap);
        textsInTheScene.Add(tmpOptionsTitle, textsNames.options_title);
        textsInTheScene.Add(tmpConfirm, textsNames.savingsMenu_confirm);
        textsInTheScene.Add(tmpCancel, textsNames.savingsMenu_cancel);
        textsInTheScene.Add(tmpPlayLevel, textsNames.mainMenu_btnPlayLevel);
        textsInTheScene.Add(tmpCredits, textsNames.mainMenu_credits);

        base.Awake();
    }
}
