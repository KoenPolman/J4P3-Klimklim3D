using UnityEngine;
using System.Text;

public class StaminaStatusReporter : MonoBehaviour
{
    public ClimbingManager climbingManager; 

    void Update()
    {
        // Press 'Tab' to see the full status of all limbs in the Console
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            PrintFullStatus();
        }
    }

    public void PrintFullStatus()
    {
        if (climbingManager == null || climbingManager.allLimbs == null) return;

        StringBuilder report = new StringBuilder();
        report.AppendLine("=== CLIMBER STAMINA REPORT ===");

        foreach (var limb in climbingManager.allLimbs)
        {
            // Gather the data
            /*
            string name = limb.gameObject.name;
            float current = limb.currentStamina;
            float max = limb.maxStamina;
            LimbState state = limb.currentState;
            // Format: "LeftHand: 85/100 (OnHold)"
            report.AppendLine($"{name}: {current:F1}/{max} | State: {state}");
            */
        }

        Debug.Log(report.ToString());
    }

    // Use this if you want to get the data for another script
    public float GetTotalStaminaPercentage()
    {
        float totalMax = 0;
        float totalCurrent = 0;

        foreach (var limb in climbingManager.allLimbs)
        {
            /*
            totalMax += limb.maxStamina;
            totalCurrent += limb.currentStamina;
            */
        }

        return (totalCurrent / totalMax) * 100f;
    }
}