using UnityEngine;
using System.Collections.Generic;

public class ClimbingManager : MonoBehaviour
{
    public List<LimbInfo> allLimbs;
    
    [Header("Base Drain Rates")]
    public float rate3Limbs = 10f;  
    public float rate2Limbs = 15f; 
    public float rate1Limb = 25f;  

    [Header("Active Effects")]
    public int holdsRemaining = 0; 
    public float chalkStaminaMultiplier = 0.5f;
    
    private float Rate4Limbs => -rate1Limb; 

    void Start()
    {
        foreach (var limb in allLimbs)
        {
            StaminaLimb stamina = limb.GetComponent<StaminaLimb>();
            if (stamina != null && (limb.GetTypeStrict() == LimbType.LeftHand || limb.GetTypeStrict() == LimbType.RightHand))
            {
                stamina.OnGrab.AddListener(OnHandGrabbed);
            }
        }
    }

    // Called by PickupManager
    public void ActivateChalkEffect(int holds)
    {
        holdsRemaining = holds;
    }

    void Update()
    {
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
        }
    }

    float GetFootMultiplier(int feet) => feet switch { 2 => 0.5f, 1 => 1.0f, 0 => 2.5f, _ => 1.0f };
    bool IsHand(LimbType t) => t == LimbType.LeftHand || t == LimbType.RightHand;

    float GetRate(int count) => count switch { 4 => Rate4Limbs, 3 => rate3Limbs, 2 => rate2Limbs, 1 => rate1Limb, _ => 0 };
}