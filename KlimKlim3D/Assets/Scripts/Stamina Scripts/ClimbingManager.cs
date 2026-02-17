using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class ClimbingManager : MonoBehaviour
{
    public List<LimbInfo> allLimbs;
    
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
            if (limb.GetState() == LimbState.holding)
            {
                activeCount++;
                if (limb.GetTypeStrict() == LimbType.LeftFoot || limb.GetTypeStrict() == LimbType.RightFoot) feetActive++;
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
            if (limb.GetState() == LimbState.holding)
            {
                float finalRate = baseRate;

                // Alleen handen profiteren van de voet-multiplier bij verbruik
                if ((limb.GetTypeStrict() == LimbType.LeftHand || limb.GetTypeStrict() == LimbType.RightHand) && baseRate > 0)
                {
                    finalRate *= footMultiplier;
                }
                StaminaLimb staminaLimb = limb.GetComponent<StaminaLimb>();
                staminaLimb.currentStamina -= finalRate * Time.deltaTime;
                staminaLimb.currentStamina = Mathf.Clamp(staminaLimb.currentStamina, 0, staminaLimb.maxStamina);
            }
            
            // Als NIETS wordt vastgehouden, valt de speler
            if (activeCount == 0)
            {
                // voeg logica toe voor vallen of beter voeg hier later een call toe naar de core playermovement
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