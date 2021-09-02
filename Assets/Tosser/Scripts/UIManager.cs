using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Tosser.Core;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private TextMeshProUGUI playerText;
    private TextMeshProUGUI enemyText;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateScore(bool playerScore)
    {
        if (playerScore)
        {
            int playerPoints = PointCalculator.instance.GetPlayerPoints;
            playerText.text = "";
        }
        else
        {
            int enemyoints = PointCalculator.instance.GetEnemyPoints;
            enemyText.text = "";
        }
    }

    public void StartButtonPressed()
    {
        Debug.Log("start button");
    }

    public void QuitButtonPressed()
    {
        Debug.Log("quit button");
    }
}
