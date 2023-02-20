using UnityEngine;

public class EyesManager : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    [SerializeField] Sprite openEyes, deadEyes, happyEyes;

    [SerializeField] Vector3 defaultPos, idleWithRecordedBehaviourPos, runningPos, jumpPos;
    [SerializeField] Vector3 defaultRot, idleWithRecordedBehaviourRot, runningRot, jumpRot;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ResetEyes()
    {
        spriteRenderer.sprite = openEyes;
        transform.localPosition = defaultPos;
        transform.localRotation = Quaternion.Euler(defaultRot);
    }

    public void OnJumping()
    {
        spriteRenderer.sprite = openEyes;
        transform.localPosition = jumpPos;
        transform.localRotation = Quaternion.Euler(jumpRot);
    }

    public void OnRunning()
    {
        spriteRenderer.sprite = openEyes;
        transform.localPosition = runningPos;
        transform.localRotation = Quaternion.Euler(runningRot);
    }

    public void OnIdleWithRecordedBehaviour()
    {
        spriteRenderer.sprite = openEyes;
        transform.localPosition = idleWithRecordedBehaviourPos;
        transform.localRotation = Quaternion.Euler(idleWithRecordedBehaviourRot);
    }

    public void OnDeath()
    {
        spriteRenderer.sprite = deadEyes;
        transform.localPosition = defaultPos;
        transform.localRotation = Quaternion.Euler(defaultRot);
    }

    public void OnSelect()
    {
        spriteRenderer.sprite = happyEyes;
    }
}
