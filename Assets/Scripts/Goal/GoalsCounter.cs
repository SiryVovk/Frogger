using System;
using UnityEngine;

public class GoalsCounter : MonoBehaviour
{
    private int goalsCounter = 6;
    
    public static event Action<GameEndCondition> OnAllGoalsReached;

    private void OnEnable()
    {
        GoalTrigger.OnGoalReached += HandleGoalReached;
    }

    private void OnDisable()
    {
        GoalTrigger.OnGoalReached -= HandleGoalReached;
    }

    private void HandleGoalReached(GoalTrigger trigger)
    {
        goalsCounter--;
        CheckIfAllGoalsReached();
    }

    private void CheckIfAllGoalsReached()
    {
        if(goalsCounter <= 0)
        {
            OnAllGoalsReached?.Invoke(GameEndCondition.AllGoalsReached);
        }
    }
}
