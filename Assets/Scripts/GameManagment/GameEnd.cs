using System;
using TMPro;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    [SerializeField] private GameObject endGameUI;
    [SerializeField] private TMP_Text endGameText;
    [SerializeField] private TMP_Text scorText;

    [SerializeField] private string WIN_TEXT = "You Win!";
    [SerializeField] private string LOSE_TEXT = "You Lose!";


    private const string HIGHEST_SCORE_KEY = "HighestScore";

    private void OnEnable()
    {
        Health.OnPlayerDied += OnEndGame;
        GoalsCounter.OnAllGoalsReached += OnEndGame;
    }

    private void OnEndGame(GameEndCondition gameEndCondition)
    {
        Time.timeScale = 0f;
        endGameUI.SetActive(true);

        switch(gameEndCondition)
        {
            case GameEndCondition.PlayerDied:
                endGameText.text = LOSE_TEXT;
                break;
            case GameEndCondition.AllGoalsReached:
                endGameText.text = WIN_TEXT;
                break;
        }

        scorText.text = $"Highest score: {PlayerPrefs.GetInt(HIGHEST_SCORE_KEY)}";
    }
}
