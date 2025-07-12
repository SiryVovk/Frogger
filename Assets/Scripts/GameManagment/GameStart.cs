using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameStart : MonoBehaviour
{
    [SerializeField] private GameObject startGameUI;
    [SerializeField] private PlayerRespawn playerRespawn;

    private Player playerInput;

    private bool isGameStarted = false;

    public static event Action OnGameStarted;

    private void Awake()
    {
        playerInput = new Player();
    }

    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.Menu.StartGame.performed += OnStartGamePerformed;
    }

    private void OnDisable()
    {
        playerInput.Disable();
        playerInput.Menu.StartGame.performed -= OnStartGamePerformed;
    }

    private void OnStartGamePerformed(InputAction.CallbackContext context)
    {
        if(isGameStarted) return;

        playerRespawn.RespawnPlayer();
        isGameStarted = true;
        startGameUI.SetActive(false);
        OnGameStarted?.Invoke();
    }
}
