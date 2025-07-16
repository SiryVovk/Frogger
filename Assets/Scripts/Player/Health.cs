using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;

    private int currentHealth;

    public bool IsAlive { get; private set; } = true;

    public event Action<int> OnHealthChanged;
    public event Action<GameEndCondition> OnPlayerDied;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void CheckForDeath()
    {
        if (!IsAlive) return;

        if (currentHealth <= 0)
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

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        IsAlive = true;
        OnHealthChanged?.Invoke(currentHealth);
    }
}
