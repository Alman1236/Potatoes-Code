using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    static public GameObject loadingScreen;
    static public Image loadingBar;

    [SerializeField] Image loadingBarReference;

    private void Awake()
    {
        GenerateReferences();
    }

    void GenerateReferences()
    {
        loadingScreen = gameObject;
        loadingBar = loadingBarReference;
        gameObject.SetActive(false);
    }

    static public void UpdateLoadingBar(float fillAmount)
    {
        loadingBar.fillAmount = fillAmount;
    }
}
