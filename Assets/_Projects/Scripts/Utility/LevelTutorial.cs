using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTutorial : MonoBehaviour
{

    private void Awake()
    {
        Time.timeScale = 0f;
    }

    public void ResetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }
}
