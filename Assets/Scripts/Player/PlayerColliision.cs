using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private float minimalWaterYThreshold = 1f;
    [SerializeField] private float overlapSizeOnLog = 0.9f;
    [SerializeField] private float overlapSizeOnGoal = 0.7f;

    private PlayerRespawn playerRespawn;
    private Health health;
    private BoxCollider2D boxCollider2D;

    private void OnEnable()
    {
        PlayerMovement.OnPlayerMovementFinished += CheckPositionAfterMove;
    }

    private void OnDisable()
    {
        PlayerMovement.OnPlayerMovementFinished -= CheckPositionAfterMove;
    }

    private void Start()
    {
        health = FindFirstObjectByType<Health>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void CheckPositionAfterMove()
    {
        if (isOnGoal()) return;

        if (isOnWaterObsticles()) return;

        if(transform.position.y > minimalWaterYThreshold)
        {
            HealthDecrese();
        }
    }

    private bool isOnWaterObsticles()
    {
        bool onWaterObsticle = PlatformDetectionUtility.IsStandingOnPlatform(GetComponent<Collider2D>(), LayerMask.GetMask(Layers.WATEROBSTICLES_LAYER), overlapSizeOnLog, out Transform logTransform);

        if (onWaterObsticle)
        {
            if (transform.parent != logTransform || transform.parent == null)
            {
                transform.SetParent(logTransform, true);
            }
            return true;
        }
        else
        {
            transform.SetParent(null);
            return false;
        }
    }

    private bool isOnGoal()
    {
        bool onGoal = PlatformDetectionUtility.IsStandingOnPlatform(GetComponent<Collider2D>(), LayerMask.GetMask(Layers.GOAL_LAYER), overlapSizeOnGoal, out Transform goalTransform);

        if (onGoal)
        {
            GoalTrigger goal = goalTransform.GetComponent<GoalTrigger>();

            if(goal != null)
            {
                goal.Activate();
                TriggerGoalSequence(goal.ghoastPlayerPrefab, goalTransform.localPosition, goalTransform.parent);
                return true;
            }
        }

        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.CAR_TAG))
        {
            HealthDecrese();
        }

        if(collision.gameObject.CompareTag(Tags.PLAYER_TAG))
        {
            HealthDecrese();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(Tags.PLAYER_TAG))
        {
            HealthDecrese();
        }
    }

    private void TeleportOrKill()
    {
        if (health.IsAlive)
        {
            playerRespawn?.RespawnPlayer();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void HealthDecrese()
    {
        health.DecreaseHealth();
        TeleportOrKill();
    }

    public void SetPlayerRespawn(PlayerRespawn respawn)
    {
        playerRespawn = respawn;
    }

    public void TriggerGoalSequence(GameObject ghostPlayerPrefab, Vector3 goalPosition, Transform parentGoal)
    {
        transform.SetParent(null);
        StartCoroutine(GoalSequenceRoutine(ghostPlayerPrefab, goalPosition, parentGoal));
    }

    private IEnumerator GoalSequenceRoutine(GameObject ghostPlayerPrefab, Vector3 goalPosition, Transform parentGoal)
    {
        PlayerMovement movement = GetComponent<PlayerMovement>();

        if (movement != null)
        {
            while (movement.isMoving)
                yield return null;
        }

        GameObject ghost = Instantiate(ghostPlayerPrefab, Vector3.zero, Quaternion.identity);
        ghost.transform.SetParent(parentGoal);
        ghost.transform.localPosition = goalPosition;

        playerRespawn?.RespawnPlayer();

        boxCollider2D.enabled = true;
    }
}
