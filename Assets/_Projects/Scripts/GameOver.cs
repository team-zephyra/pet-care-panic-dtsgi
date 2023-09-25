using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;

    public void GameOverTrigger()
    {
        gameOverPanel.SetActive(true);

        // Do Score Calculation Here

        //
    }
}
