using UnityEngine;

public class CarMove : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float minSpeed = 1f;

    [SerializeField] private Vector3 moveDirection = Vector3.right;

    private float speed;

    private void Awake()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    private void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    public void SetMoveDirection(Vector3 newDirection)
    {
        moveDirection = newDirection;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
