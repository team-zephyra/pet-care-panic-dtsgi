using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInGame : MonoBehaviour
{
    public float countdownDuration = 60f; // Default countdown duration (1 minute)
    private float currentTimeLeft;

    public float TimeLeft => currentTimeLeft; // Public property to expose the time left.
    public GameObject gameOverScreen;

    public TextMeshProUGUI countdownText; // Reference to the TextMeshPro Text element.

    private void Start()
    {
        ResetCountdown(); // Initialize the countdown timer.
    }

    private void Update()
    {
        if (!GameManager.Instance.IsPaused)
        {
            // Update the countdown timer.
            if (currentTimeLeft > 0f)
            {
                currentTimeLeft -= Time.deltaTime;
                if (currentTimeLeft <= 0f)
                {
                    // Countdown timer reached zero; trigger game over.
                    GameOver();
                }
            }

            // Update the TextMeshPro Text element with mm:ss format.
            UpdateCountdownText();
        }
    }

    public void GameOver()
    {
        GameManager.Instance.IsPaused = true;
        Time.timeScale = 0f;

        gameOverScreen.SetActive(true);
    }

    private void ResetCountdown()
    {
        currentTimeLeft = countdownDuration;
        UpdateCountdownText(); // Update TextMeshPro Text element when resetting.
    }

    private void UpdateCountdownText()
    {
        if (countdownText != null)
        {
            // Convert timeLeft to TimeSpan and format as mm:ss.
            TimeSpan timeSpan = TimeSpan.FromSeconds(currentTimeLeft);
            string formattedTime = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);

            // Update the TextMeshPro Text element.
            countdownText.text = formattedTime;
        }
    }
}
