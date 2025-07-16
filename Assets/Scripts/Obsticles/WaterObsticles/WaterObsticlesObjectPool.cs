using System.Collections.Generic;
using UnityEngine;

public class WaterObsticlesObjectPool : MonoBehaviour, IObjectPool
{
    [SerializeField] private GameObject[] waterObsticles;

    [SerializeField] private int poolSize = 10;

    private List<GameObject> waterObsticlesPool;

    private void Awake()
    {
        InitilizePool();
    }

    public GameObject CreateObject()
    {
        int randomIndex = Random.Range(0, waterObsticles.Length);
        GameObject waterObsticle = Instantiate(waterObsticles[randomIndex], transform);

        var despawnHandler = waterObsticle.GetComponentInChildren<PoolDespawnHandler>(true);
        if (despawnHandler == null)
        {
            Debug.LogError("No PoolDespawnHandler in spawned object");
            return waterObsticle;
        }

        despawnHandler.Init(this);

        waterObsticle.SetActive(false);
        return waterObsticle;
    }

    public GameObject GetObjectFromPool()
    {
        GameObject waterObsticle;

        if (waterObsticlesPool == null || waterObsticlesPool.Count == 0)
        {
            waterObsticle = CreateObject();
            waterObsticle.SetActive(true);
            return waterObsticle;
        }

        if(waterObsticlesPool.Count > 0)
        {
            int randomIndex = Random.Range(0, waterObsticlesPool.Count);
            waterObsticle = waterObsticlesPool[randomIndex];
            waterObsticlesPool.RemoveAt(randomIndex);
            waterObsticle.SetActive(true);
            return waterObsticle;
        }

        return null;
    }

    public void InitilizePool()
    {
        waterObsticlesPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject waterObsticl = CreateObject();
            waterObsticlesPool.Add(waterObsticl);
        }
    }

    public void ReturnObject(GameObject gameObject)
    {
        if(waterObsticlesPool.Count >= poolSize)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
            waterObsticlesPool.Add(gameObject);
        }
    }
}
