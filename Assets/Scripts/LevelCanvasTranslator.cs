using UnityEngine;
using TMPro;
using EnumsAndStructs;

public class LevelCanvasTranslator : Translator
{
    [SerializeField]
    TextMeshProUGUI tmpResume, tmpQuit, tmpOptions, tmpQuit2, tmpSkip, tmpLanguage,
       tmpResolution, tmpEffectsAudio, tmpMusicAudio, tmpScreenMode, tmpFramerateCap, 
       tmpOptionsTitle, tmpPlayNext, tmpPlayAgain, tmpGameEnded, tmpLevelCompleted, tmpPauseTitle,
        tmpCommandsTitle, tmpCommands, tmpRToStart, tmpRToReset;

    protected override void Awake()
    {
        textsInTheScene.Add(tmpResume, textsNames.pause_resume);
        textsInTheScene.Add(tmpQuit, textsNames.pause_backToMenu);
        textsInTheScene.Add(tmpQuit2, textsNames.pause_backToMenu);
        textsInTheScene.Add(tmpSkip, textsNames.pause_skipLevel);
        textsInTheScene.Add(tmpPlayNext, textsNames.endgame_playNext);
        textsInTheScene.Add(tmpPlayAgain, textsNames.endgame_playAgain);
        textsInTheScene.Add(tmpLevelCompleted, textsNames.endgame_levelCompleted);
        textsInTheScene.Add(tmpOptions, textsNames.options_title);
        textsInTheScene.Add(tmpPauseTitle, textsNames.pause_title);
        
        textsInTheScene.Add(tmpCommandsTitle, textsNames.options_commandsTitle);
        textsInTheScene.Add(tmpCommands, textsNames.options_commands);


        textsInTheScene.Add(tmpLanguage, textsNames.options_language);
        textsInTheScene.Add(tmpResolution, textsNames.options_resolution);
        textsInTheScene.Add(tmpEffectsAudio, textsNames.options_effectsVolume);
        textsInTheScene.Add(tmpMusicAudio, textsNames.options_musicVolume);
        textsInTheScene.Add(tmpScreenMode, textsNames.options_screenMode);
        textsInTheScene.Add(tmpFramerateCap, textsNames.options_framerateCap);
        textsInTheScene.Add(tmpOptionsTitle, textsNames.options_title);
        
        textsInTheScene.Add(tmpRToStart, textsNames.tmpRToStart);
        textsInTheScene.Add(tmpRToReset, textsNames.tmpRToReset);
        
        if(tmpGameEnded != null)
        {
            textsInTheScene.Add(tmpGameEnded, textsNames.gameEnded);
        }

        base.Awake();
    }
}
