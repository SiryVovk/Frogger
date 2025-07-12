using System;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] public GameObject ghoastPlayerPrefab;

    public static event Action<GoalTrigger> OnGoalReached;
    public void Activate()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        OnGoalReached?.Invoke(this);
    }
}
