using Sirenix.OdinInspector;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    bool isCameraFree = false;
    Vector3 targetPosition;

    public bool isCameraStatic;

    [SerializeField, HideIf("isCameraStatic")]
    Transform followedCharacter;

    [SerializeField, HideIf("isCameraStatic")]
    Vector2 offsetFromFollowedCharacter;

    [SerializeField, HideIf("isCameraStatic")]
    float positiveXLimit, negativeXLimit;

    [SerializeField, HideIf("isCameraStatic")]
    float positiveYLimit, negativeYLimit;

    void Awake()
    {
        targetPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (!isCameraStatic)
        {
            if (!isCameraFree)
            {
                UpdateCamPosition();
            }

            CheckCamLimits();
        }

        MoveTowardTargetPosition();
    }

    public void SetFollowedCharacter(Transform character)
    {
        if (!isCameraStatic)
        {
            followedCharacter = character;
            UpdateCamPosition();
            CheckCamLimits();
        }
    }

    void MoveTowardTargetPosition()
    {
        float speed = Data.LockedCameraSpeed;

        if (isCameraFree)
        {
            speed = Data.FreeCameraSpeed;
        }

        if (Mathf.Abs(Vector3.Distance(transform.position, targetPosition)) < .2f)
        {
            transform.position = targetPosition;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.fixedDeltaTime);
        }

    }

    void UpdateCamPosition()
    {
        float x = followedCharacter.position.x + offsetFromFollowedCharacter.x;
        float y = followedCharacter.position.y + offsetFromFollowedCharacter.y;

        targetPosition = new Vector3(x, y, transform.position.z);
    }

    public void FreeCamera(bool value)
    {
        isCameraFree = value;

        if (!isCameraFree)
        {
            CharacterSelector.instance.ReselectCharacter();
        }
    }

    public void MoveFreeCamera(Vector2 movement)
    {
        if (isCameraFree)
        {
            targetPosition = targetPosition + new Vector3(movement.x, movement.y, 0) * Data.FreeCameraSpeed * Time.deltaTime;
        }
    }

    void CheckCamLimits()
    {
        if (targetPosition.x > positiveXLimit)
        {
            targetPosition = new Vector3(positiveXLimit, targetPosition.y, targetPosition.z);
        }
        else if (targetPosition.x < negativeXLimit)
        {
            targetPosition = new Vector3(negativeXLimit, targetPosition.y, targetPosition.z);
        }

        if (targetPosition.y > positiveYLimit)
        {
            targetPosition = new Vector3(targetPosition.x, positiveYLimit, targetPosition.z);
        }
        else if (targetPosition.y < negativeYLimit)
        {
            targetPosition = new Vector3(targetPosition.x, negativeYLimit, targetPosition.z);
        }
    }
}
