 using UnityEngine;

public class IfPlayerWinEndGame : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Health playerHealth;
    [SerializeField] private PlayerRespawn playerRespawn;
    [SerializeField] private CarsSpawner carsSpawner;
    [SerializeField] private WaterObsticlesSpawner waterObsticlesSpawner;

    [Header("UI")]
    [SerializeField] private GameObject EndGameUI;

    [Header("Settings")]
    [SerializeField] private float onWinWaterSpawnObcticleIncreaseTime = 0.05f;
    [SerializeField] private float onWinCarSpawnVeacleDecreseTime = 0.05f;

    private Player playerInput;

    private void Awake()
    {
        playerInput = InputManager.InputActions;
    }

    private void OnEnable()
    {
        playerInput.Menu.StartGame.performed += OnRestartGamePreform;
        playerInput.Menu.EndGame.performed += OnEndGamePreform;
    }

    private void OnDisable()
    {
        playerInput.Menu.StartGame.performed -= OnRestartGamePreform;
        playerInput.Menu.EndGame.performed -= OnEndGamePreform;
    }

    private void OnRestartGamePreform(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Time.timeScale = 1f;
        playerRespawn.RespawnPlayer();
        playerHealth.ResetHealth();
        EndGameUI.SetActive(false);
        carsSpawner.DecreseInterval(onWinCarSpawnVeacleDecreseTime);
        waterObsticlesSpawner.IncreaseInterval(onWinWaterSpawnObcticleIncreaseTime);
        enabled = false;

    }

    private void OnEndGamePreform(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        AppUtils.QuitApplication();
    }
}
