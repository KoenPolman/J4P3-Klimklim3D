using UnityEngine;

public class PickupManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ClimbingManager climbingManager;

    [Header("Chalk Settings")]
    [SerializeField] private KeyCode chalkKey = KeyCode.E;
    public int chalkInventory = 0;
    private int chalkHoldsPerCharge = 8;

    // // Future-proofing for Coffee
    // [Header("Coffee Settings")]
    // [SerializeField] private KeyCode coffeeKey = KeyCode.Q;
    // public int coffeeInventory = 0;

    void Awake()
    {
        if (climbingManager == null) climbingManager = GetComponent<ClimbingManager>();
    }

    void Update()
    {
        // Activate Chalk
        if (Input.GetKeyDown(chalkKey) && chalkInventory > 0)
        {
            // Only activate if an effect isn't already running
            if (climbingManager.holdsRemaining <= 0)
            {
                chalkInventory--;
                climbingManager.ActivateChalkEffect(chalkHoldsPerCharge);
                Debug.Log("Chalk used! Inventory: " + chalkInventory);
            }
        }

        // // Space for Coffee Activation logic later
        // if (Input.GetKeyDown(coffeeKey) && coffeeInventory > 0)
        // {
        //     // UseCoffee();
        // }
    }

    public void AddChalk(int amount) => chalkInventory += amount;
    //public void AddCoffee(int amount) => coffeeInventory += amount;
}