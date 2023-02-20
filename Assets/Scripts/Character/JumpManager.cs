using System.Collections;
using UnityEngine;
using EnumsAndStructs;
using System;

public class JumpManager : MonoBehaviour
{
    //Logic
    bool canJump = true;
    bool wantToJump = false;

    Coroutine resetWantToJumpRoutine;

    bool playedTheHitGroundSound = true;

    //References
    Rigidbody2D myRigidBody;
    PhasesManager phasesManager;
    Character me;

    [SerializeField] Transform feetPos;
    [SerializeField] ParticleSystem jumpDust;
    [SerializeField] GameObject shadow;

    //public
    [HideInInspector] public bool isInAir = false;

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        me = GetComponent<Character>();

        phasesManager = PhasesManager.instance;

    }

    void Update()
    {
        CheckGround();
    }

    public void OnResettingCharacter()
    {
        wantToJump = false;
        myRigidBody.velocity = Vector2.zero;
    }

    public void TryToJump()
    {
        if (CharacterSelector.instance.selectedCharacter == me)
        {
            me.linker.MakeLinkedCharacterJump();
        }

        if (!me.deathManager.isDead && !me.freezingManager.isFrozen)
        {
            if (canJump)
            {
                Jump(usingTrampoline: false);
            }
            else
            {
                wantToJump = true;

                if (resetWantToJumpRoutine != null)
                {
                    StopCoroutine(resetWantToJumpRoutine);
                }
                resetWantToJumpRoutine = StartCoroutine(ResetWantToJump());
            }
        }
    }

    public void Jump(bool usingTrampoline)
    {
        AddVerticalForce(usingTrampoline);
        OnJumping();

        me.animationsManager.DoAnimation(Animations.jump);
        jumpDust.Play();
        shadow.SetActive(false);

        if (usingTrampoline)
        {
            me.audioManager.PlaySound(AudioClipsNames.jumpWithTrampoline);
        }
        else
        {
            phasesManager.OnSendingCommandToCharacter();
            me.actionsRecorder.RecordAction(actionTypes.jump, 0, Vector3.zero);
            me.audioManager.PlaySound(AudioClipsNames.jump);
        }
    }

    void OnJumping()
    {
        canJump = false;
        isInAir = true;
        wantToJump = false;
        playedTheHitGroundSound = false;
    }

    void AddVerticalForce(bool usingTrampoline)
    {
        Vector2 jumpForce = new Vector2(0, Data.JumpForce);

        if (usingTrampoline)
        {
            jumpForce.y = Data.JumpForceWithTrampoline;
        }

        //myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, 0);
        myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce.y);
        //myRigidBody.AddForce(jumpForce, ForceMode2D.Impulse);
    }

    void CheckGround()
    {
        if (Mathf.Abs(myRigidBody.velocity.y) < 0.13f && ImOnSomething())
        {
            OnTouchingGround();
        }
        else
        {
            canJump = false;
        }

    }

    bool ImOnSomething()
    {
        byte i = 0;
        bool imSteppingOnASurface = false;
        Collider2D[] whatImOn = Physics2D.OverlapBoxAll(feetPos.position, new Vector2(1.5f, 0.3f), 0);

        while (i < whatImOn.Length && !imSteppingOnASurface)
        {
            //if i'm stepping on something that is not me
            if (whatImOn[i] != null && whatImOn[i].gameObject != gameObject)
            {
                imSteppingOnASurface = true;
            }

            i++;
        }

        if (imSteppingOnASurface)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnTouchingGround()
    {
        canJump = true;
        isInAir = false;
        shadow.SetActive(true);

        if (!playedTheHitGroundSound)
        {
            playedTheHitGroundSound = true;

            if (Data.currentPhase != Phases.resetting)
            {
                me.audioManager.PlaySound(AudioClipsNames.touchedGroundAfterJump);
            }
        }

        if (wantToJump)
        {
            TryToJump();
        }
    }

    IEnumerator ResetWantToJump()
    {
        yield return new WaitForSeconds(Data.JumpCommandMaxAnticipation);

        wantToJump = false;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;

        //Gizmos.DrawCube(feetPos.position, new Vector2(1.5f, 0.3f));
    }
}
