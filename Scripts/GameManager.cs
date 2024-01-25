using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] public GameObject lapsScreen;
    [SerializeField] private float health = 1;
    public static GameManager instance;

    private bool isPaused;

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
    /// Check if the player pauses the game
    /// or dies and do the stuff thats with it
    /// </summary>
    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (Input.GetKeyDown(KeyCode.Escape) && currentScene.name == "RaceBaanV2" && pauseScreen != null)
        {
            if (isPaused == false)
            {
                pauseScreen.SetActive(true);
                isPaused = true;
                Time.timeScale = 0f;
            }
            else if(isPaused == true)
            {
                pauseScreen.SetActive(false);
                isPaused = false;
                Time.timeScale = 1f;
            }
        }

        if (health < 1 && gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    /// <summary>
    /// Update the playerHealth
    /// </summary>
    /// <param name="damage">The damage the player receives</param>
    public void UpdateHealth(float damage)
    {
        health += damage;
    }

    /// <summary>
    /// Quick function to easily get the health of the player
    /// </summary>
    /// <returns>The current health of the player</returns>
    public float GetHealth()
    {
        return health;
    }

    /// <summary>
    /// Restart the current level
    /// </summary>
    public void RestartLVL()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ResetStats();
    }

    /// <summary>
    /// Reset the TimeScale so the game continues
    /// </summary>
    public void ResetTimeScale()
    {
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Reset the Stats
    /// </summary>
    public void ResetStats()
    {
        health = 1;
        Time.timeScale = 1f;
    }
}
