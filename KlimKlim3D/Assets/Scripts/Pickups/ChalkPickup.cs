// using UnityEngine;

// public class ChalkPickup : MonoBehaviour
// {
//     public int usesToGive = 3;

//     private void OnTriggerEnter(Collider healthcare)
//     {
//         ClimbingManager manager = healthcare.GetComponentInParent<ClimbingManager>();
        
//         if (manager != null)
//         {
//             manager.AddChalkToInventory(usesToGive);
//             Destroy(gameObject); // Verdwijn na oppakken
//             Debug.Log($"Chalk opgepakt! Je hebt nu {usesToGive} extra beurten.");
//         }
//     }
// }