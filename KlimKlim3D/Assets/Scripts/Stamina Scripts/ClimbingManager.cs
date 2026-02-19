using UnityEngine;
using System.Collections.Generic;

public class ClimbingManager : MonoBehaviour
{
    public List<LimbInfo> allLimbs;
    
    [Header("Base Drain Rates")]
    public float rate3Limbs = 10f;  
    public float rate2Limbs = 15f; 
    public float rate1Limb = 25f;  

    [Header("Chalk System")]
    public KeyCode activateChalkKey = KeyCode.E;
    public int chalkInventory = 0;        // Hoeveelheid 'beurten' van 8 holds
    public int holdsRemaining = 0;        // Resterende holds voor huidige beurt
    public float chalkStaminaMultiplier = 0.5f; // De reductie (halvering)
    
    private float Rate4Limbs => -rate1Limb; 

    void Start()
    {
        // Abonneer op de OnGrab events van de handen om holds te tellen
        foreach (var limb in allLimbs)
        {
            StaminaLimb stamina = limb.GetComponent<StaminaLimb>();
            if (stamina != null && (limb.GetTypeStrict() == LimbType.LeftHand || limb.GetTypeStrict() == LimbType.RightHand))
            {
                stamina.OnGrab.AddListener(OnHandGrabbed);
            }
        }
    }

    public void AddChalkToInventory(int amount) => chalkInventory += amount;

    void Update()
    {
        // Activatie via E
        if (Input.GetKeyDown(activateChalkKey) && chalkInventory > 0 && holdsRemaining <= 0)
        {
            UseChalkCharge();
        }

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

                // Pas multipliers toe op handen
                if ((limb.GetTypeStrict() == LimbType.LeftHand || limb.GetTypeStrict() == LimbType.RightHand) && baseRate > 0)
                {
                    finalRate *= footMultiplier;

                    // Pas Chalk toe als het effect actief is
                    if (holdsRemaining > 0)
                    {
                        finalRate *= chalkStaminaMultiplier;
                    }
                }

                StaminaLimb staminaLimb = limb.GetComponent<StaminaLimb>();
                staminaLimb.currentStamina -= finalRate * Time.deltaTime;
                staminaLimb.currentStamina = Mathf.Clamp(staminaLimb.currentStamina, 0, staminaLimb.maxStamina);
            }
            
            if (activeCount == 0) { /* Val logica */ }
        }
    }

    void UseChalkCharge()
    {
        chalkInventory--;
        holdsRemaining = 8;
        Debug.Log("Chalk geactiveerd! Effect werkt voor 8 holds.");
    }

    void OnHandGrabbed()
    {
        if (holdsRemaining > 0)
        {
            holdsRemaining--;
            if (holdsRemaining <= 0) Debug.Log("Chalk effect uitgewerkt.");
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