using UnityEngine;
using Sirenix.OdinInspector;

public class CharactersLinker : MonoBehaviour
{
    public bool isLinkedToAnotherCharacter = false;

    [ShowIf("isLinkedToAnotherCharacter")] 
    public Character linkedCharacter;

    [SerializeField, ShowIf("isLinkedToAnotherCharacter")] 
    bool sendOppositeInputsToLinkedCharacter = true;

    public void UpdateMovementOfLinkedCharacter(float xMovement)
    {
        if (isLinkedToAnotherCharacter)
        {
            if (sendOppositeInputsToLinkedCharacter)
            {
                xMovement = -xMovement;
            }

            linkedCharacter.movement.UpdateMovement(xMovement);
        }
    }

    public void MakeLinkedCharacterJump()
    {
        if (isLinkedToAnotherCharacter)
        {
            linkedCharacter.jumpManager.TryToJump();
        }
    }
}
