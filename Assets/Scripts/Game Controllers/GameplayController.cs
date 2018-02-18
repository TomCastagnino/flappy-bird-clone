using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{

    public static GameplayController instance;

    [SerializeField]
    private Text scoreText, endScore, bestScore, gameOverText;
    [SerializeField]
    private Button restartGameButton, instructionsButton, goToMenuButton;
    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private GameObject[] birds;
    [SerializeField]
    private Sprite[] medals;
    [SerializeField]
    private Image medalImage;

    void Awake()
    {
        MakeInstance();
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PauseGame()
    {
        if (BirdScript.instance != null && BirdScript.instance.isAlive)
        {
            pausePanel.SetActive(true);
            gameOverText.gameObject.SetActive(false);
            endScore.text = BirdScript.instance.score.ToString();
            bestScore.text = GameController.instance.GetHighscore().ToString();
            Time.timeScale = 0f;
            restartGameButton.onClick.RemoveAllListeners();
            restartGameButton.onClick.AddListener(() => ResumeGame());
            goToMenuButton.onClick.AddListener(() => GoToMenu());
        }
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GoToMenu()
    {
        pausePanel.SetActive(false);
        SceneFader.instance.FadeIn("MainMenu");
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        SceneFader.instance.FadeIn(SceneManager.GetActiveScene().name);
    }

    public void PlayGame()
    {
        scoreText.gameObject.SetActive(true);
        birds[GameController.instance.GetSelectedBird()].SetActive(true);
        instructionsButton.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void PlayerDiedShowScore(int score)
    {
        pausePanel.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
        endScore.text = score.ToString();
        if (score > GameController.instance.GetHighscore())
        {
            GameController.instance.SetHighscore(score);
        }
        bestScore.text = GameController.instance.GetHighscore().ToString();
        GiveMedals(score);
        restartGameButton.onClick.RemoveAllListeners();
        restartGameButton.onClick.AddListener(() => RestartGame());
        goToMenuButton.onClick.AddListener(() => GoToMenu());
    }

    private void GiveMedals(int score)
    {
        if (score <= 20)
        {
            medalImage.sprite = medals[0];
        }
        else if (score > 20 && score < 40)
        {
            medalImage.sprite = medals[1];
            GameController.instance.UnlockGreenBird();
        }
        else
        {
            medalImage.sprite = medals[2];
            GameController.instance.UnlockGreenBird();
            GameController.instance.UnlockRedBird();
        }
       
    }

}
	
	
