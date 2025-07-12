using UnityEngine;

public class HealthUi : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private GameObject[] hearts;

    private void Awake()
    {
        if(health == null)
        {
            health = FindFirstObjectByType<Health>();
        }
    }

    private void OnEnable()
    {
        if (health != null)
            health.OnHealthChanged += UpdateHealthDisplay;
    }

    private void OnDisable()
    {
        if (health != null)
            health.OnHealthChanged -= UpdateHealthDisplay;
    }

    private void UpdateHealthDisplay(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < currentHealth);
        }
    }
}
