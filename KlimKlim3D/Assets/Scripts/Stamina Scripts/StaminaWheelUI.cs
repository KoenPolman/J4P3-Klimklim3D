using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class StaminaWheelUI : MonoBehaviour
{
    public StaminaLimb targetLimb;
    public Image donutFill;
    
    [Header("Positioning")]
    public bool followHandPosition = true;
    public Vector3 offset = new Vector3(0, 100, 0); // Pixels above hand

    [Header("Visual Tweaks")]
    public float fadeSpeed = 10f;
    public Gradient staminaColor; 
    
    [Header("Panic Settings")]
    public float panicThreshold = 0.25f; 
    public float pulseSpeed = 15f;
    public float pulseAmount = 0.1f;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Vector3 originalScale;
    private Camera mainCam;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        originalScale = transform.localScale;
        mainCam = Camera.main;
        canvasGroup.alpha = 0; 
    }

    void Update()
    {
        if (targetLimb == null || donutFill == null) return;

        // 1. Logic: Follow the hand in screen space
        if (followHandPosition)
        {
            UpdatePosition();
        }

        float ratio = targetLimb.currentStamina / targetLimb.maxStamina;

        // 2. Smooth Fade
        float targetAlpha = (targetLimb.currentState == LimbState.OnHold) ? 1f : 0f;
        canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, Time.deltaTime * fadeSpeed);

        if (canvasGroup.alpha > 0)
        {
            donutFill.fillAmount = ratio;
            donutFill.color = staminaColor.Evaluate(ratio);
            HandlePulse(ratio);
        }
        else
        {
            transform.localScale = originalScale;
        }
    }

    void UpdatePosition()
    {
        // Convert the 3D Hand position to a 2D Screen position
        Vector3 screenPos = mainCam.WorldToScreenPoint(targetLimb.transform.position);
        
        // Apply to the UI element
        rectTransform.position = screenPos + offset;
    }

    void HandlePulse(float ratio)
    {
        if (ratio <= panicThreshold)
        {
            float pulse = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
            transform.localScale = originalScale * pulse;
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * 5f);
        }
    }
}