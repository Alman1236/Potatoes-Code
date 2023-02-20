using System.Collections;
using UnityEngine;
using EnumsAndStructs;

public class FreezingManager : MonoBehaviour
{
    [SerializeField] bool canCharacterFreeze = false;
    [SerializeField] ParticleSystem freezingEffect;

    bool isCooldownActive = false;

    public bool isFrozen { get; private set; } = false;

    Rigidbody2D myRigidbody;
    Character me;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        me = GetComponent<Character>();
    }

    public void OnResettingCharacter()
    {
        if (canCharacterFreeze)
        {
            isCooldownActive = false;
            Unfreeze();
            isCooldownActive = false;

            freezingEffect.Clear();
        }   
    }

    public void FreezeOrUnfreeze()
    {
        me.actionsRecorder.RecordAction(actionTypes.freezeOrUnfreeze, 0, Vector3.zero);

        if (canCharacterFreeze && !isCooldownActive && !me.deathManager.isDead)
        {
            PhasesManager.instance.OnSendingCommandToCharacter();

            if (isFrozen)
            {
                Unfreeze();
            }
            else
            {
                Freeze();
            }
        }
    }

    void Freeze()
    {
        StartCoroutine(StartCooldown());
        myRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        isFrozen = true;
        freezingEffect.Play();
    }

    void Unfreeze()
    {
        StartCoroutine(StartCooldown());
        myRigidbody.constraints = RigidbodyConstraints2D.None;
        myRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        isFrozen = false;
        freezingEffect.Stop();

    }

    IEnumerator StartCooldown()
    {
        isCooldownActive = true;
        yield return new WaitForSeconds(0.07f);
        isCooldownActive = false;
    }

    public void OnDeath()
    {
        if (canCharacterFreeze)
        {
            Unfreeze();
        }
    }
}
