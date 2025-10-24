using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float transitionTime = 0.5f;
    [SerializeField] private float movementDeley = 0.05f;

    public bool isMoving = false;
    public bool inputEnabled = true;

    private PlayerInput playerInput;

    public event Action<Vector3> OnPlayerUpMove;
    public event Action OnPlayerMove; 
    public event Action OnPlayerMovementFinished;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (isMoving || !inputEnabled) return;

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
        OnPlayerMove?.Invoke();
        isMoving = true;
        inputEnabled = false;
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
        OnPlayerUpMove?.Invoke(targetPosition);

        isMoving = false;
        OnPlayerMovementFinished?.Invoke();

        yield return new WaitForSeconds(movementDeley);

        inputEnabled = true;
    }
}
