using UnityEngine;

public class ChalkPickup : MonoBehaviour
{
    private int usesToGive = 1;

    private void OnTriggerEnter(Collider other)
    {
        PickupManager manager = other.GetComponentInParent<PickupManager>();
        
        if (manager != null)
        {
            manager.AddChalk(usesToGive);
            Destroy(gameObject);
        }
    }
}