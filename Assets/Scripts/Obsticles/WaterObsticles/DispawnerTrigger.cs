using UnityEngine;

public class DispawnerTrigger : MonoBehaviour
{
    [SerializeField] private MonoBehaviour despawnTarget;
    private IDespawnable despawnable;

    private void Awake()
    {
        despawnable = despawnTarget as IDespawnable;
        if (despawnable == null)
            Debug.LogError($"{gameObject.name} has DispawnerTrigger but no valid IDespawnable assigned!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.DISPAWN_TAG))
        {
            despawnable?.Despawn();
        }
    }
}
