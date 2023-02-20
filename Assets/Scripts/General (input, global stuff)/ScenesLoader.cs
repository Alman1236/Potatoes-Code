using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using EnumsAndStructs;

public class ScenesLoader : MonoBehaviour
{
    static public ScenesLoader instance;

    [SerializeField] GameObject loadingScreen;
    int mainMenuSceneIndex;

    [SerializeField] GameObject musicManagerPrefab;
    GameObject musicManager;

    ushort lastLoadedLevel;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        GenerateInstance();
        mainMenuSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void InstantiateLoadingScreen()
    {
        GameObject go = Instantiate(loadingScreen);
        DontDestroyOnLoad(go);
    }

    void InstantiateMusicManager()
    {
        musicManager = Instantiate(musicManagerPrefab);
        DontDestroyOnLoad(musicManager);
    }

    void GenerateInstance()
    {
        if (instance == null)
        {
            instance = this;

            InstantiateLoadingScreen();
            InstantiateMusicManager();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadMainMenu()
    {
        LoadLevel(mainMenuSceneIndex);
    }

    public void ReloadLevel()
    {
        LoadLevel(lastLoadedLevel);
    }

    public void LoadNextLevel()
    {
        if (lastLoadedLevel + 1 <= DemoData.GetHighestPlayableLevel())
        {
            LoadLevel(lastLoadedLevel + 1);
        }
        else
        {
            LoadMainMenu();
        }
    }

    public void LoadLevel(int level)
    {
        StartCoroutine(LoadScene(level));
        lastLoadedLevel = (ushort)level;
    }

    IEnumerator LoadScene(int index)
    {
        LoadingScreen.loadingScreen.SetActive(true);
        AsyncOperation loading = SceneManager.LoadSceneAsync(index);

        while (!loading.isDone)
        {
            float progress = Mathf.Clamp01(loading.progress / .9f);
            LoadingScreen.UpdateLoadingBar(progress);

            yield return new WaitForEndOfFrame();
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        LoadingScreen.loadingScreen.SetActive(false);
        Data.OnChangingScene();

        if (musicManager != null)
            musicManager.GetComponent<MusicManager>().OnSceneLoaded();
    }
}
