using UnityEngine;
using UnityEngine.UI;
using EnumsAndStructs;
using Sirenix.OdinInspector;

public class LevelsGridManager : SerializedMonoBehaviour
{
    [SerializeField] level[] levels = new level[1];
    [SerializeField] Button[] levelsButtons = new Button[1];
   
    struct level
    {
        [HideInInspector] public ushort sceneIndex;
        [HideInInspector] public Button associatedButton;
        public Sprite preview;
    }

    level selectedLevel;

    private void Awake()
    {
        InitializeButtons();
        selectedLevel = levels[0];
    }

    void InitializeButtons()
    {
        for (ushort i = 0; i < levels.Length; i++)
        {
            ushort levelNumber = i;

            //i + 1 because the scene order is:
            //0: main menu;  1: level 0;  2: level 1; ...
            levels[i].sceneIndex = (ushort)(i + 1);
            levels[i].associatedButton = levelsButtons[i];
            levels[i].associatedButton.onClick.AddListener(delegate { OnClickLevelButton(levels[levelNumber]); });
        }
    }

    public void LoadLevelsData()
    {
        ResetAllLevelsButtons();
        MakeAvailableLevelsButtonsYellow();
        MakeCompletedLevelsButtonsGreen();
    }
    
    void ResetAllLevelsButtons()
    {
        for (short i = 0; i < levelsButtons.Length; i++)
        {
            levelsButtons[i].interactable = false;
            levelsButtons[i].GetComponent<Image>().color = Color.red;
        }
    }

    void MakeCompletedLevelsButtonsGreen()
    {
        if (DemoData.isDemoBuild && DemoData.unlockAllLevels)
        {
            for (short i = 0; i < levels.Length; i++)
            {
                levels[i].associatedButton.interactable = true;
                levels[i].associatedButton.GetComponent<Image>().color = Color.green;
                
            }
        }
        else
        {
            for (short i = 0; i < Data.activeProfile.completedLevels.Count; i++)
            {
                ushort level = Data.activeProfile.completedLevels[i];

                if (level <= DemoData.GetHighestPlayableLevel())
                {
                    levels[level].associatedButton.interactable = true;
                    levels[level].associatedButton.GetComponent<Image>().color = Color.green;
                }
            }
        }
        
    }

    void MakeAvailableLevelsButtonsYellow()
    {
        for (byte i = 0; i < Data.activeProfile.availableLevels.Length; i++)
        {
            ushort availableLevel = Data.activeProfile.availableLevels[i];

            if (availableLevel != (ushort)Conventions.noLevel && availableLevel < DemoData.GetHighestPlayableLevel())
            {
                levels[availableLevel].associatedButton.interactable = true;
                levels[availableLevel].associatedButton.GetComponent<Image>().color = Color.yellow;
            }
        }
    }

    void OnClickLevelButton(level level)
    {
        selectedLevel = level;
        LevelPreviewManager.UpdateLevelPreview(level.preview);
    }

    public void OnClickPlayLevel()
    {
        ScenesLoader.instance.LoadLevel(selectedLevel.sceneIndex);
    }
}
