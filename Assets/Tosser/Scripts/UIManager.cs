using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using Tosser.Core;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private TextMeshProUGUI playerText;
    [SerializeField] private TextMeshProUGUI enemyText;
    [SerializeField] private Button startButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button menuButton;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        startButton.onClick.AddListener(StartButtonPressed);
        quitButton.onClick.AddListener(QuitButtonPressed);
        menuButton.onClick.AddListener(MainMenuButtonPressed);
        MainMenu(false);
    }

    public void MainMenu(bool enter)
    {
        if (enter)
        {
            startButton.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(false);
            menuButton.gameObject.SetActive(true);
        }
        else
        {
            startButton.gameObject.SetActive(false);
            quitButton.gameObject.SetActive(false);
            menuButton.gameObject.SetActive(false);
        }
    }

    public void PauseMenu(bool enter)
    {
        restartButton.gameObject.SetActive(enter);
        quitButton.gameObject.SetActive(enter);
        menuButton.gameObject.SetActive(enter);
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

    public void MainMenuButtonPressed()
    {
        SceneManager.LoadScene(0);
    }

    public void StartButtonPressed()
    {
        StartGame();
    }

    public void QuitButtonPressed()
    {
        Application.Quit();
    }

    private void OnDisable()
    {
        startButton.onClick.RemoveAllListeners();
    }
}
