using UnityEngine;

public interface IObjectPool
{
    public void InitilizePool();

    public GameObject GetObjectFromPool();

    public GameObject CreateObject();

    public void ReturnObject(GameObject gameObject);

}
