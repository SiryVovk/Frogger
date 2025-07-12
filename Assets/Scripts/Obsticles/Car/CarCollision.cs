using UnityEngine;

public class CarCollision : MonoBehaviour
{
    private CarObjectPool carObjectPool;

    public void Init(CarObjectPool carObjectPool)
    {
        this.carObjectPool = carObjectPool;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Tags.DISPAWN_TAG))
        {
            carObjectPool.ReturnObject(this.gameObject);
        }

        if(collision.CompareTag(Tags.CAR_TAG))
        {
            float newSpeed = collision.GetComponent<CarMove>().GetSpeed();
            this.gameObject.GetComponent<CarMove>().SetSpeed(newSpeed);
        }
    }
}
