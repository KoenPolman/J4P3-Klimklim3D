using UnityEngine;

public class ChalkPickup : MonoBehaviour
{
    [SerializeField] private int usesToGive = 3;

    private void OnTriggerEnter(Collider other)
    {
        // Zoek de ClimbingManager via de collider die de trigger raakt
        ClimbingManager manager = other.GetComponentInParent<ClimbingManager>();
        
        if (manager != null)
        {
            manager.AddChalkToInventory(usesToGive);
            Destroy(gameObject); // De pickup is eenmalig
            Debug.Log($"Chalk opgepakt! +{usesToGive} uses.");
        }
    }
}