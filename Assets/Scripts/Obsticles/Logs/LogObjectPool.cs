using System.Collections.Generic;
using UnityEngine;

public class LogObjectPool : MonoBehaviour, IObjectPool
{
    [SerializeField] private GameObject logPrefab;

    [SerializeField] private int poolSize = 10;

    private Queue<GameObject> logPool;

    private void Awake()
    {
        InitilizePool();
    }

    public GameObject CreateObject()
    {
        GameObject log = Instantiate(logPrefab, transform);
        log.GetComponent<LogCollision>().Init(this);
        log.SetActive(false);
        return log;
    }

    public GameObject GetObjectFromPool()
    {
        GameObject log;

        if (logPool == null || logPool.Count == 0)
        {
            log = CreateObject();
            log.SetActive(true);
            return log;
        }

        if(logPool.Count > 0)
        {
            log = logPool.Dequeue();
            log.SetActive(true);
            return log;
        }

        return null;
    }

    public void InitilizePool()
    {
        logPool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject log = CreateObject();
            logPool.Enqueue(log);
        }
    }

    public void ReturnObject(GameObject gameObject)
    {
        if(logPool.Count >= poolSize)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
            logPool.Enqueue(gameObject);
        }
    }
}
