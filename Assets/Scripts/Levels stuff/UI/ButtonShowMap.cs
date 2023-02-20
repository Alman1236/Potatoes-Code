using UnityEngine;
using EnumsAndStructs;
using UnityEngine.UI;
using TMPro;

public class ButtonShowMap : MonoBehaviour
{
    CameraBehaviour cam;

    [SerializeField] GameObject[] whatToTurnOffWhileSeeingMap;
    [SerializeField] GameObject pnlInfo;
    [SerializeField] GameObject pnlShowingMap;

    public bool HideBtnShowMap = true;

    private void Awake()
    {
        if (HideBtnShowMap)
        {
            gameObject.SetActive(false);
        }
        else
        {
            cam = Camera.main.GetComponent<CameraBehaviour>();
        }
    }

    public void OnClickBtnShowMap()
    {
        AudioManager.instance.PlaySound(AudioClipsNames.buttonClick);

        ShowMap();
    }

    public void OnClickStopShowingMap()
    {
        AudioManager.instance.PlaySound(AudioClipsNames.buttonClick);

        StopShowingMap();
    }

    public void OnPointerEnter()
    {
        pnlInfo.SetActive(true);
        pnlInfo.GetComponentInChildren<TextMeshProUGUI>().text = GameTexts.GetTranslatedText(textsNames.pnlInfo_freeCamera);
    }

    void ShowMap()
    {
        pnlShowingMap.SetActive(true);
        pnlShowingMap.GetComponentInChildren<TextMeshProUGUI>().text = GameTexts.GetTranslatedText(textsNames.freeCameraControls);
        pnlInfo.SetActive(false);
        Data.SetCurrentPhase(Phases.exploringMap);
        cam.FreeCamera(true);

        for (byte i = 0; i < whatToTurnOffWhileSeeingMap.Length; i++)
        {
            whatToTurnOffWhileSeeingMap[i].SetActive(false);
        }

        Button btnShowMap = gameObject.GetComponent<Button>();

        btnShowMap.onClick.RemoveAllListeners();
        btnShowMap.onClick.AddListener(OnClickStopShowingMap);

        pnlInfo.SetActive(false);
    }

    void StopShowingMap()
    {
        pnlShowingMap.SetActive(false);
        pnlInfo.SetActive(false);
        Data.SetCurrentPhase(Phases.preparation);
        cam.FreeCamera(false);

        for (byte i = 0; i < whatToTurnOffWhileSeeingMap.Length; i++)
        {
            whatToTurnOffWhileSeeingMap[i].SetActive(true);
        }

        Button btnShowMap = gameObject.GetComponent<Button>();

        btnShowMap.onClick.RemoveAllListeners();
        btnShowMap.onClick.AddListener(OnClickBtnShowMap);
        pnlInfo.SetActive(false);
    }

    public void ShowOrUnshowMap()
    {
        if (pnlShowingMap.activeSelf)
        {
            StopShowingMap();
        }
        else
        {
            ShowMap();
        }
    }
}
