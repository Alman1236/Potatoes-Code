using UnityEngine;
using EnumsAndStructs;

public class MovementManager : MonoBehaviour
{
    [HideInInspector] public bool isMoving = false;

    //it can be a number between -1 and 1 based on the position of the joypad stick
    float speedMultiplier;

    PhasesManager phasesManager;
    Character me;

    [SerializeField] ParticleSystem dustMoved;

    bool isStopRecorded = false;

    private void Start()
    {
        phasesManager = PhasesManager.instance;
        me = GetComponent<Character>();
    }

    public void OnResettingCharacter()
    {
        speedMultiplier = 0;
        isMoving = false;
        isStopRecorded = false;
    }

    public void UpdateMovement(float xMovement)
    {
        speedMultiplier = xMovement;

        UpdateRotation();

        if (xMovement > .1 || xMovement < -.1)
        {
            isMoving = true;
            PlayStepsSound();
            phasesManager.OnSendingCommandToCharacter();
        }
        else
        {
            isMoving = false;
            speedMultiplier = 0;

            if (!isStopRecorded)
            {
                me.actionsRecorder.RecordAction(actionTypes.movement, 0, transform.position);
                isStopRecorded = true;
            }

        }

        if (CharacterSelector.instance.selectedCharacter == me)
        {
            me.linker.UpdateMovementOfLinkedCharacter(xMovement);
        }
    }

    void UpdateRotation()
    {
        if (!me.deathManager.isDead && !me.freezingManager.isFrozen)
        {
            if (speedMultiplier > 0 && !isWatchingRight())
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (speedMultiplier < 0 && isWatchingRight())
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    void PlayStepsSound()
    {
        if (!me.deathManager.isDead && !me.freezingManager.isFrozen)
        {
            if (!me.jumpManager.isInAir)
            {
                me.audioManager.PlayStepsSound(true);
            }
            else
            {
                me.audioManager.PlayStepsSound(false);
            }
        }
        else
        {
            me.audioManager.PlayStepsSound(false);
        }
    }

    private void FixedUpdate()
    {
        if (!me.deathManager.isDead && !me.freezingManager.isFrozen)
        {
            Move();
        }
    }

    public double error = 0;
    void Move()
    {
        if (Data.currentPhase == Phases.win)
        {
            speedMultiplier = 0;
        }

        if (isMoving && speedMultiplier != 0 && !me.freezingManager.isFrozen)
        {
            if (!ImPushingMoreThan1Character())
            {
                Vector3 movement = Vector3.right * Data.CharacterSpeed * speedMultiplier * Time.fixedDeltaTime;

                transform.Translate(movement, Space.World);
            }

            me.actionsRecorder.RecordAction(actionTypes.movement, speedMultiplier, transform.position);
            isStopRecorded = false;

            me.animationsManager.DoAnimation(Animations.run);

            if (!dustMoved.isEmitting && !me.jumpManager.isInAir)
            {
                dustMoved.Play();
            }
        }
        else
        {
            me.animationsManager.DoAnimation(Animations.idle);
            me.audioManager.PlayStepsSound(false);
        }


    }

    bool isWatchingRight()
    {
        if (transform.rotation.eulerAngles.y == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool ImPushingMoreThan1Character()
    {
        RaycastHit2D[] charactersImPushing;
        byte charactersImPushingCount = 0;

        if (isWatchingRight())
        {
            //todo: this won't work if you change colliders
            charactersImPushing = Physics2D.RaycastAll(transform.position, Vector2.right, GetComponent<BoxCollider2D>().bounds.extents.x * 3.25f);
        }
        else
        {
            charactersImPushing = Physics2D.RaycastAll(transform.position, Vector2.left, GetComponent<BoxCollider2D>().bounds.extents.x * 3.25f);
        }

        for (byte i = 0; i < charactersImPushing.Length; i++)
        {
            if (charactersImPushing[i].collider.gameObject.TryGetComponent(out Character character) && character != me)
            {
                charactersImPushingCount++;
            }
        }

        if (charactersImPushingCount < 2)
        {
            return false;
        }
        else
        {
            return true;
        }


    }
}
