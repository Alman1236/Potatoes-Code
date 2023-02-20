using System.Collections;
using UnityEngine;
using EnumsAndStructs;

public class FallingPlatform : MonoBehaviour,IResettable
{
    Vector3 startingPos;
    Quaternion startingRotation;

    bool isFalling = false;

    bool fallingSoundPlayed = false;
    bool crackingSoundPlayed = false;

    [SerializeField] Collider2D myCollider;

    [SerializeField] float delay = 0.4f;

    Character lastCharacterOnPlatform;

    void Awake()
    {
        Initialize();
    }

    void FixedUpdate()
    {
        MoveDown();
    }

    void Initialize()
    {
        startingPos = transform.position;
        startingRotation = transform.rotation;
    }

    void IResettable.Reset()
    {
        fallingSoundPlayed = false;
        crackingSoundPlayed = false;
        transform.SetPositionAndRotation(startingPos, startingRotation);
        myCollider.enabled = true;
        isFalling = false;
        StopAllCoroutines();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            lastCharacterOnPlatform = collision.GetComponent<Character>();
            OnPlayerWalkOnPlatform();
        }
    }

    void OnPlayerWalkOnPlatform()
    {
        if (!crackingSoundPlayed)
        {
            crackingSoundPlayed = true;
            AudioManager.instance.PlaySound(AudioClipsNames.platformCracking);
        }

        StartCoroutine(FallAfterDelay());
    }

    IEnumerator FallAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        Fall();
    }

    void MoveDown()
    {
        if (isFalling)
        {
            transform.position += Vector3.down * Data.PlatformFallingSpeed * Time.fixedDeltaTime;
        }
    }

    void Fall()
    {
        if (!fallingSoundPlayed)
        {
            fallingSoundPlayed = true;
            AudioManager.instance.PlaySound(AudioClipsNames.platformFalling);
        }
        myCollider.enabled = false;
        isFalling = true;
    }
}
