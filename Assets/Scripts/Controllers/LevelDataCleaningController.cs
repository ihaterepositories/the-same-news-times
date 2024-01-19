using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDataCleaningController : MonoBehaviour
{
    private LevelDataCleaningController _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
        CleanSavedData();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu")
        {
            CleanSavedData();
        }
    }

    private void CleanSavedData()
    {
        PlayerPrefs.SetInt("GreenScore", 0);
        PlayerPrefs.SetInt("PinkScore", 0);
        PlayerPrefs.SetFloat("GameDuration", 0f);
        Debug.Log("- saved data cleaned");
    }
}
