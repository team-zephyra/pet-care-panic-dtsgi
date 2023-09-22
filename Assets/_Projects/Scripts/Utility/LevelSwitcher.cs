using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitcher : MonoBehaviour
{
    public void LoadSceneBySceneName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        GameManager.Instance.ResumeGame();
    }

    public void LoadSceneBySceneBuildIndex(int sceneBuildIndex)
    {
        SceneManager.LoadScene(sceneBuildIndex);
        GameManager.Instance.ResumeGame();
    }
}
