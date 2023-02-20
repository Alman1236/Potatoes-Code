using UnityEngine;
using UnityEngine.UI;
using TMPro;
using EnumsAndStructs;

public class ProfileButton : MonoBehaviour
{
    [HideInInspector] public byte profileSlot;

    LevelsGridManager levelsGridManager;
    SavingsManager savingsManager;
    MainMenuButtonsFunctions buttonsFunctions;
    [SerializeField] PopUpNotificationManager notificationManager;

    ProfileData linkedProfile;
    [SerializeField] Button btnProfile, btnDelete;
    [SerializeField] TextMeshProUGUI tmpProfileName,tmpTimePlayed, tmpLevelsCompleted;

    private void Awake()
    {
        levelsGridManager = FindObjectOfType<LevelsGridManager>();
        savingsManager = FindObjectOfType<SavingsManager>();
        buttonsFunctions = FindObjectOfType<MainMenuButtonsFunctions>();
    }

    public void ResetProfileButton()
    {
        savingsManager.DeleteSaveFile(profileSlot);

        btnDelete.gameObject.SetActive(false);

        btnProfile.onClick.RemoveAllListeners();
        btnProfile.onClick.AddListener(OnClickCreateNewProfile);

        ResetTexts();
    }

    private void OnEnable()
    {
        if (!btnDelete.gameObject.activeSelf)
        {
            ResetTexts();
        }
    }

    void ResetTexts()
    {
        tmpProfileName.text = GameTexts.GetTranslatedText(EnumsAndStructs.textsNames.savingsMenu_createNewProfile);
        tmpLevelsCompleted.text = "-";
        tmpTimePlayed.text = "-";
    }

    public void Set(ProfileData linkedProfile) 
    {
        linkedProfile.profileSlot = profileSlot;
        this.linkedProfile = linkedProfile;

        btnProfile.onClick.RemoveAllListeners();

        btnProfile.onClick.AddListener(OnClickProfileButton);
        btnProfile.onClick.AddListener(buttonsFunctions.OnClickProfileButton);
        btnProfile.onClick.AddListener(levelsGridManager.LoadLevelsData);

        btnDelete.gameObject.SetActive(true);
        btnDelete.onClick.RemoveAllListeners();

        btnDelete.onClick.AddListener(OnClickDeleteProfile);

        tmpProfileName.text = linkedProfile.profileName;
        tmpLevelsCompleted.text = linkedProfile.completedLevels.Count + "\\" + "50";
        tmpTimePlayed.text = (ushort)(linkedProfile.secondsPlayed/3600) + "h " + (ushort)((linkedProfile.secondsPlayed % 3600)/60) +"m";

        SavingsManager.SaveNewProfile(linkedProfile, profileSlot);
    }

    void OnClickCreateNewProfile()
    {
        notificationManager.RequestToPlayer(GameTexts.GetTranslatedText(textsNames.savingsMenu_insertNameRequest), OnReceivingNameForNewPlayer, true);
    }

    public void OnReceivingNameForNewPlayer()
    {
        ProfileData newProfile = new ProfileData();

        if(notificationManager.lastTextInserted == "")
        {
            newProfile.profileName = "Potato";
        }
        else
        {
            newProfile.profileName = notificationManager.lastTextInserted;
        }
        
        newProfile.isProfileCreated = true;

        Set(newProfile);
    }

    void OnClickProfileButton()
    {
        Data.activeProfile = linkedProfile;
    }

    void OnClickDeleteProfile()
    {
        string question = GameTexts.GetTranslatedText(textsNames.savingsMenu_confirmToDeleteSavingPart1) + linkedProfile.profileName + GameTexts.GetTranslatedText(textsNames.savingsMenu_confirmToDeleteSavingPart2);
        notificationManager.RequestToPlayer(question, ResetProfileButton);
    }

    
}
