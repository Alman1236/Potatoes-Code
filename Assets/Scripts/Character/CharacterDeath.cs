using UnityEngine;
using EnumsAndStructs;
using Sirenix.OdinInspector;

public class CharacterDeath : SerializedMonoBehaviour
{
    [HideInInspector] public bool isDead;
    Character me;

    [SerializeField] bool imLinkedToObjective;
    [ShowIf("imLinkedToObjective"), SerializeField] GameObject linkedObjective;

    private void Awake()
    {
        me = GetComponent<Character>();
    }

    public void Die(deathCause cause)
    {
        if (!isDead)
        {
            me.audioManager.PlayStepsSound(false);

            if (imLinkedToObjective)
            {
                linkedObjective.SetActive(false);
            }

            me.freezingManager.OnDeath();

            if (cause == deathCause.flames || cause == deathCause.badPotato)
            {
                me.audioManager.PlaySound(AudioClipsNames.deathByFlames);
                me.freezingManager.OnDeath();
            }
            else if (cause == deathCause.fall)
            {
                AudioManager.instance.PlaySound(AudioClipsNames.fallingOutOfMap);
            }
            else
            {
                me.audioManager.PlaySound(AudioClipsNames.death);
            }

            me.animationsManager.DoAnimation(Animations.death);
            isDead = true;

            if (CharacterSelector.instance.selectedCharacter == me)
            {
                FindObjectOfType<InGameUIManager>().OnSelectedCharacterDeath();
            }
        }
    }

    public void OnResettingCharacter()
    {
        if (imLinkedToObjective)
        {
            linkedObjective.SetActive(true);
        }

        isDead = false;
    }
}
