using System.Collections.Generic;
using UnityEngine;
using EnumsAndStructs;

public class GameTexts : MonoBehaviour
{
    static Dictionary<textsNames, string> englishTexts = new Dictionary<textsNames, string>()
    {
        {textsNames.tutorialMessage_Welcome, "Hello, and welcome to POTATOES! In this game you control multiple characters, but one at the time" },
        {textsNames.tutorialMessage_PreparationPhase, "Now, you are in PREPARATION phase. During this phase you can study the level, or choose a character" },
        {textsNames.tutorialMessage_ChooseACharacter, "To select a character, click on it, or press LEFT SHIFT to cycle between them. Select one character to continue" },
        {textsNames.tutorialMessage_RecordingPhase, "Nice! Now, when you'll move or jump you'll switch to RECORDING phase. During this phase every input you give is recorded and repeated while you use other characters" },
        {textsNames.tutorialMessage_RecordingOverwriting, "Note: if you play RECORDING phase with a character who already has recorded behaviour, you'll overwrite it" },
        {textsNames.tutorialMessage_RecordingPhaseEnd, "If the time ends or when you manually end the recording phase (pressing R), you will go back to PREPARATION phase" },
        {textsNames.tutorialMessage_SecondPreparationPhase, "Now, if you play with another character, the first one will repeat the recorded inputs! Watch him, is ready go!" },
        {textsNames.tutorialMessage_ReachTheStars, "You can use this to reach BOTH STARS SIMULTANEOUSLY, have fun!" },
        {textsNames.tutorialMessage_ButtonsInTheTopRightCorner, "You can use the buttons in the top right corner to clear character behaviours" },
        {textsNames.tutorialMessage_FreeCameraButton, "In the bigger levels, you can use the \"free camera\" button in the top right corner to explore the level" },
        {textsNames.tutorialMessage_FreezingPotato, "While you play with freezing potato, press E to freeze/unfreeze her. Frozen potato can't be moved and it is uneffected by gravity" },
        {textsNames.tutorialMessage_Shooting, "The potatoes with a backpack can throw rocks and destroy bots. Aim with the MOUSE and Press E to throw a rock" },
        {textsNames.tutorialMessage_StarCharacter, "In this level you must reach the star potato to win. If the star potato is dead, you can't redeem objective!" },
        
        {textsNames.tutorialMessage_ChooseACharacterToContinue, "Select a character to continue" },
        
        {textsNames.tutorialCommands_Skip, "Left click / Space to continue\nScroll wheel / X to skip" },

        {textsNames.pnlInfo_clearAllRecordings, "Clear all potoatoes' recordings" },
        {textsNames.pnlInfo_clearSelectedCharacterRecording, "Clear selected character recording" },
        {textsNames.pnlInfo_freeCamera, "Unlocks the camera. Use directional keys to move it" },

        {textsNames.freeCameraControls, "WASD: control camera\nC: cancel" },

        {textsNames.inGameUi_preparationPhase, "Preparation" },
        {textsNames.inGameUi_recordingPhase, "Recording" },
        {textsNames.inGameUi_resetting, "Rewind" },

        {textsNames.mainMenu_btnPlay, "Play" },
        {textsNames.mainMenu_btnOptions, "Options" },
        {textsNames.mainMenu_btnQuit, "Quit" },
        {textsNames.mainMenu_btnBack, "Back" },
        {textsNames.mainMenu_btnPlayLevel, "Play" },
        {textsNames.mainMenu_title, "Potatoes" },
        {textsNames.mainMenu_credits, "Game by Aldo Mangione\nMusic by Gabriel Zurek\nSounds from: Zapsplat.com" },

        {textsNames.savingsMenu_confirm, "Confirm" },
        {textsNames.savingsMenu_cancel, "Cancel" },
        {textsNames.savingsMenu_insertNameRequest, "Insert Name" },
        {textsNames.savingsMenu_confirmToDeleteSavingPart1, "Are you sure you want to delete " },
        {textsNames.savingsMenu_confirmToDeleteSavingPart2, "? You cannot undo this action" },
        {textsNames.savingsMenu_createNewProfile, "Create new profile" },

        {textsNames.options_effectsVolume, "Effects volume" },
        {textsNames.options_musicVolume, "Music volume" },
        {textsNames.options_resolution, "Resolution" },
        {textsNames.options_language, "Language" },
        {textsNames.options_framerateCap, "Framerate cap" },
        {textsNames.options_screenMode, "Screen mode" },
        {textsNames.options_title, "Options" },
        {textsNames.options_uncappedFramerate, "Uncapped" },
        {textsNames.options_fullScreenMode, "Fullscreen" },
        {textsNames.options_windowedMode, "Windowed" },
        {textsNames.options_commandsTitle, "Commands" },
        {textsNames.options_commands, "-Lshift to cycle characters\n-R to start or end recording phase\n-WASD to move\n-C to lock/unlock camera\n-Q reset selected potato\n-V reset all potatoes\n-You can click potatoes to select them" },

        {textsNames.endgame_playAgain, "Play\nagain" },
        {textsNames.endgame_playNext, "Play\nnext" },
        {textsNames.endgame_levelCompleted, "Level completed!" },

        {textsNames.pause_resume, "Resume" },
        {textsNames.pause_title, "Pause" },
        {textsNames.pause_options, "Options" },
        {textsNames.pause_skipLevel, "Skip level" },
        {textsNames.pause_backToMenu, "Main menu" },

        {textsNames.tmpRToReset, "Press R to reset " },
        {textsNames.tmpRToStart, "Press R or WASD to start" },

        {textsNames.gameEnded, "CONGRATULATIONS! You completed the game, thanks for playing it!" },
        {textsNames.demoEnded, "Compliments, you completed demo levels, to play the other 35 levels you need to buy full game. Thanks for playing!" }
    };

