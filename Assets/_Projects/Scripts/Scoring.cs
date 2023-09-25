using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    private int score;

    [Header("UI Update")]
    [SerializeField] private TextMeshProUGUI textScore;
    public void UpdateScore(int _scoreAdd)
    {
        score += _scoreAdd;
        textScore.text = score.ToString();
    }
}
