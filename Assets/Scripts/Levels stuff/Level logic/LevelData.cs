using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;

public class LevelData : SerializedMonoBehaviour
{
    static public LevelData instance;

    [SerializeField] GameObject winWindow;
    [SerializeField] TextMeshProUGUI tmpObjectivesReedemed, endgameText;
    
    [SerializeField] byte objectivesNumber;

    public float timeToCompleteLevel = 20;
    public Character[] characters;
    [SerializeField] IResettable[] objectsToResetOnPreparationPhase;

    byte completedObjectives = 0;
    void Awake()
    {
        GenerateInstance();
        InitializeCharactersIndices();

        if(objectivesNumber > 1)
        {
            tmpObjectivesReedemed.transform.parent.gameObject.SetActive(true) ;
        }
    }

    void InitializeCharactersIndices()
    {
        for (byte i = 0; i < characters.Length; i++)
        {
            characters[i].indexInLevel = i;
        }
    }

    void GenerateInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("There should be only one LevelData.cs in every scene! ", gameObject);
            Debug.Break();
        }
    }

    public void OnReachingObjective()
    {
        completedObjectives++;

        if (objectivesNumber > 1)
        {
            tmpObjectivesReedemed.text = completedObjectives + "/" + objectivesNumber;
        }

        if (completedObjectives == objectivesNumber)
        {
            Win();
        }
    }

    public void Win()
    {
        MusicManager music = FindObjectOfType<MusicManager>();
        if (music != null)
        {
            music.OnPlayingVictrorySound();
        }
        else
        {
            Debug.Log("music manager not found");
        }

        ushort whichLevelIsThis = (ushort)(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex - 1);
        
        winWindow.SetActive(true);
        //if(whichLevelIsThis == 14 && DemoData.isDemoBuild)
        //{
            //endgameText.text = GameTexts.GetTranslatedText(EnumsAndStructs.textsNames.demoEnded);
        //}

        if (whichLevelIsThis == 49)
        {
            endgameText.text = GameTexts.GetTranslatedText(EnumsAndStructs.textsNames.gameEnded);
        }

        Timer.instance.StopDecreasingTime();
        PhasesManager.instance.OnWinning();

       
        Data.activeProfile.OnWinning(whichLevelIsThis, Time.timeSinceLevelLoad);
    }

    public void OnUndoObjective()
    {
        completedObjectives--;
     
        if (objectivesNumber > 1)
        {
            tmpObjectivesReedemed.text = completedObjectives + "/" + objectivesNumber;
        }
    }

    public void ResetLevel()
    {
        for (byte i = 0; i < objectsToResetOnPreparationPhase.Length; i++)
        {
            objectsToResetOnPreparationPhase[i].Reset();
        }
    }
}
