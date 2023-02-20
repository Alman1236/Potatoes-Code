using System.Collections;
using UnityEngine;
using EnumsAndStructs;
using Sirenix.OdinInspector;

public class FlameThrower : MonoBehaviour, IActivable, IResettable, IObserver
{
    [SerializeField] bool activatesOnHisOwn = false;

    [SerializeField, ShowIf("activatesOnHisOwn")] float flamesAndPauseDuration = 1;
    [SerializeField, ShowIf("activatesOnHisOwn")] bool startAsActive = false;

    [SerializeField] GameObject flames;

    void IObserver.Notify()
    {
        if (Data.currentPhase == Phases.recording)
        {
            OnStartingRecordPhase();
        }
    }

    void OnStartingRecordPhase()
    {
        if (activatesOnHisOwn)
        {
            if (startAsActive)
            {
                StartCoroutine(DeactivateAfterWaiting());
            }
            else
            {
                StartCoroutine(ActivateAfterWaiting());
            }
        }
    }

    IEnumerator ActivateAfterWaiting()
    {
        yield return new WaitForSeconds(flamesAndPauseDuration);
        
        ThrowFlames();
        StartCoroutine(DeactivateAfterWaiting());
    }

    IEnumerator DeactivateAfterWaiting()
    {
        yield return new WaitForSeconds(flamesAndPauseDuration);

        StopThrowingFlames();
        StartCoroutine(ActivateAfterWaiting());
    }

    void IResettable.Reset()
    {
        if (startAsActive)
        {
            ThrowFlames();
        }
        else
        {
            StopThrowingFlames();
        }
      
        StopAllCoroutines();
    }

    void IActivable.Activate()
    {
        ThrowFlames();
    }

    void IActivable.Deactivate()
    {
        StopThrowingFlames();
    }

    void ThrowFlames()
    {
        flames.SetActive(true);
    }

    void StopThrowingFlames()
    {
        flames.SetActive(false);
    }


}
