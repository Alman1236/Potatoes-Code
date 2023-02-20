using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SavingsManager : MonoBehaviour
{
    [SerializeField] ProfileButton[] profilesButtons = new ProfileButton[3];

    static string[] profilesDataFilePath;

    private void Start()
    {
        //todo: when opening a file, check for permissions

        for (byte i = 0; i < profilesButtons.Length; i++)
        {
            profilesButtons[i].profileSlot = i;
        }

        profilesDataFilePath = new string[Data.maxSaveFiles];
        for (sbyte i = (sbyte)(Data.maxSaveFiles - 1); i >= 0; i--)
        {
            //next line and the same one in DeleteSaveFile() should ALWAYS BE THE SAME
            profilesDataFilePath[i] = Application.persistentDataPath + "/profile_" + i + "_data.data";
        }

        LoadGameData();
    }

    void LoadGameData()
    {
        ProfileData[] profilesData = OpenOrCreateProfilesDataFiles();

        for (byte i = 0; i < Data.maxSaveFiles; i++)
        {
            if (profilesData[i].isProfileCreated)
            {
                profilesButtons[i].Set(profilesData[i]);
            }
            else
            {
                profilesButtons[i].ResetProfileButton();
            }
        }
    }

    ProfileData[] OpenOrCreateProfilesDataFiles()
    {
        FileStream file;
        ProfileData[] profilesData = new ProfileData[Data.maxSaveFiles];
        BinaryFormatter formatter = new BinaryFormatter();

        for (byte i = 0; i < Data.maxSaveFiles; i++)
        {
            if (File.Exists(profilesDataFilePath[i]))
            {
                file = new FileStream(profilesDataFilePath[i], FileMode.Open);

                ProfileData profileData = formatter.Deserialize(file) as ProfileData;
                profilesData[i] = profileData;
            }
            else
            {
                file = new FileStream(profilesDataFilePath[i], FileMode.CreateNew);
                formatter.Serialize(file, new ProfileData());

                profilesData[i] = new ProfileData();
            }

            file.Close();
        }

        return profilesData;
    }

    public void DeleteSaveFile(ushort whichProfile)
    {
        //next line and the same one in Awake() should ALWAYS BE THE SAME
        string path = Application.persistentDataPath + "/profile_" + whichProfile + "_data.data";

        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    static public void Save()
    {
        string path = profilesDataFilePath[Data.activeProfile.profileSlot];
        FileStream file = new FileStream(path, FileMode.Open);
        BinaryFormatter formatter = new BinaryFormatter();

        formatter.Serialize(file, Data.activeProfile);

        file.Close();
    }

    static public void SaveNewProfile(ProfileData data, byte slot)
    {
        string path = profilesDataFilePath[slot];
        FileStream file = new FileStream(path, FileMode.OpenOrCreate);
        BinaryFormatter formatter = new BinaryFormatter();

        formatter.Serialize(file, data);

        file.Close();
    }
}
