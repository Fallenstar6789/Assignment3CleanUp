using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChange : MonoBehaviour
{
    public static SceneChange instance;
    public GameObject loadingScreen; // Assign in Inspector
    public float loadingDelay = 2.5f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NextLevel()
    {
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            StartCoroutine(LoadSceneWithLoading(nextIndex));
        }
        else
        {
            Debug.LogWarning("No next scene exists!");
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneWithLoading(sceneName));
    }

    private IEnumerator LoadSceneWithLoading(int sceneIndex)
    {
        if (loadingScreen) loadingScreen.SetActive(true);

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneIndex);
        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            yield return null;
        }

        yield return new WaitForSeconds(loadingDelay); // Show loading screen even after load
        async.allowSceneActivation = true;
    }

    private IEnumerator LoadSceneWithLoading(string sceneName)
    {
        if (loadingScreen) loadingScreen.SetActive(true);

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            yield return null;
        }

        yield return new WaitForSeconds(loadingDelay); // Keep loading screen visible briefly
        async.allowSceneActivation = true;
    }

    private void  OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Always turn off the loading screen when the scene is fully loaded
        if (loadingScreen) loadingScreen.SetActive(false);

        int index = scene.buildIndex;
        if (index == 1 || index == 2 || index ==3 || index == 0)
        {
            RPGGameManager gameManager = FindObjectOfType<RPGGameManager>();
            if (gameManager != null)
            {
                gameManager.SetupScene();
            }
        }
    }
}