using UnityEngine;
using System.Collections.Generic;

public class ClimbingManager : MonoBehaviour
{
    public List<StaminaLimb> allLimbs;
    
    [Header("Base Drain Rates")]
    public float rate3Limbs = 10f;  
    public float rate2Limbs = 15f; 
    public float rate1Limb = 25f;  

    private float Rate4Limbs => -rate1Limb; // Snelheid van herstel bij rust

    void Update()
    {
        int activeCount = 0;
        int feetActive = 0;

        // Tel actieve ledematen en specifiek actieve voeten
        foreach (var limb in allLimbs)
        {
            if (limb.currentState == LimbState.OnHold)
            {
                activeCount++;
                if (limb.type == LimbType.Foot) feetActive++;
            }
        }

        // De "Foundation" Logica:
        // 2 voeten: Handen verbruiken 0.5x (Rustig)
        // 1 voet: Handen verbruiken 1.0x (Normaal)
        // 0 voeten: Handen verbruiken 2.5x (Zwaar)
        float footMultiplier = feetActive switch {
            2 => 0.5f,
            1 => 1.0f,
            0 => 2.5f,
            _ => 1.0f
        };

        float baseRate = GetRate(activeCount);

        foreach (var limb in allLimbs)
        {
            if (limb.currentState == LimbState.OnHold)
            {
                float finalRate = baseRate;

                // Alleen handen profiteren van de voet-multiplier bij verbruik
                if (limb.type == LimbType.Hand && baseRate > 0)
                {
                    finalRate *= footMultiplier;
                }

                limb.currentStamina -= finalRate * Time.deltaTime;
                limb.currentStamina = Mathf.Clamp(limb.currentStamina, 0, limb.maxStamina);
            }
            
            // Als NIETS wordt vastgehouden, valt de speler
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