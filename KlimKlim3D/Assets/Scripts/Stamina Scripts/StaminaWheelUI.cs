using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class StaminaWheelUI : MonoBehaviour
{
    public StaminaLimb targetLimb;
    public Image donutFill;
    
    [Header("Visual Tweaks")]
    public float fadeSpeed = 10f;
    public Gradient staminaColor; // Set this in the Inspector!
    
    [Header("Panic Settings (Low Stamina)")]
    public float panicThreshold = 0.25f; // Starts at 25%
    public float pulseSpeed = 15f;
    public float pulseAmount = 0.1f;

    private CanvasGroup canvasGroup;
    private Vector3 originalScale;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        originalScale = transform.localScale;
        canvasGroup.alpha = 0; 
    }

    void Update()
    {
        if (targetLimb == null || donutFill == null) return;

        float ratio = targetLimb.currentStamina / targetLimb.maxStamina;

        // 1. Smooth Fade In/Out
        float targetAlpha = (targetLimb.currentState == LimbState.OnHold) ? 1f : 0f;
        canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, Time.deltaTime * fadeSpeed);

        if (canvasGroup.alpha > 0)
        {
            // 2. Update Fill
            donutFill.fillAmount = ratio;

            // 3. Update Color (Green -> Orange -> Red)
            // Evaluates the gradient based on 0.0 to 1.0
            donutFill.color = staminaColor.Evaluate(ratio);

            // 4. Panic Pulse Logic
            HandlePulse(ratio);
        }
        else
        {
            // Reset scale when hidden so it doesn't stay "bloated"
            transform.localScale = originalScale;
        }
    }

    void HandlePulse(float ratio)
    {
        if (ratio <= panicThreshold)
        {
            // Simple Sine wave math for a heartbeat effect
            float pulse = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
            transform.localScale = originalScale * pulse;
        }
        else
        {
            // Return to normal size if we recover stamina
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * 5f);
        }
    }
}