using UnityEngine;

public class CoffeePickup : MonoBehaviour
{
    [SerializeField] private int usesToGive = 1;

    private void OnTriggerEnter(Collider other)
    {
        // Try to find PickupManager on the parent (Player)
        PickupManager manager = other.GetComponentInParent<PickupManager>();
        
        if (manager != null)
        {
            manager.AddCoffee(usesToGive);
            Debug.Log($"[CoffeePickup] Collected coffee pickup. Granted uses: {usesToGive}.");
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("[CoffeePickup] Triggered by an object without a PickupManager in its parent hierarchy.");
        }
    }
}