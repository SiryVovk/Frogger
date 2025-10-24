using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioClip moveClip;
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private AudioClip wniClip;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerCollision playerCollision;

    private void OnEnable()
    {
        playerMovement.OnPlayerMove += Move;
        playerCollision.playerDied += Death;
        playerCollision.playerOnGoal += Win;
    }

    private void OnDisable()
    {
        playerMovement.OnPlayerMove -= Move;
        playerCollision.playerDied -= Death;
        playerCollision.playerOnGoal -= Win;
    }

    private void Move()
    {
        audioSource.PlayOneShot(moveClip);
    }

    private void Death()
    {
        audioSource.PlayOneShot(deathClip);
    }

    private void Win()
    {
        audioSource.PlayOneShot(wniClip);
    }
}
