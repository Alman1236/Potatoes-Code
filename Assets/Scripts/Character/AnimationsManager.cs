using UnityEngine;
using EnumsAndStructs;
using System.Collections;

public class AnimationsManager : MonoBehaviour
{
    Animations currentAnimation;

    Character me;
    Animator animator;

    EyesManager eyes;

    Coroutine selectionRoutine;

    private void Awake()
    {
        me = GetComponent<Character>();
        animator = GetComponent<Animator>();
        eyes = GetComponentInChildren<EyesManager>();
    }

    public void OnResettingCharacter()
    {
        eyes.ResetEyes();
        DoIdleAnimation();
    }

    void DoIdleAnimation()
    {
        if(currentAnimation != Animations.selected)
        {
            if (me.actionsRecorder.hasRecordedBehaviour && (Data.currentPhase == Phases.preparation || Data.currentPhase == Phases.resetting))
            {
                animator.Play("idleRecordedBehaviour");
                currentAnimation = Animations.idleWithRecordedMovement;
                eyes.OnIdleWithRecordedBehaviour();
            }
            else
            {
                animator.Play("idle");
                currentAnimation = Animations.idle;
                eyes.ResetEyes();
            }
        }
    }

    public void DoAnimation(Animations animation)
    {
        if (!me.deathManager.isDead && currentAnimation != animation)
        {
            switch (animation)
            {
                case Animations.death:
                    animator.Play("death");
                    eyes.OnDeath();
                    currentAnimation = Animations.death;
                    break;

                case Animations.idle:
                    if (!me.jumpManager.isInAir)
                    {
                        DoIdleAnimation();
                    }
                    break;

                case Animations.run:

                    if (!me.jumpManager.isInAir && Data.currentPhase == Phases.recording)
                    {
                        animator.Play("run");
                        currentAnimation = Animations.run;
                        eyes.OnRunning();
                    }

                    break;

                case Animations.jump:
                    animator.Play("jump");
                    currentAnimation = Animations.jump;
                    eyes.OnJumping();
                    break;

                case Animations.selected:
                    if(selectionRoutine != null)
                    {
                        StopCoroutine(selectionRoutine);
                    }
                    selectionRoutine = StartCoroutine(DoSelectedAnimation());         
                    break;
            }
        }
    }

    IEnumerator DoSelectedAnimation()
    {
        currentAnimation = Animations.idle;
        eyes.OnSelect();

        yield return new WaitForSeconds(0.6f);

        currentAnimation = Animations.none;
    }
}
