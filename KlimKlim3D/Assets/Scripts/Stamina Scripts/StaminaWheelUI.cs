using UnityEngine;
using UnityEngine.UI;

public class StaminaWheelUI : MonoBehaviour
{
    public StaminaLimb targetLimb;
    public Image donutFill;
    
    // We use CanvasGroup instead of GameObject for visibility
    private CanvasGroup canvasGroup; 

    void Start()
    {
        // Automatically find the CanvasGroup on this object
        canvasGroup = GetComponent<CanvasGroup>();
        
        if (canvasGroup == null)
        {
            Debug.LogError($"Please add a CanvasGroup component to {gameObject.name}!");
        }
    }

    void Update()
    {
        if (targetLimb == null || canvasGroup == null) return;

        if (targetLimb.currentState == LimbState.OnHold)
        {
            // Make it visible
            canvasGroup.alpha = 1f;
            donutFill.fillAmount = targetLimb.currentStamina / targetLimb.maxStamina;
        }
        else
        {
            // Make it invisible but keep the script running
            canvasGroup.alpha = 0f;
        }
    }
}