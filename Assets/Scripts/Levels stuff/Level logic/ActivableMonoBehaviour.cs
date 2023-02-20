using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivableMonoBehaviour : MonoBehaviour,IResettable
{
    byte numberOfObjectsActivatingMe;

    public void TryToActivate()
    {
        numberOfObjectsActivatingMe++;

        if(numberOfObjectsActivatingMe == 1)
        {
            Activate();
        }
    }

    public void TryToDeActivate()
    {
        numberOfObjectsActivatingMe--;

        if (numberOfObjectsActivatingMe == 0)
        {
            Deactivate();
        }
    }

    protected virtual void Activate()
    {

    }

    protected virtual void Deactivate()
    {

    }

    void IResettable.Reset()
    {
        numberOfObjectsActivatingMe = 0;
    }
}
