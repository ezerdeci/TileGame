using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private static int score;
    private static int coinEarned;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI finalScore;

    void Start()
    {
        score = 0;
        coinEarned = 0;
        scoreText.text = score.ToString();
        coinText.text = coinEarned.ToString();
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
        coinText.text = coinEarned.ToString();
        finalScore.text = "Your Score: " + score.ToString();
    }

    public static void increaseScore(int increment)
    {
        score += increment;
    }

    public int getScore()
    {
        return score;
    }

    public static void increaseEarnedCoin(int increment)
    {
        coinEarned += increment;
    }

    public static int getCoinEarned()
    {
        return coinEarned;
    }
}