    static Dictionary<textsNames, string> italianTexts = new Dictionary<textsNames, string>()
    {
        {textsNames.tutorialMessage_Welcome, "Ciao, e benvenuto in POTATOES! In questo gioco controllerai vari personaggi, ma uno alla volta" },
        {textsNames.tutorialMessage_PreparationPhase, "Ora sei in fase di PREPARAZIONE. Durante questa fase puoi studiare il livello, o scegliere un personaggio" },
        {textsNames.tutorialMessage_ChooseACharacter, "Per selezionare un personaggio, cliccaci sopra, o premi SHIFT per passare da uno all'altro. Per continuare, seleziona un personaggio" },
        {textsNames.tutorialMessage_RecordingPhase, "Ottimo! Ora, quando ti muoverai passerai alla fase di REGISTRAZIONE. Durante questa fase ogni tuo input sara' registrato e ripetuto mentre usi gli altri personaggi" },
        {textsNames.tutorialMessage_RecordingOverwriting, "Nota: se giochi la fase di registrazione con un personaggio che ha gia' del comportamento registrato, lo sovrascriverai" },
        {textsNames.tutorialMessage_RecordingPhaseEnd, "Quando il tempo termina, o quando termini manualmente la fase di registrazione (premendo R), tornerai alla fase di preparazione" },
        {textsNames.tutorialMessage_SecondPreparationPhase, "Ora, se scegli un altro personaggio, il primo ripetera' gli input registrati! Guardalo, e' pronto a partire!" },
        {textsNames.tutorialMessage_ReachTheStars, "Puoi utilizzare cio' per raggiungere contemporaneamente ENTRAMBE le STELLE! Buon divertimento!" },
        {textsNames.tutorialMessage_ButtonsInTheTopRightCorner, "Puoi usare i bottoni in alto a destra per resettare i personaggi!" },
        {textsNames.tutorialMessage_FreeCameraButton, "Nei livelli piu' grandi, puoi usare il bottone \"libera telecamera\" in alto a destra per esplorare il livello" },
        {textsNames.tutorialMessage_FreezingPotato, "Mentre usi una patata congelante, premi E per congelarla/scongelarla. Le patate congelate non possono essere mosse, nemmeno dalla gravita'" },
        {textsNames.tutorialMessage_Shooting, "Le patate con uno zaino possono lanciare pietre per distruggere i robot. Mira con il MOUSE e premi E per lanciarle" },
        {textsNames.tutorialMessage_StarCharacter, "In questo livello devi raggiungere la patata stellata. Se la patata stellata muore, non puoi completare il livello!" },
         
        {textsNames.tutorialMessage_ChooseACharacterToContinue, "Seleziona un personaggio per continuare" },

        {textsNames.tutorialCommands_Skip, "Click sinistro / spazio per continuare\nRotella del mouse / X per fermare" },
        
        {textsNames.pnlInfo_clearAllRecordings, "Resetta tutte le patate" },
        {textsNames.pnlInfo_clearSelectedCharacterRecording, "Resetta la patata selezionata" },
        {textsNames.pnlInfo_freeCamera, "Libera la telecamera. Usa i tasti direzionali per muoverla" },

        {textsNames.inGameUi_preparationPhase, "Preparazione" },
        {textsNames.inGameUi_recordingPhase, "Registrazione" },
        {textsNames.inGameUi_resetting, "Riavvolgendo" },

        {textsNames.freeCameraControls, "WASD: controlla telecamera\nC: annulla" },
                
        {textsNames.mainMenu_btnPlay, "Gioca" },
        {textsNames.mainMenu_btnOptions, "Opzioni" },
        {textsNames.mainMenu_btnQuit, "Esci" },
        {textsNames.mainMenu_btnBack, "Indietro" },
        {textsNames.mainMenu_btnPlayLevel, "Gioca" },
        {textsNames.mainMenu_title, "Potatoes" },
        {textsNames.mainMenu_credits, "Gioco creato da Aldo Mangione\nMusica di Gabriel Zurek\nSuoni da: Zapsplat.com" },

        {textsNames.savingsMenu_confirm, "Conferma" },
        {textsNames.savingsMenu_cancel, "Indietro" },
        {textsNames.savingsMenu_insertNameRequest, "Inserisci il nome" },
        {textsNames.savingsMenu_confirmToDeleteSavingPart1, "Sei sicuro di voler eliminare \"" },
        {textsNames.savingsMenu_confirmToDeleteSavingPart2, "\"? Non puoi annullare quest'azione" },
        {textsNames.savingsMenu_createNewProfile, "Crea nuovo profilo" },

        {textsNames.options_effectsVolume, "Volume effetti" },
        {textsNames.options_musicVolume, "Volume musica" },
        {textsNames.options_resolution, "Risoluzione" },
        {textsNames.options_language, "Lingua" },
        {textsNames.options_framerateCap, "Fps massimi" },
        {textsNames.options_screenMode, "Modalita' finestra" },
        {textsNames.options_title, "Opzioni" },
        {textsNames.options_uncappedFramerate, "Senza limite" },
        {textsNames.options_fullScreenMode, "Schermo intero" },
        {textsNames.options_windowedMode, "Finestra" },
        {textsNames.options_commandsTitle, "Comandi" },
        {textsNames.options_commands, "-Lshift per cambiare personaggio\n-R per iniziare o finire la fase di registrazione\n-WASD per muoverti\n-C per bloccare/sbloccare la telecamera\n-Q cancella il comportamento della patata selezionata\n-V cancella il comportamento di tutte le patate\n-Puoi cliccare le patate per selezionarle" },


        {textsNames.endgame_playAgain, "Gioca ancora" },
        {textsNames.endgame_playNext, "Gioca il prossimo" },
        {textsNames.endgame_levelCompleted, "Livello completato!" },

        {textsNames.pause_resume, "Riprendi" },
        {textsNames.pause_title, "Pausa" },
        {textsNames.pause_options, "Opzioni" },
        {textsNames.pause_skipLevel, "Salta livello" },
        {textsNames.pause_backToMenu, "Esci" },

        {textsNames.tmpRToReset, "Premi R per riavviare" },
        {textsNames.tmpRToStart, "Premi R o WASD per iniziare" },

        {textsNames.gameEnded, "CONGRATULAZIONI! Hai completato il gioco, grazie per averlo giocato!" },
        {textsNames.demoEnded, "Complimenti, hai completato i livelli di prova, per giocare gli altri 35 livelli e' necessario acquistare il gioco. Grazie per aver giocato!" }

    };


    static public string GetTranslatedText(textsNames whichText)
    {
        string text = "";

        switch (Options.languageSet)
        {
            case Languages.english:
                if (englishTexts.ContainsKey(whichText))
                {
                    text = englishTexts[whichText];
                }
                break;

            case Languages.italian:
                if (italianTexts.ContainsKey(whichText))
                {
                    text = italianTexts[whichText];
                }
                break;
        }

        if (text == "")
        {
            Debug.LogError("text: " + whichText.ToString() + " not contained in " + Options.languageSet.ToString() + " dictionary");
        }
        return text;
    }
}
