using UnityEngine;
using UnityEngine.SceneManagement;

public class IfPlayerLoseEndGame : MonoBehaviour
{
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnEndGamePreform(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        AppUtils.QuitApplication();
    }
}
