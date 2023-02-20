using System.Collections.Generic;
using UnityEngine;
using EnumsAndStructs;

public class ActionsRecorder : MonoBehaviour
{
    CharacterSelector characterSelector;
    Character me;

    Vector3 startingPosition;
    Quaternion startingRotation;

    Queue<action> todoActions = new Queue<action>();
    public Queue<action> GetToDoActions()
    {
        action[] todoActionsCopy = this.todoActions.ToArray();
        Queue<action> todoActions = new Queue<action>();

        for (uint i = 0; i < todoActionsCopy.Length; i++)
        {
            todoActions.Enqueue(todoActionsCopy[i]);
        }

        todoActions.Enqueue(new action(actionTypes.movement,Data.actualTick, 0, Vector3.zero));
        return todoActions;
    }

    public bool hasRecordedBehaviour { get; private set; } = false;

    private void Start()
    {
        InitializeVariables();
    }

    void InitializeVariables()
    {
        characterSelector = CharacterSelector.instance;
        me = GetComponent<Character>();

        startingPosition = transform.position;
        startingRotation = transform.rotation;
    }

    public void OnResettingCharacter()
    {
        transform.SetParent(null);
        transform.SetPositionAndRotation(startingPosition, startingRotation);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    public void ClearRecordedActions()
    {
        //Debug.Log(gameObject.name + ": cleared recorded actions"); 
        todoActions.Clear();
        hasRecordedBehaviour = false;

        if (Data.currentPhase == Phases.preparation)
        {
            me.animationsManager.DoAnimation(Animations.idle);
        }
    }

    public void RecordAction(actionTypes action, float associatedValue, Vector3 associatedVector3)
    {
        if (CanRecordAction())
        {
            action executedAction = new action(action, Data.actualTick, associatedValue, associatedVector3);

            todoActions.Enqueue(executedAction);

            hasRecordedBehaviour = true;
        }
    }

    bool CanRecordAction()
    {
        bool imLinkedToSelectedCharacter = false;

        if (characterSelector.selectedCharacter.linker.isLinkedToAnotherCharacter)
        {
            if(characterSelector.selectedCharacter.linker.linkedCharacter == me)
            {
                imLinkedToSelectedCharacter = true;
            }
        }

        if ((characterSelector.selectedCharacter == me || imLinkedToSelectedCharacter) && Data.currentPhase == Phases.recording)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}