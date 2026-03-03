using UnityEngine;

public class PickupManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ClimbingManager climbingManager;

    [Header("Chalk Settings")]
    [SerializeField] private KeyCode chalkKey = KeyCode.E;
    public int chalkInventory = 0;
    [SerializeField] private int chalkHoldsPerCharge = 8;

    [Header("Coffee Settings")]
    [SerializeField] private KeyCode coffeeKey = KeyCode.Q;
    public int coffeeInventory = 0;

    void Awake()
    {
        if (climbingManager == null) climbingManager = GetComponent<ClimbingManager>();
    }

    void Update()
    {
        // Activate Chalk
        if (Input.GetKeyDown(chalkKey))
        {
            if (chalkInventory <= 0)
            {
                Debug.Log("[PickupManager] Chalk key pressed, but no chalk in inventory.");
                return;
            }

            // Only activate if an effect isn't already running
            if (climbingManager.holdsRemaining <= 0)
            {
                chalkInventory--;
                climbingManager.ActivateChalkEffect(chalkHoldsPerCharge);
                Debug.Log($"[PickupManager] Chalk used. Inventory left: {chalkInventory}. Holds boosted: {chalkHoldsPerCharge}.");
            }
            else
            {
                Debug.Log($"[PickupManager] Chalk already active with {climbingManager.holdsRemaining} boosted holds remaining.");
            }
        }

        if (Input.GetKeyDown(coffeeKey))
        {
            if (coffeeInventory <= 0)
            {
                Debug.Log("[PickupManager] Coffee key pressed, but no coffee in inventory.");
                return;
            }

            if (!climbingManager.IsCoffeeEffectActive)
            {
                coffeeInventory--;
                climbingManager.ActivateCoffeeEffect();
                Debug.Log($"[PickupManager] Coffee used. Inventory left: {coffeeInventory}.");
            }
            else
            {
                Debug.Log("[PickupManager] Coffee effect is already active.");
            }
        }
    }

    public void AddChalk(int amount)
    {
        chalkInventory += amount;
        Debug.Log($"[PickupManager] Added {amount} chalk. Inventory now: {chalkInventory}.");
    }

    public void AddCoffee(int amount)
    {
        coffeeInventory += amount;
        Debug.Log($"[PickupManager] Added {amount} coffee. Inventory now: {coffeeInventory}.");
    }
}