using UnityEngine;

public class ChalkPickup : MonoBehaviour
{
    [SerializeField] private int usesToGive = 3;

    private void OnTriggerEnter(Collider other)
    {
        // Try to find PickupManager on the parent (Player)
        PickupManager manager = other.GetComponentInParent<PickupManager>();
        
        if (manager != null)
        {
            manager.AddChalk(usesToGive);
            Destroy(gameObject);
            Debug.Log("Chalk picked up!");
        }
    }
}