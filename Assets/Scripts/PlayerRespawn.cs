using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private GameObject playerPrefab;

    private GameObject currentPlayer;

    private void Awake()
    {
        RespawnPlayer();
    }

    public void RespawnPlayer()
    {
        if (currentPlayer == null)
        {
            currentPlayer = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
            currentPlayer.GetComponent<PlayerCollision>().SetPlayerRespawn(this);
        }
        else
        {
            currentPlayer.transform.position = respawnPoint.position;
            currentPlayer.SetActive(true);
        }
    }
}
