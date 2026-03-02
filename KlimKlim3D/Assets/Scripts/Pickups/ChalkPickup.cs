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
            Debug.Log($"[ChalkPickup] Collected chalk pickup. Granted uses: {usesToGive}.");
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("[ChalkPickup] Triggered by an object without a PickupManager in its parent hierarchy.");
        }
    }
}