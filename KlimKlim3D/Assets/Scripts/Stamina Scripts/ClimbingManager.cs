using UnityEngine;
using System.Collections.Generic;

public class ClimbingManager : MonoBehaviour
{
    public List<StaminaLimb> allLimbs;
    
    [Header("Drain Rates (Positive = Drain, Negative = Regrow)")]
    public float rate3Limbs = 2f;  // Slow drain
    public float rate2Limbs = 10f; // Moderate
    public float rate1Limb = 25f;  // Fast

    // Logic: 4 limbs = negative of 3 limbs rate
    private float Rate4Limbs => -rate3Limbs; 

    void Update()
    {
        int activeCount = 0;
        foreach (var limb in allLimbs)
        {
            if (limb.currentState == LimbState.OnHold) activeCount++;
        }

        float currentRate = GetRate(activeCount);

        foreach (var limb in allLimbs)
        {
            // If we are holding, apply drain or regeneration
            if (limb.currentState == LimbState.OnHold)
            {
                limb.currentStamina -= currentRate * Time.deltaTime;
                limb.currentStamina = Mathf.Clamp(limb.currentStamina, 0, limb.maxStamina);
            }
            
            // If NOTHING is holding, everybody falls
            if (activeCount == 0)
            {
                limb.currentState = LimbState.Falling;
            }
        }
    }

    float GetRate(int count)
    {
        return count switch
        {
            4 => Rate4Limbs,
            3 => rate3Limbs,
            2 => rate2Limbs,
            1 => rate1Limb,
            _ => 0
        };
    }
}