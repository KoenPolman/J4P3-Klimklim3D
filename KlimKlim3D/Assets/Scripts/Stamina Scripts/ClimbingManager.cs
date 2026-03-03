using UnityEngine;
using System.Collections.Generic;

public class ClimbingManager : MonoBehaviour
{
    public List<LimbInfo> allLimbs;
    private readonly Dictionary<LimbInfo, bool> handWasHolding = new();
    
    [Header("Base Drain Rates")]
    public float rate3Limbs = 10f;  
    public float rate2Limbs = 15f; 
    public float rate1Limb = 25f;  

    [Header("Active Effects")]
    public int holdsRemaining = 0; 
    public float chalkStaminaMultiplier = 0.5f;
    [SerializeField] private float coffeeDurationSeconds = 15f;
    private bool chalkEffectWasActive = false;
    private bool coffeeEffectWasActive = false;
    private float coffeeEffectEndTime = -1f;
    private float Rate4Limbs => -rate1Limb; 

    public bool IsCoffeeEffectActive => Time.time < coffeeEffectEndTime;

    void Start()
    {
        foreach (var limb in allLimbs)
        {
            if (IsHand(limb.GetTypeStrict()))
            {
                handWasHolding[limb] = limb.GetState() == LimbState.holding;
            }
        }
    }

    // Called by PickupManager
    public void ActivateChalkEffect(int holds)
    {
        holdsRemaining = Mathf.Max(0, holds);
        Debug.Log($"[ClimbingManager] Chalk effect activated for {holdsRemaining} holds.");
    }

    // Called by PickupManager
    public void ActivateCoffeeEffect()
    {
        coffeeEffectEndTime = Time.time + Mathf.Max(0f, coffeeDurationSeconds);
        Debug.Log($"[ClimbingManager] Coffee effect activated. Stamina drain paused for {coffeeDurationSeconds:0.#} seconds.");
    }

    void Update()
    {
        TrackHandGrabTransitions();

        bool chalkEffectIsActive = holdsRemaining > 0;

        if (chalkEffectWasActive && !chalkEffectIsActive)
        {
            Debug.Log("[ClimbingManager] Chalk effect has worn off.");
        }

        chalkEffectWasActive = chalkEffectIsActive;

        bool coffeeEffectIsActive = IsCoffeeEffectActive;

        if (coffeeEffectWasActive && !coffeeEffectIsActive)
        {
            Debug.Log("[ClimbingManager] Coffee effect has worn off.");
        }

        coffeeEffectWasActive = coffeeEffectIsActive;

        int activeCount = 0;
        int feetActive = 0;

        foreach (var limb in allLimbs)
        {
            if (limb.GetState() == LimbState.holding)
            {
                activeCount++;
                if (limb.GetTypeStrict() == LimbType.LeftFoot || limb.GetTypeStrict() == LimbType.RightFoot) feetActive++;
            }
        }

        float footMultiplier = GetFootMultiplier(feetActive);
        float baseRate = GetRate(activeCount);

        foreach (var limb in allLimbs)
        {
            if (limb.GetState() == LimbState.holding)
            {
                float finalRate = baseRate;

                if (IsHand(limb.GetTypeStrict()) && baseRate > 0)
                {
                    finalRate *= footMultiplier;

                    // Apply Chalk effect if active
                    if (holdsRemaining > 0) finalRate *= chalkStaminaMultiplier;
                }

                StaminaLimb staminaLimb = limb.GetComponent<StaminaLimb>();
                if (coffeeEffectIsActive) continue;
                staminaLimb.currentStamina -= finalRate * Time.deltaTime;
                staminaLimb.currentStamina = Mathf.Clamp(staminaLimb.currentStamina, 0, staminaLimb.maxStamina);
            }
        }
    }

    private void OnHandGrabbed()
    {
        if (holdsRemaining > 0)
        {
            holdsRemaining--;
            Debug.Log($"[ClimbingManager] Chalk hold consumed. Remaining: {holdsRemaining}.");

            if (holdsRemaining == 0)
            {
                Debug.Log("[ClimbingManager] Chalk effect expired after final boosted hold.");
            }
        }
    }

    private void TrackHandGrabTransitions()
    {
        foreach (var limb in allLimbs)
        {
            if (!IsHand(limb.GetTypeStrict())) continue;

            bool isHolding = limb.GetState() == LimbState.holding;
            bool wasHolding = handWasHolding.TryGetValue(limb, out var previous) && previous;

            if (!wasHolding && isHolding)
            {
                Debug.Log($"[ClimbingManager] New hand hold detected on {limb.name}.");
                OnHandGrabbed();
            }

            handWasHolding[limb] = isHolding;
        }
    }

    float GetFootMultiplier(int feet) => feet switch { 2 => 0.5f, 1 => 1.0f, 0 => 2.5f, _ => 1.0f };
    bool IsHand(LimbType t) => t == LimbType.LeftHand || t == LimbType.RightHand;
    float GetRate(int count) => count switch { 4 => Rate4Limbs, 3 => rate3Limbs, 2 => rate2Limbs, 1 => rate1Limb, _ => 0 };
}