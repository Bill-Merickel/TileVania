using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI startInstructionText;

    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start() {
        livesText.text = "";
        scoreText.text = "";
        titleText.text = "TileVania";
        startInstructionText.text = "Click Enter to Start";
    }

    void Update() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (currentSceneIndex == 0)
            {
                LoadFirstLevel();
            }
            else if (currentSceneIndex == 1)
            {
                ResetGameSession();
            }
        }
    }

    public void ResetPlayerStats()
    {
        playerLives = 3;
        score = 0;
    }

    public void LoadFirstLevel()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        ResetPlayerStats();
        SceneManager.LoadScene(2);
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
        titleText.text = "";
        startInstructionText.text = "";
    }

    public void DisplayVictoryText()
    {
        ResetPlayerStats();
        livesText.text = "";
        scoreText.text = "";
        titleText.text = "You Won!";
        startInstructionText.text = "Click Enter to Play Again";
    }

    public void DisplayGameOverText()
    {
        ResetPlayerStats();
        livesText.text = "";
        scoreText.text = "";
        titleText.text = "Game Over";
        startInstructionText.text = "Click Enter to Try Again";
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            showGameOverScreen();
        }
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

    void TakeLife()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
    }

    public void showGameOverScreen()
    {
        SceneManager.LoadScene(1);
        DisplayGameOverText();
    }

    void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
