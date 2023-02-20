using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumsAndStructs;
using UnityEngine.EventSystems;

public class MainMenuButtonsFunctions : MonoBehaviour
{
    [SerializeField] GameObject pnlMainMenu;

    [SerializeField] GameObject pnlPlay;
    [SerializeField] GameObject pnlSavings;
    [SerializeField] GameObject pnlOptions;

    //[SerializeField] GameObject mainMenuFirstSelectedItem, savingsMenuFirstSelectedItem, optionsMenuFirstSelectedItem, playMenuFirstSelectedItem;
    //GameObject lastSelectedItem;

    void Awake()
    {
        //var myAction = new InputAction(binding: "/*/<button>");
        //myAction.performed += AnyKeyPressed;
        //myAction.Enable();
    }

    //void AnyKeyPressed(InputAction.CallbackContext context)
    //{
        /*if (context.control.device.name == "Mouse" || context.control.device.name == "Keyboard")
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            if(EventSystem.current.currentSelectedGameObject == null)
            {
                SelectFirstItemInMenu(lastSelectedItem);
            } 
        }*/
    //}

    private void Start()
    {
        pnlSavings.SetActive(false);
        //lastSelectedItem = mainMenuFirstSelectedItem;
    }

    //next two methods are called by every button
    public void OnClickButton()
    {
        AudioManager.instance.PlaySound(AudioClipsNames.buttonClick);
    }
    public void OnPointerOverButton()
    {
        AudioManager.instance.PlaySound(AudioClipsNames.mouseOverButton);
    }


    public void OnClickPlay()
    {
        pnlMainMenu.SetActive(false);
        pnlSavings.SetActive(true);

        //SelectFirstItemInMenu(savingsMenuFirstSelectedItem);
    }

    public void OnClickProfileButton()
    {
        pnlSavings.SetActive(false);
        pnlPlay.SetActive(true);
        //SelectFirstItemInMenu(playMenuFirstSelectedItem);
    }

    public void OnClickOptions()
    {
        pnlMainMenu.SetActive(false);
        pnlOptions.SetActive(true);
        //SelectFirstItemInMenu(optionsMenuFirstSelectedItem);
    }

    public void OnClickQuit()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

    //called by every back button in main menu
    public void OnClickBack()
    {
        pnlMainMenu.SetActive(true);
        //SelectFirstItemInMenu(mainMenuFirstSelectedItem);

        pnlOptions.SetActive(false);
        pnlSavings.SetActive(false);
    }

    public void OnClickBackFromPlayMenu()
    {
        pnlSavings.SetActive(true);
        pnlPlay.SetActive(false);
        //SelectFirstItemInMenu(savingsMenuFirstSelectedItem);
    }

    void SelectFirstItemInMenu(GameObject item)
    {
        /*lastSelectedItem = item;

        if (Gamepad.current != null || Joystick.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(item);
        }
        else
        {
            Debug.Log("Gamepad not found");
        }*/
    }
    
}
