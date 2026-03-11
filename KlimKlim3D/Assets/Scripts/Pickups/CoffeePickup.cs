using UnityEngine;

public class CoffeePickup : MonoBehaviour
{
    private int usesToGive = 1;

    private void OnTriggerEnter(Collider other)
    {
        PickupManager manager = other.GetComponentInParent<PickupManager>();
        
        if (manager != null)
        {
            manager.AddCoffee(usesToGive);
            Destroy(gameObject);
        }
    }
}