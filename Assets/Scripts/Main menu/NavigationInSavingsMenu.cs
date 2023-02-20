using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class NavigationInSavingsMenu : MonoBehaviour
{
    /*InputActions inputActions;
    InputAction axis;

    [SerializeField] GameObject savingsMenu;

    [SerializeField] GameObject btnSaving1, btnSaving2, btnSaving3, btnDeleteSaving1, btnDeleteSaving2, btnDeleteSaving3, btnBack;

    string currentlySelectedButtonName = "btn Profile 1";

      private void Awake()
      {
          inputActions = new InputActions();
          axis = inputActions.Mainmenu.NavigateInSavingsMenu;
          axis.Enable();
      }

      private void OnDisable()
      {
          inputActions.Disable();
      }

      void Update()
      {
          if (savingsMenu.activeSelf)
          {
              SelectNewItem(axis.ReadValue<Vector2>());
          } 
      }

      void SelectNewItem(Vector2 axis)
      {
          if (axis.x > 0.1f)
          {
              if(currentlySelectedButtonName == btnSaving1.gameObject.name)
              {
                  SelectGameobject(btnSaving2);
              }

              if (currentlySelectedButtonName == btnDeleteSaving1.gameObject.name)
              {
                  SelectGameobject(btnSaving2);
              }

              if (currentlySelectedButtonName == btnSaving2.gameObject.name)
              {
                  SelectGameobject(btnSaving3);
              }

              if (currentlySelectedButtonName == btnSaving3.gameObject.name)
              {
                  SelectGameobject(btnBack);
              }
          }
          else if(axis.x < -0.1f)
          {
              if (currentlySelectedButtonName == btnSaving1.gameObject.name)
              {
                  if (btnDeleteSaving1.activeSelf)
                  {
                      SelectGameobject(btnDeleteSaving1);
                  }
              }
              else if(currentlySelectedButtonName == btnDeleteSaving1.gameObject.name)
              {
                  SelectGameobject(EventSystem.current.transform.parent.gameObject);
              }
          }
      }

      void SelectGameobject(GameObject go)
      {
          EventSystem.current.SetSelectedGameObject(null);
          EventSystem.current.SetSelectedGameObject(go);
          currentlySelectedButtonName = go.name;
      }*/
}
