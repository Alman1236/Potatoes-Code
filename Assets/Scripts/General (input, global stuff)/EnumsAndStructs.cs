using UnityEngine;

namespace EnumsAndStructs
{
    public struct action
    {
        public actionTypes actionType;

        //For movement, for instance, is x axis
        public float associatedValue;

        public Vector3 associatedVector;

        //During which tick the action was executed
        public uint executionTick;

        public action(actionTypes action, uint executionTick, float associatedValue, Vector3 associatedVector)
        {
            actionType = action;
            this.associatedValue = associatedValue;
            this.executionTick = executionTick;
            this.associatedVector = associatedVector;
        }
    }

    public enum actionTypes : byte
    {
        jump,
        movement,
        shot,
        freezeOrUnfreeze,
    }

    public enum textsNames : byte
    {
        tutorialMessage_Welcome,
        tutorialMessage_PreparationPhase,
        tutorialMessage_ChooseACharacter,
        tutorialMessage_RecordingPhase,
        tutorialMessage_RecordingOverwriting,
        tutorialMessage_RecordingPhaseEnd,
        tutorialMessage_SecondPreparationPhase,
        tutorialMessage_ReachTheStars,
        tutorialMessage_ButtonsInTheTopRightCorner,
        tutorialMessage_FreeCameraButton,
        tutorialMessage_FreezingPotato,
        tutorialMessage_Shooting,
        tutorialMessage_StarCharacter,
        tutorialMessage_ChooseACharacterToContinue,
        tutorialCommands_Skip,

        pnlInfo_clearAllRecordings,
        pnlInfo_clearSelectedCharacterRecording,
        pnlInfo_freeCamera,

        mainMenu_title,
        mainMenu_btnPlay,
        mainMenu_btnPlayLevel,
        mainMenu_btnOptions,
        mainMenu_btnBack,
        mainMenu_btnQuit,
        mainMenu_credits,

        options_title,
        options_language,
        options_resolution,
        options_screenMode,
        options_effectsVolume,
        options_musicVolume,
        options_framerateCap,
        options_uncappedFramerate,
        options_windowedMode,
        options_fullScreenMode,

        options_commandsTitle,
        options_commands,

        pause_resume,
        pause_backToMenu,
        pause_options,
        pause_skipLevel,

        endgame_playAgain,
        endgame_playNext,

        savingsMenu_createNewProfile,
        savingsMenu_confirmToDeleteSavingPart1,
        savingsMenu_confirmToDeleteSavingPart2,
        savingsMenu_confirm,
        savingsMenu_cancel,
        savingsMenu_insertNameRequest,

        inGameUi_preparationPhase,
        inGameUi_recordingPhase,
        inGameUi_resetting,
        gameEnded,
        endgame_levelCompleted,
        pause_title,
        
        demoEnded,
        freeCameraControls,
        tmpRToStart,
        tmpRToReset,
    }

    public enum Phases : byte
    {
        recording,
        preparation,
        exploringMap,
        resetting,
        win
    }

    public enum deathCause : byte
    {
        flames,
        fall,
        bot,
        spikes,
        badPotato,
    }

    public enum AudioClipsNames : byte
    {
        mouseOverButton,
        buttonClick,
        victory,
        clockTick,
        onReachingObjective,
        onCompletingObjective,
        onUndoObjective,
        targetNotValidForObjective,
        steps,
        jump,
        tutorialMessage,
        touchedGroundAfterJump,
        jumpWithTrampoline,
        pressurePlatePressed,
        characterSelection,
        flames,
        deathByFlames,
        deathByBot,
        deathBySpikes,
        death,
        botFlying,//manca
        fallingOutOfMap,
        platformCracking,
        platformFalling,
        levelReset,
        clearedBehaviours,
        ambienceSounds,
        //night ambience sounds
        //sunset ambience sounds
        //winter ambience sounds
        //freeze,
        //unfreeze
        rockThrown,
        botHit,
        botDestroyed,

    }

    public enum Languages : byte
    {
        english = 0,
        italian = 1
    }

    public enum SoundTypes : byte
    {
        standardSound,
        music,
    }

    public enum Animations : byte
    {
        none,
        idle,
        idleWithRecordedMovement,
        run,
        jump,
        selected,
        death,
    }

    public enum Conventions : ushort
    {
        noLevel = ushort.MaxValue,
    }
}

