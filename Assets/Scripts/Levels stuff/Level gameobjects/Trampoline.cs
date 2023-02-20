using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    Coroutine pressionRoutine, blockedTrampolineRoutine;
    SpriteRenderer spriteRenderer;

    Sprite trampoline;
    [SerializeField] Sprite pressedTrampoline;

    bool isTrampolineBlocked = false;
    sbyte peopleOnTrampoline = 0; 

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        trampoline = spriteRenderer.sprite;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            OnPlayerJumpOnTrampoline(collision.GetComponent<Character>());
        }
    }

    void OnPlayerJumpOnTrampoline(Character character)
    {
        character.jumpManager.Jump(usingTrampoline: true);    

        if (pressionRoutine != null)
        {
            StopCoroutine(pressionRoutine);
        }
        pressionRoutine = StartCoroutine(DoPressionAnimation());

        peopleOnTrampoline++;
        
        if(peopleOnTrampoline == 1)
        {
           blockedTrampolineRoutine = StartCoroutine(StartBlockingTrampoline());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            peopleOnTrampoline--;

            if(peopleOnTrampoline == 0)
            {
                StopCoroutine(blockedTrampolineRoutine);
                isTrampolineBlocked = false;

                if(pressionRoutine == null)
                {
                    spriteRenderer.sprite = trampoline;
                }
            }
        }
    }

    IEnumerator StartBlockingTrampoline()
    {
        yield return new WaitForSeconds(0.2f);

        spriteRenderer.sprite = pressedTrampoline;
        isTrampolineBlocked = true;
    }

    IEnumerator DoPressionAnimation()
    {
        spriteRenderer.sprite = pressedTrampoline;

        yield return new WaitForSeconds(0.2f);

        spriteRenderer.sprite = trampoline;
    }
}
