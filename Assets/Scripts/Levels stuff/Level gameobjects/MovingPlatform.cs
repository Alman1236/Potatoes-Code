using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour, IActivable, IResettable, IObserver
{
    [SerializeField] Vector3[] orderedMovementPoints = new Vector3[0];
    byte nextPoint;

    bool isMovingTowardNextTarget;
    Vector3 startingPos;

    [SerializeField,Tooltip("if true add this object to phases manager observers")] 
    bool isAlwaysActive = false;

    void Awake()
    {
        transform.position = orderedMovementPoints[0];
        startingPos = transform.position;
        SetNextPoint();

        if (orderedMovementPoints.Length < 2)
        {
            Debug.LogError("Platform has not enough movement points", gameObject);
        }
    }

    void IActivable.Activate()
    {
        isMovingTowardNextTarget = true;
    }

    void IActivable.Deactivate()
    {
        isMovingTowardNextTarget = false;
    }

    void IResettable.Reset()
    {
        isMovingTowardNextTarget = false;
        transform.position = startingPos;
        nextPoint = 1;
    }

    void SetNextPoint()
    {
        nextPoint++;

        if (nextPoint >= orderedMovementPoints.Length)
        {
            nextPoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (isMovingTowardNextTarget)
        {
            MoveTowardTargetPosition();
        }
    }

    void MoveTowardTargetPosition()
    {
        if (Mathf.Abs(Vector3.Distance(transform.position, orderedMovementPoints[nextPoint])) < .2f)
        {
            transform.position = orderedMovementPoints[nextPoint];
            SetNextPoint();
        }
        else
        {
            Vector3 movement = Vector3.MoveTowards(transform.position, orderedMovementPoints[nextPoint], Data.MovingPlatformSpeed * Time.fixedDeltaTime);
            transform.position = movement;
        }
    }

    void IObserver.Notify()
    {
        if (isAlwaysActive)
        {
            if (Data.currentPhase == EnumsAndStructs.Phases.recording)
            {
                isMovingTowardNextTarget = true;
            }
        }
       
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            collision.transform.SetParent(null);
        }
    }
}
