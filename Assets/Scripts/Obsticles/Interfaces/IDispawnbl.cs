public interface IDespawnable
{
    /// <summary>
    /// Returns the object to the pool when it collides with a dispawn trigger.
    /// </summary>
    void Despawn();
}
