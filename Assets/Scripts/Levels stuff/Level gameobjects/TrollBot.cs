using UnityEngine;

public class TrollBot : MonoBehaviour, IResettable, IObserver
{
    Vector3 startingPos;
    bool isMoving;

    void Awake()
    {
        startingPos = transform.position;
    }

    void IResettable.Reset()
    {
        isMoving = false;
        transform.position = startingPos;
    }

    void IObserver.Notify()
    {
        if (Data.currentPhase == EnumsAndStructs.Phases.recording)
        {
            isMoving = true;
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            transform.position = new Vector3(transform.position.x - Data.BotSpeed * Time.fixedDeltaTime, transform.position.y, transform.position.z);
        }
    }
}
