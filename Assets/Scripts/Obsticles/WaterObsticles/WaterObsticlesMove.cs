using UnityEngine;

public class WaterObsticlesMove : MonoBehaviour
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
