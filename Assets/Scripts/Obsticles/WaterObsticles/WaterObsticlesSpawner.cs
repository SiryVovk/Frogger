using System.Collections;
using UnityEngine;

public class WaterObsticlesSpawner : MonoBehaviour, ISpawner
{
    [SerializeField] private Transform[] leftSpawnPoints;
    [SerializeField] private Transform[] rightSpawnPoints;

    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float maxSpawnInterval = 3f;

    private WaterObsticlesObjectPool waterObsticlesObjectPool;

    private bool lastSpawnWasLeft;
    private int lastLeftSpawnIndex = -1;
    private int lastRightSpawnIndex = -1;

    private void Awake()
    {
        waterObsticlesObjectPool = GetComponent<WaterObsticlesObjectPool>();
        lastSpawnWasLeft = Random.value < 0.5f;
        StartCoroutine(InitializeSpawner());
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

        GameObject waterObsticle = waterObsticlesObjectPool.GetObjectFromPool();
        if (waterObsticle == null)
        {
            Debug.LogWarning("No log object available in pool.");
            return;
        }

        WaterObsticlesMove waterObsticleMove = waterObsticle.GetComponent<WaterObsticlesMove>();
        if (waterObsticleMove == null)
        {
            Debug.LogWarning("WaterObsticlesMove component missing on pooled object.");
            return;
        }

        waterObsticleMove.transform.position = spawnPoint.position;
        ChoseMoveDirection(waterObsticleMove, spawnLeft, moveDirection);

        ChoseRotation(waterObsticle, spawnLeft);
    }

    private void ChoseRotation(GameObject waterObsticle,bool isLeft)
    {
        if (isLeft)
        {
            waterObsticle.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            waterObsticle.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
    }

    private void ChoseMoveDirection(WaterObsticlesMove waterObsticlesMove, bool isLeft, Vector3 moveDirection)
    {
        if (isLeft)
        {
            waterObsticlesMove.SetMoveDirection(moveDirection);
        }
        else
        {
            waterObsticlesMove.SetMoveDirection(moveDirection * -1);
        }
    }

    private IEnumerator InitializeSpawner()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnInterval);
            Spawn();
        }
    }

    public void IncreaseInterval(float IncreseAmount)
    {
        if (spawnInterval > maxSpawnInterval)
        {
            spawnInterval -= IncreseAmount;
        }
    }
}
