using UnityEngine;

public class Score : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Health health;
    [SerializeField] private PlayerMovement playerMovement;

    [Header("Score Increments")]
    [SerializeField] private int scoreIncrementUpMovement = 10;
    [SerializeField] private int scoreIncrementOnGoal = 100;
    [SerializeField] private int scoreIncrementOnTimeBonus = 5;
    [SerializeField] private int scoreIncrementOnWin = 1000;

    [SerializeField] private float timeBonusThreshold = 30f;

    private int score = 0;
    private float highestY = float.MinValue;
    private float currentTime;

    private void OnEnable()
    {
        playerMovement.OnPlayerUpMove += HandleUpMovement;
        PlayerRespawn.OnRespawn += ResetHighestY;
        GoalTrigger.OnGoalReached += HandleGoalReached;
        GoalsCounter.OnAllGoalsReached += (x) => IncreaseScore(scoreIncrementOnWin);
        health.OnPlayerDied += SaveHighScore;
    }

    private void OnDisable()
    {
        playerMovement.OnPlayerUpMove -= HandleUpMovement;
        PlayerRespawn.OnRespawn -= ResetHighestY;
        GoalTrigger.OnGoalReached -= HandleGoalReached;
        GoalsCounter.OnAllGoalsReached -= (x) => IncreaseScore(scoreIncrementOnWin);
        health.OnPlayerDied -= SaveHighScore;
    }

    private void Start()
    {
        ResetTimeBonus();
    }

    private void Update()
    {
        if (currentTime > 0f)
        {
            currentTime -= Time.deltaTime;
        }
    }

    private void ResetTimeBonus()
    {
        currentTime = timeBonusThreshold;
    }

    public void HandleUpMovement(Vector3 direction)
    {
        if (direction.y > highestY)
        {
            highestY = direction.y;
            IncreaseScore(scoreIncrementUpMovement);
        }
    }

    public void ResetHighestY()
    {
        highestY = float.MinValue;
    }
    private void IncreaseScore(int scoreIncrement)
    {
        score += scoreIncrement;
    }

    public int GetScore()
    {
        return score;
    }

    private void HandleGoalReached(GoalTrigger goalTrigger)
    {
        IncreaseScore(scoreIncrementOnGoal);

        int timeBonus = Mathf.FloorToInt(currentTime) * scoreIncrementOnTimeBonus;
        IncreaseScore(timeBonus);
    }

    private void SaveHighScore(GameEndCondition gameEndCondition)
    { 
        int highScore = PlayerPrefs.GetInt(PlayerPrefrencesStrings.HIGH_SCORE_KEY, 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt(PlayerPrefrencesStrings.HIGH_SCORE_KEY, score);
            PlayerPrefs.Save();
        }
    }
}
