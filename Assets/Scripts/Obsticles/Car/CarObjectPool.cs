using System.Collections.Generic;
using UnityEngine;

public class CarObjectPool : MonoBehaviour, IObjectPool
{
    [SerializeField] private GameObject carPrefab;

    [SerializeField] private int poolSize = 10;

    private Queue<GameObject> carPool;

    private void Awake()
    {
        InitializeCarsOnScene();
        InitilizePool();
    }

    private void InitializeCarsOnScene()
    {
        CarMove[] carsOnScene = FindObjectsByType<CarMove>(FindObjectsSortMode.None);

        foreach (CarMove car in carsOnScene)
        {
            car.GetComponent<CarCollision>().Init(this);
        }
    }

    public void InitilizePool()
    {
        carPool = new Queue<GameObject>();

        for (int i = carPool.Count-1; i < poolSize; i++)
        {
            carPool.Enqueue(CreateObject());
        }
    }

    public GameObject GetObjectFromPool()
    {
        GameObject car;

        if (carPool == null || carPool.Count == 0)
        {
            car = CreateObject();
            car.SetActive(true);
            return car;
        }

        if(carPool.Count > 0)
        {
            car = carPool.Dequeue();
            car.SetActive(true);
            return car;
        }

        return null;
    }

    public GameObject CreateObject()
    {
        GameObject car = Instantiate(carPrefab, transform);
        car.GetComponent<CarCollision>().Init(this);
        car.SetActive(false);
        return car;
    }

    public void ReturnObject(GameObject car)
    {
        if (carPool.Count >= poolSize)
        {
            Destroy(car);
        }
        else
        {
            car.SetActive(false);
            carPool.Enqueue(car);
        }
    }
}
