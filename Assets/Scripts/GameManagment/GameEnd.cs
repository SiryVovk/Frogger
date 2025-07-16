using TMPro;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    [Header("End Game UI")]
    [SerializeField] private GameObject endGameUI;
    [SerializeField] private TMP_Text endGameText;
    [SerializeField] private TMP_Text scorText;
    [SerializeField] private TMP_Text yourScore;
    [Header("End Game Conditions")]
    [SerializeField] private IfPlayerLoseEndGame ifPlayerLoseEndGame;
    [SerializeField] private IfPlayerWinEndGame ifPlayerWinEndGame;
    [Header("Game Components")]
    [SerializeField] private Score score;
    [SerializeField] private Health health;

    [SerializeField] private string WIN_TEXT = "You Win!";
    [SerializeField] private string LOSE_TEXT = "You Lose!";

    private void OnEnable()
    {
        health.OnPlayerDied += OnEndGame;
        GoalsCounter.OnAllGoalsReached += OnEndGame;
    }

    private void OnDisable()
    {
        health.OnPlayerDied -= OnEndGame;
        GoalsCounter.OnAllGoalsReached -= OnEndGame;
    }

    private void OnEndGame(GameEndCondition gameEndCondition)
    {
        Time.timeScale = 0f;
        endGameUI.SetActive(true);

        switch(gameEndCondition)
        {
            case GameEndCondition.PlayerDied:
                endGameText.text = LOSE_TEXT;
                ifPlayerLoseEndGame.enabled = true;
                break;
            case GameEndCondition.AllGoalsReached:
                endGameText.text = WIN_TEXT;
                ifPlayerWinEndGame.enabled = true;
                break;
        }

        scorText.text = $"Highest score: {PlayerPrefs.GetInt(PlayerPrefrencesStrings.HIGH_SCORE_KEY)}";
        yourScore.text = "Your score: " + score.GetScore().ToString();
    }
}
