using UnityEngine;

public class LogCollision : MonoBehaviour
{
    private const string DISPAWN_TAG = "Dispawn";

    private LogObjectPool logObjectPool;

    public void Init(LogObjectPool logObjectPool)
    {
        this.logObjectPool = logObjectPool;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(DISPAWN_TAG))
        {
            logObjectPool.ReturnObject(this.gameObject);
        }
    }
}
