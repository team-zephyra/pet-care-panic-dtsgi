using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsPaused { get; set; }

    private void Awake()
    {
        // Ensure there is only one instance of GameManager.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensure GameManager persists across scenes.
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PauseGame()
    {
        IsPaused = true;
        Time.timeScale = 0f;
        // Other pause-related actions...
    }

    public void ResumeGame()
    {
        IsPaused = false;
        Time.timeScale = 1f;
        // Other resume-related actions...
    }

    

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
