using Sirenix.OdinInspector;
using UnityEngine;
using EnumsAndStructs;

public class Objective : SerializedMonoBehaviour
{
    [SerializeField] bool everyCharacterCanTakeThis = true;

    [HideIf("everyCharacterCanTakeThis")]
    [SerializeField] Character[] charactersThatCanTakeThis = new Character[0];

    byte charactersOnObjective = 0;

    [SerializeField] byte neededCharactersOnObjective = 1;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            Character character = collision.GetComponent<Character>();
            OnCharacterEnterObjective(character);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Character") && CanCharacterTakeThis(collision.GetComponent<Character>()))
        {
            charactersOnObjective--;

            if (WasObjectiveReached())
            {
                AudioManager.instance.PlaySound(AudioClipsNames.onUndoObjective);
                LevelData.instance.OnUndoObjective();
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    void OnCharacterEnterObjective(Character character)
    {
        if (CanCharacterTakeThis(character) && !character.deathManager.isDead)
        {
            charactersOnObjective++;
            CheckIfObjectiveIsReached(character);
        }
        else
        {
            character.audioManager.PlaySound(AudioClipsNames.targetNotValidForObjective);
        }
    }

    bool WasObjectiveReached()
    {
        if (charactersOnObjective == neededCharactersOnObjective - 1)
        {
            return true;
        }

        return false;
    }

    void CheckIfObjectiveIsReached(Character whoReachedObjective)
    {
        if (charactersOnObjective == neededCharactersOnObjective)
        {
            AudioManager.instance.PlaySound(AudioClipsNames.onCompletingObjective);
            LevelData.instance.OnReachingObjective();
            transform.localScale = new Vector3(1.25f, 1.25f, 1);
        }
        else
        {
            whoReachedObjective.audioManager.PlaySound(AudioClipsNames.onReachingObjective);
        }
    }

    bool CanCharacterTakeThis(Character character)
    {
        if (everyCharacterCanTakeThis)
        {
            return true;
        }
        else
        {
            for (byte i = 0; i < charactersThatCanTakeThis.Length; i++)
            {
                if (charactersThatCanTakeThis[i].indexInLevel == character.indexInLevel)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
