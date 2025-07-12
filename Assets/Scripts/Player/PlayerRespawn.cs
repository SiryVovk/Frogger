using System;
using System.Collections;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private GameObject playerPrefab;

    private GameObject currentPlayer;

    public static event Action OnRespawn;

    public void RespawnPlayer()
    {
        if (currentPlayer == null)
        {
            currentPlayer = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
            currentPlayer.GetComponent<PlayerCollision>().SetPlayerRespawn(this);
        }
        else
        {
            var movement = currentPlayer.GetComponent<PlayerMovement>();
            if (movement != null)
            {
                movement.StopAllCoroutines();
                movement.inputEnabled = false;
                movement.isMoving = false;
            }

            currentPlayer.SetActive(false);
            currentPlayer.transform.position = respawnPoint.position;
            currentPlayer.SetActive(true);

            if (movement != null)
                StartCoroutine(EnableInputAfterDelay(movement));

            OnRespawn?.Invoke();
        }
    }

    private IEnumerator EnableInputAfterDelay(PlayerMovement movement)
    {
        yield return new WaitForSeconds(0.1f);
        movement.inputEnabled = true;
    }
}
