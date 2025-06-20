using UnityEngine;

public class LogSpawner : MonoBehaviour, ISpawner
{
    [SerializeField] private Transform[] leftSpawnPoints;
    [SerializeField] private Transform[] rightSpawnPoints;

    [SerializeField] private float spawnInterval = 2f;

    private LogObjectPool logObjectPool;

    private bool lastSpawnWasLeft;
    private int lastLeftSpawnIndex = -1;
    private int lastRightSpawnIndex = -1;

    private void Awake()
    {
        logObjectPool = GetComponent<LogObjectPool>();
        lastSpawnWasLeft = Random.value < 0.5f;
        InvokeRepeating(nameof(Spawn), 0f, spawnInterval);
    }

    public void Spawn()
    {
        bool spawnLeft = !lastSpawnWasLeft;
        Transform[] spawnPoints = spawnLeft ? leftSpawnPoints : rightSpawnPoints;
        Vector3 moveDirection = spawnLeft ? Vector3.left : Vector3.right;

        int lastSpawnIndex = spawnLeft ? lastLeftSpawnIndex : lastRightSpawnIndex;

        int spawnIndex;
        if (spawnPoints.Length <= 1)
        {
            spawnIndex = 0;
        }
        else
        {
            do
            {
                spawnIndex = Random.Range(0, spawnPoints.Length);
            } while (spawnIndex == lastSpawnIndex);
        }

        if (spawnLeft)
            lastLeftSpawnIndex = spawnIndex;
        else
            lastRightSpawnIndex = spawnIndex;

        Transform spawnPoint = spawnPoints[spawnIndex];
        lastSpawnWasLeft = spawnLeft;

        GameObject logObj = logObjectPool.GetObjectFromPool();
        if (logObj == null)
        {
            Debug.LogWarning("No log object available in pool.");
            return;
        }

        LogMove log = logObj.GetComponent<LogMove>();
        if (log == null)
        {
            Debug.LogWarning("LogMove component missing on pooled object.");
            return;
        }

        log.transform.position = spawnPoint.position;
        log.SetMoveDirection(moveDirection);
    }
}
