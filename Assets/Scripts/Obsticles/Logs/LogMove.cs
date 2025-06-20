using UnityEngine;

public class LogMove : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection = Vector3.right;

    [SerializeField] private float speed;

    private void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    public void SetMoveDirection(Vector3 newDirection)
    {
        moveDirection = newDirection;
    }
}
