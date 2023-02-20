using UnityEngine;
using Sirenix.OdinInspector;
using EnumsAndStructs;

public class PressurePlate : SerializedMonoBehaviour,IResettable
{
    [SerializeField] bool canBeDeactivatedOncePressed = true;
    [SerializeField] IActivable[] linkedObjects = new IActivable[0];

    byte numberCharactersOnPlate = 0;

    [SerializeField] Sprite pressedSprite;
    [SerializeField] Sprite notPressedSprite;

    SpriteRenderer mySpriteRenderer;

    void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Character"))
        {
            numberCharactersOnPlate++;

            if(numberCharactersOnPlate == 1)
            {
                PressPlate();
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (canBeDeactivatedOncePressed && collider.CompareTag("Character"))
        {
            numberCharactersOnPlate--;

            if (numberCharactersOnPlate == 0)
            {
                StopPressingPlate();
            }
        }
    }

    void PressPlate()
    {
        mySpriteRenderer.sprite = pressedSprite;

        AudioManager.instance.PlaySound(AudioClipsNames.pressurePlatePressed);

        for (byte i = 0; i < linkedObjects.Length; i++)
        {
            linkedObjects[i].Activate();
        }
    }

    void StopPressingPlate()
    {
        mySpriteRenderer.sprite = notPressedSprite;

        if(Data.currentPhase != Phases.resetting)
        {
            //AudioManager.instance.PlaySound(AudioClipsNames.pressurePlateNotPressed);
        }
       
        for (byte i = 0; i < linkedObjects.Length; i++)
        {
            linkedObjects[i].Deactivate();
        }
    }

    void IResettable.Reset()
    {
        mySpriteRenderer.sprite = notPressedSprite;
    }
}
