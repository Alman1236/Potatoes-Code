using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProfileData
{
    public byte profileSlot;

    public bool isProfileCreated = false;
    public string profileName;

    public List<ushort> completedLevels = new List<ushort>();
    public ushort[] availableLevels = new ushort[3] { 0, 1, 2 };

    public uint secondsPlayed = 0;

    public void OnWinning(ushort levelWon, float timeUsedToCompleteLevel)
    {
        bool flag = false;
        byte i = 0;

        UpdateTimePlayed(timeUsedToCompleteLevel);

        if (!completedLevels.Contains(levelWon))
        {
            completedLevels.Add(levelWon);
        }

        do
        {
            if (availableLevels[i] == levelWon)
            {
                flag = true;

                if ((ushort)(GetHighestCompletedOrAvailableLevel() + 1) <= DemoData.GetHighestPlayableLevel())
                {
                    availableLevels[i] = (ushort)(GetHighestCompletedOrAvailableLevel() + 1);
                }                   
            }

            i++;
        } while (i < availableLevels.Length && flag == false);

        SavingsManager.Save();
    }

    public void UpdateTimePlayed(float timeUsedToCompleteLevel)
    {
        secondsPlayed += (uint)(timeUsedToCompleteLevel);
    }

    ushort GetHighestCompletedOrAvailableLevel()
    {
        ushort highestAvailableOrCompletedLevel = GetHighestAvailableLevel();

        for (ushort i = 0; i < completedLevels.Count; i++)
        {
            if (completedLevels[i] > highestAvailableOrCompletedLevel)
            {
                highestAvailableOrCompletedLevel = completedLevels[i];
            }
        }

        return highestAvailableOrCompletedLevel;
    }

    ushort GetHighestAvailableLevel()
    {
        ushort highestAvailableLevel = availableLevels[2];

        if (availableLevels[0] > availableLevels[1] && availableLevels[0] > availableLevels[2])
        {
            highestAvailableLevel = availableLevels[0];
        }
        else if (availableLevels[1] > availableLevels[2] && availableLevels[1] > availableLevels[0])
        {
            highestAvailableLevel = availableLevels[1];
        }

        return highestAvailableLevel;
    }

    public bool CanPlayLevel(byte level)
    {
        if (completedLevels.Contains(level))
        {
            return true;
        }

        for (byte i = 0; i < availableLevels.Length; i++)
        {
            if(availableLevels[i] == level)
            {
                return true;
            }
        }

        return false;
    }
}
