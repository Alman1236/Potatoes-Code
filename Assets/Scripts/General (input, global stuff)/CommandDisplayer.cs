using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Attached to a gameobject with tmp and sprite renderer components, 
/// this updates the text to say which key needs to be pressed (on keyboard or gamepad) 
/// </summary>
public class CommandDisplayer : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    TextMeshProUGUI tmp;

    [SerializeField] string keyToShowWhileUsingKeyboard;
    [SerializeField] Sprite keyToShowWhileUsingPlayStationJoystick;
    [SerializeField] Sprite keyToShowWhileUsingXBoxGamepad;

    public enum controlsUsed
    {
        keyboardAndMouse,
        joystick,
        gamepad
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        if(spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on " + gameObject.name,gameObject);
        }
        
        tmp = GetComponent<TextMeshProUGUI>();

        if (tmp == null)
        {
            Debug.LogError("TextMeshProUGUI component not found on " + gameObject.name, gameObject);
        }
    }

    void UpdateControlsUsed(controlsUsed controlsUsed)
    {
        switch (controlsUsed)
        {
            case controlsUsed.gamepad:
                tmp.text = "";
                spriteRenderer.sprite = keyToShowWhileUsingXBoxGamepad;
                break;

            case controlsUsed.keyboardAndMouse:
                tmp.text = keyToShowWhileUsingKeyboard;
                spriteRenderer.sprite = null;
                break;

            case controlsUsed.joystick:
                tmp.text = "";
                spriteRenderer.sprite = keyToShowWhileUsingPlayStationJoystick;
                break;
        }
    }
}
