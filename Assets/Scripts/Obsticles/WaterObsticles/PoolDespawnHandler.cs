using UnityEngine;

public class PoolDespawnHandler : MonoBehaviour, IDespawnable
{
    private WaterObsticlesObjectPool pool;

    public void Init(WaterObsticlesObjectPool pool)
    {
        this.pool = pool;
    }

    public void Despawn()
    {
        pool.ReturnObject(gameObject);
    }
}