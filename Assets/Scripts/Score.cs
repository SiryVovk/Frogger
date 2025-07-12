using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private int scoreIncrementUpMovement = 10;
    [SerializeField] private int scoreIncrementOnGoal = 100;
    [SerializeField] private int scoreIncrementOnTimeBonus = 5;
    [SerializeField] private int scoreIncrementOnWin = 1000;

    [SerializeField] private float timeBonusThreshold = 30f;

    private int score = 0;
    private float highestY = float.MinValue;
    private float currentTime;

    private const string HIGH_SCORE_KEY = "HighScore";

    private void OnEnable()
    {
        PlayerMovement.OnPlayerUpMove += HandleUpMovement;
        PlayerRespawn.OnRespawn += ResetHighestY;
        GoalTrigger.OnGoalReached += HandleGoalReached;
        GoalsCounter.OnAllGoalsReached += (x) => IncreaseScore(scoreIncrementOnWin);
        Health.OnPlayerDied += SaveHighScore;
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
        int highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, score);
            PlayerPrefs.Save();
        }
    }
}
