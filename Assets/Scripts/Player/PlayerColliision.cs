using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private const string CAR_TAG = "Car";
    private const string WATER_TAG = "Water";
    private const string LOG_TAG = "Log";

    private HashSet<Collider2D> logsTouching = new HashSet<Collider2D>();
    private PlayerRespawn playerRespawn;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(CAR_TAG))
        {
            playerRespawn?.RespawnPlayer();
        }

        if (collision.gameObject.CompareTag(LOG_TAG))
        {
            transform.SetParent(collision.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(LOG_TAG))
        {
            logsTouching.Add(other);
            UpdateLogParent();
        }

        if (other.CompareTag(WATER_TAG))
        {
            StartCoroutine(CheckIfDrown());
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(WATER_TAG))
        {
            StartCoroutine(CheckIfDrown());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(LOG_TAG))
        {
            logsTouching.Remove(other);
            UpdateLogParent();
        }
    }

    private void UpdateLogParent()
    {
        if (logsTouching.Count > 0)
        {
            var log = logsTouching.First();
            transform.SetParent(log.transform);
        }
        else
        {
            transform.SetParent(null);
        }
    }

    private IEnumerator CheckIfDrown()
    {
        yield return new WaitForSeconds(0.05f);
        if (logsTouching.Count == 0)
        {
            playerRespawn?.RespawnPlayer();
        }
    }

    public void SetPlayerRespawn(PlayerRespawn respawn)
    {
        playerRespawn = respawn;
    }
}
