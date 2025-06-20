using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float transitionTime = 0.5f;

    private bool isMoving = false;

    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (isMoving) return;

        if (playerInput.IsMovingUp)
        {
            StartCoroutine(MoveRoutine(Vector3.up));
        }
        else if (playerInput.IsMovingDown)
        {
            StartCoroutine(MoveRoutine(Vector3.down));
        }
        else if (playerInput.IsMovingLeft)
        {
            StartCoroutine(MoveRoutine(Vector3.left));
        }
        else if (playerInput.IsMovingRight)
        {
            StartCoroutine(MoveRoutine(Vector3.right));
        }
    }

    private IEnumerator MoveRoutine(Vector3 direction)
    {
        isMoving = true;
        float elapsedTime = 0f;

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + direction;

        while (elapsedTime < transitionTime)
        {
            Vector3 logDelta = Vector3.zero;

            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / transitionTime) + logDelta;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;

        isMoving = false;
    }
}
