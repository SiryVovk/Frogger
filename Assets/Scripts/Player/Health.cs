using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;

    private int currentHealth;

    public bool IsAlive = true;

    public event Action<int> OnHealthChanged;
    public static event Action<GameEndCondition> OnPlayerDied;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void CheckForDeath()
    {
        if(currentHealth <= 0)
        {
            IsAlive = false;
            OnPlayerDied?.Invoke(GameEndCondition.PlayerDied);
        }
    }

    public void DecreaseHealth()
    {
        currentHealth--;
        CheckForDeath();
        OnHealthChanged?.Invoke(currentHealth);
    }
}
