using UnityEngine;

public class CarsSpawner : MonoBehaviour, ISpawner
{
    [SerializeField] private Transform[] leftSpawnPoints;
    [SerializeField] private Transform[] rightSpawnPoints;

    [SerializeField] private float spawnInterval = 2f;

    private CarObjectPool carObjectPool;

    private void Start()
    {
        carObjectPool = GetComponent<CarObjectPool>();
        InvokeRepeating(nameof(Spawn), 0f, spawnInterval);
    }

    public void Spawn()
    {
        Transform spawnPoint;
        Vector3 moveDirection;
        if (Random.value < 0.5f)
        {
            spawnPoint = leftSpawnPoints[Random.Range(0, leftSpawnPoints.Length)];
            moveDirection = Vector3.left;
        }
        else
        {
            spawnPoint = rightSpawnPoints[Random.Range(0, rightSpawnPoints.Length)];
            moveDirection = Vector3.right;
        }

        CarMove car = carObjectPool.GetObjectFromPool().GetComponent<CarMove>();
        car.transform.position = spawnPoint.position;
        car.SetMoveDirection(moveDirection);
    }
}
