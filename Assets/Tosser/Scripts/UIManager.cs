using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Tosser.Core;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private TextMeshProUGUI playerText;
    [SerializeField] private TextMeshProUGUI enemyText;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject quitButton;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
    }

    public void MainMenu(bool enter)
    {
        if (enter)
        {
            startButton.SetActive(true);
            quitButton.SetActive(true);
            restartButton.SetActive(false);
        }
        else
        {
            startButton.SetActive(false);
            quitButton.SetActive(false);
        }
    }

    public void PauseMenu(bool enter)
    {
        if (enter)
        {
            restartButton.SetActive(true);
            quitButton.SetActive(true);
        }
        else
        {
            restartButton.SetActive(false);
            quitButton.SetActive(false);
        }
    }

    public void UpdateScore(bool playerScore)
    {
        int playerPoints = PointCalculator.instance.GetPlayerPoints;
        playerText.text = "Player Score: " + playerPoints;

        int enemyPoints = PointCalculator.instance.GetEnemyPoints;
        enemyText.text = "Enemy Score: " + enemyPoints;

        if (playerScore)
        {
            // Play particle effect based on who scored
        }
    }

    void StartGame()
    {
        MainMenu(false);
    }

    public void RestartButtonPressed()
    {
        StartGame();
    }

    public void StartButtonPressed()
    {
        StartGame();
    }

    public void QuitButtonPressed()
    {
        Application.Quit();
    }
}
