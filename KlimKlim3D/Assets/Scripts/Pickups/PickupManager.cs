using UnityEngine;

[RequireComponent(typeof(ClimbingPickupEffects))]
public class PickupManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ClimbingPickupEffects pickupEffects;

    [Header("Body Pickup Visuals")]
    [SerializeField] private GameObject chalkBodyPickupVisual;
    [SerializeField] private GameObject coffeeBodyPickupVisual;

    private KeyCode chalkKey = KeyCode.E;
    private int chalkInventory = 0;
    private int chalkHoldsPerCharge = 8;
    private KeyCode coffeeKey = KeyCode.Q;
    private int coffeeInventory = 0;

    void Awake()
    {
        if (pickupEffects == null) pickupEffects = GetComponent<ClimbingPickupEffects>();
        RefreshBodyPickupVisuals();
    }

    void Update()
    {
        if (Input.GetKeyDown(chalkKey))
        {
            if (chalkInventory <= 0)
            {
                return;
            }

            if (pickupEffects != null && pickupEffects.HoldsRemaining <= 0)
            {
                chalkInventory--;
                pickupEffects.ActivateChalkEffect(chalkHoldsPerCharge);
            }

            RefreshBodyPickupVisuals();
        }

        if (Input.GetKeyDown(coffeeKey))
        {
            if (coffeeInventory <= 0)
            {
                return;
            }

            if (pickupEffects != null && !pickupEffects.IsCoffeeEffectActive)
            {
                coffeeInventory--;
                pickupEffects.ActivateCoffeeEffect();
            }

            RefreshBodyPickupVisuals();
        }
    }

    public void AddChalk(int amount)
    {
        chalkInventory += amount;
        RefreshBodyPickupVisuals();
    }

    public void AddCoffee(int amount)
    {
        coffeeInventory += amount;
        RefreshBodyPickupVisuals();
    }

    private void RefreshBodyPickupVisuals()
    {
        if (chalkBodyPickupVisual != null)
        {
            chalkBodyPickupVisual.SetActive(chalkInventory > 0);
        }

        if (coffeeBodyPickupVisual != null)
        {
            coffeeBodyPickupVisual.SetActive(coffeeInventory > 0);
        }
    }
}