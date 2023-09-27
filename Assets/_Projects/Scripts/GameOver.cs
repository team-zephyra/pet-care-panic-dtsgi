using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [Header("UI Properties")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI[] textMinimumScore;
    [SerializeField] private GameObject[] starImg;
    [SerializeField] private Image winImage;

    [Header("Setup")]
    [SerializeField] private Sprite spriteWin;
    [SerializeField] private Sprite spriteLose;
    private int minScore1;
    private int minScore2;
    private int minScore3 = 999999;
    private int playerScore = 0;

    public void GameOverTrigger()
    {
        playerScore = GameManager.instance.GetGameScore();
        bool win = false;
        // Do Score Calculation Here
        if(playerScore >= minScore1) { starImg[0].SetActive(true); win = true; }
        if (playerScore >= minScore2) { starImg[1].SetActive(true); }
        if(playerScore >= minScore3) { starImg[2].SetActive(true); }
        
        // Player Score
        textScore.text =playerScore.ToString();

        if(win)
        {
            winImage.sprite = spriteWin;
        }
        else
        {
            winImage.sprite = spriteLose;
        }

        gameOverPanel.SetActive(true);
    }

    //Set Minimum Score OnStart
    public void SetMinimumScore(int _star1, int _star2, int _star3)
    {
        minScore1 = _star1;
        minScore2 = _star2;
        minScore3 = _star3;

        textMinimumScore[0].text = _star1.ToString();
        textMinimumScore[1].text = _star2.ToString();
        textMinimumScore[2].text = _star3.ToString();
    }

    public void BackToMainMenu()
    {
        GameManager.instance.BackToMainMenu();
    }

    public void RestartLevel()
    {
        GameManager.instance.RestartLevel();
    }
}
