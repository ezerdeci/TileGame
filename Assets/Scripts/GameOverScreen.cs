using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverScreen;
    public static bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameOver() {
        gameOverScreen.SetActive(true);
        gameOver = true;
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        gameOverScreen.SetActive(false);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1;
        gameOver = false;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        gameOver = false;
        gameOverScreen.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
