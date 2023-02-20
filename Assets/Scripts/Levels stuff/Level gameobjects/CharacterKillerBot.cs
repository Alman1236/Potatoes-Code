using UnityEngine;
using EnumsAndStructs;

public class CharacterKillerBot : MonoBehaviour, IResettable
{
    [SerializeField] Transform[] targetsToKill;
    [SerializeField] GameObject redLight;
    byte targetImFollowing;

    Rigidbody2D myRigidbody;

    Vector3 startingPosition;
    Quaternion startingRotation;

    bool isBroken = false;

    void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        startingPosition = transform.position;
        startingRotation = transform.rotation;

        myRigidbody = GetComponent<Rigidbody2D>();

        if (targetsToKill.Length == 0)
        {
            Debug.LogError("Bot has no targets!", gameObject);
            Debug.Break();
        }
    }

    void IResettable.Reset()
    {
        transform.SetPositionAndRotation(startingPosition, startingRotation);

        myRigidbody.velocity = Vector2.zero;

        targetImFollowing = 0;

        GetComponent<CircleCollider2D>().enabled = true;
        isBroken = false;

        myRigidbody.gravityScale = 0;
        redLight.SetActive(true);
    }

    void FixedUpdate()
    {
        if (targetsToKill[targetImFollowing].GetComponent<CharacterDeath>().isDead)
        {
            KillNextTarget();
        }

        if (Data.currentPhase == Phases.recording && !isBroken)
        {
            RotateTowardTarget();
            MoveTowardTarget();
        }
    }

    void RotateTowardTarget()
    {
        //direction = targetImFollowing.position - myRigidbody.position
        Vector2 lookDirection = new Vector2(targetsToKill[targetImFollowing].position.x, targetsToKill[targetImFollowing].position.y) - myRigidbody.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + 270;

        myRigidbody.rotation = angle;
    }

    void MoveTowardTarget()
    {
        Vector2 targetPos = new Vector2(targetsToKill[targetImFollowing].position.x, targetsToKill[targetImFollowing].position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPos, Data.BotSpeed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isBroken && collision.gameObject.TryGetComponent(out Character target))
        {
            target.deathManager.Die(deathCause.bot);
        }
    }

    void KillNextTarget()
    {
        if (ThereIsAnotherTarget())
        {
            targetImFollowing++;
        }
        else
        {
            if (!isBroken)
            {
                myRigidbody.gravityScale = -1.5f;
            }  
        }
    }

    bool ThereIsAnotherTarget()
    {
        if (targetImFollowing < targetsToKill.Length - 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Destroy()
    {
        //do animation
        AudioManager.instance.PlaySound(AudioClipsNames.botHit);
        AudioManager.instance.PlaySound(AudioClipsNames.botDestroyed);

        isBroken = true;
        redLight.SetActive(false);
        GetComponent<CircleCollider2D>().enabled = false;
        myRigidbody.gravityScale = 1;
    }
}
