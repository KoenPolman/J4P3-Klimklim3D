using UnityEngine;
using System.Collections.Generic;

public class ClimbingManager : MonoBehaviour
{
    public List<LimbInfo> allLimbs;
    [SerializeField] private ClimbingPickupStatusEffects pickupEffects;
    private readonly Dictionary<LimbInfo, bool> handWasHolding = new();

    // Stamina drain rates per limb count
    [SerializeField] float rate3Limbs = 10f;
    [SerializeField] float rate2Limbs = 15f;
    [SerializeField] float rate1Limb = 25f;  
    private float Rate4Limbs => -rate1Limb; 

    void Awake()
    {
        if (pickupEffects == null) pickupEffects = GetComponent<ClimbingPickupStatusEffects>();
    }

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

    void Update()
    {
        TrackHandGrabTransitions();

        bool coffeeEffectIsActive = pickupEffects != null && pickupEffects.IsCoffeeEffectActive;

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
                    if (pickupEffects != null && pickupEffects.holdsRemaining > 0)
                    {
                        finalRate *= pickupEffects.ChalkStaminaMultiplier;
                    }
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
        pickupEffects?.consumeHandHold();
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
                OnHandGrabbed();
            }

            handWasHolding[limb] = isHolding;
        }
    }

    float GetFootMultiplier(int feet) => feet switch { 2 => 0.5f, 1 => 1.0f, 0 => 2.5f, _ => 1.0f };
    bool IsHand(LimbType t) => t == LimbType.LeftHand || t == LimbType.RightHand;
    float GetRate(int count) => count switch { 4 => Rate4Limbs, 3 => rate3Limbs, 2 => rate2Limbs, 1 => rate1Limb, _ => 0 };
}