using System.Collections.Generic;
using UnityEngine;
using EnumsAndStructs;

public class ActionsPlayer : MonoBehaviour
{
    Character me;

    bool isPlaying = false;

    Queue<action> todoActions = new Queue<action>();

    public Vector3 correctPosition;
    private void Awake()
    {
        me = GetComponent<Character>();
    }

    public void StartPlaying()
    {
        GetComponent<Rigidbody2D>().gravityScale = 10;
        todoActions = me.actionsRecorder.GetToDoActions();

        me.movement.error = 0;

        if (todoActions.Count > 0)
        {      
            isPlaying = true;
        }
    }

    public void StopPlaying()
    {
        isPlaying = false;
    }

    void Update()
    {
        if (isPlaying && !Data.isGamePaused && !me.deathManager.isDead)
        {
            Play();
        }
    }

    void Play()
    {
        bool executedAction;

        do
        {
            executedAction = false;

            if (todoActions.Count > 0)
            {
                action todoAction = todoActions.Peek();

                if (todoAction.executionTick <= Data.actualTick)
                {
                    ExecuteAction(todoAction);
                    todoActions.Dequeue();
                    executedAction = true;
                }
            }
            else
            {
                isPlaying = false;
            }

        } while (executedAction);
    }

    void ExecuteAction(action action)
    {
        switch (action.actionType)
        {
            case actionTypes.jump:
                me.jumpManager.TryToJump();
                break;

            case actionTypes.movement:
                me.movement.UpdateMovement(action.associatedValue);
                correctPosition = action.associatedVector;
                break;

            case actionTypes.shot:
                me.shootingManager.Shoot(action.associatedVector);
                break;

            case actionTypes.freezeOrUnfreeze:
                me.freezingManager.FreezeOrUnfreeze();
                break;
        }
    }
}
