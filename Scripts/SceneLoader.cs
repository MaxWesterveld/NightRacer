using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject sceneLoader;
    public static SceneLoader instance;

    /// <summary>
    /// Making an singleton so its easily accessible
    /// and there is only one of
    /// </summary>
    private void Awake()
    {
        if (instance != this && instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    /// <summary>
    /// Load the game, the first level in this situation
    /// </summary>
    public void LoadGame()
    {
        SceneManager.LoadScene("RaceBaanV2", LoadSceneMode.Single);
    }


    /// <summary>
    /// Load the next level in the game
    /// </summary>
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Load the main Scene again
    /// </summary>
    public void LoadMainScreen()
    {
        SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
    }

    /// <summary>
    /// Quit the game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}