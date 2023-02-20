using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using EnumsAndStructs;

public class PopUpNotificationManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmpNotificationText;
    [SerializeField] GameObject inputField;
    [SerializeField] Button btnConfirm, btnBack;

    SavingsManager savingsManager;

    [HideInInspector] public string lastTextInserted;

    private void Awake()
    {
        btnBack.onClick.AddListener(OnClickConfirmAndBack);

        savingsManager = FindObjectOfType<SavingsManager>();
    }

    public void RequestToPlayer(string question, UnityAction onClickConfirmEvent)
    {
        RequestToPlayer(question, onClickConfirmEvent, false);
    }
    public void RequestToPlayer(string question, UnityAction onClickConfirmEvent, bool isInputTextNeeded)
    {
        AudioManager.instance.PlaySound(AudioClipsNames.tutorialMessage);

        gameObject.SetActive(true);

        tmpNotificationText.text = question;
        
        inputField.SetActive(isInputTextNeeded);

        btnConfirm.onClick.RemoveAllListeners();

        if (isInputTextNeeded)
        {
            btnConfirm.onClick.AddListener(SaveTextOfInputField);
        }

        btnConfirm.onClick.AddListener(OnClickConfirmAndBack);
        btnConfirm.onClick.AddListener(onClickConfirmEvent);
    }

    void SaveTextOfInputField()
    {
        lastTextInserted = inputField.GetComponent<TMP_InputField>().text;
        inputField.GetComponent<TMP_InputField>().text = "";
    }

    void OnClickConfirmAndBack()
    {
        AudioManager.instance.PlaySound(AudioClipsNames.buttonClick);

        gameObject.SetActive(false);
    }
}
