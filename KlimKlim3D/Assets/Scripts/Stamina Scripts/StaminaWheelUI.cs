using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class StaminaWheelUI : MonoBehaviour
{
    public LimbInfo limbInfo;
    public StaminaLimb staminaLimb;
    public Image donutFill;
    
    // Positioning
    public Vector3 offset = new Vector3(0, 100, 0);
   
    // Visual Effects
    private float fadeSpeed = 10f;
    [SerializeField] private Gradient staminaColor;
    [SerializeField] private ClimbingPickupStatusEffects pickupEffects;

    // Rainbow (coffee) effect parameters
    private float rainbowSpeed = 1f;

    // Panic effects parameters
    private float panicThreshold = 0.25f; 
    private float pulseSpeed = 15f;
    private float pulseAmount = 0.1f;
    private float jitterAmount = 5f; 
    
    // Internal references
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Vector3 originalScale;
    private Vector3 targetScreenPos;
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
        if (staminaLimb == null || donutFill == null) return;

        UpdatePosition();

        float ratio = staminaLimb.currentStamina / staminaLimb.maxStamina;

        float targetAlpha = (limbInfo.GetState() == LimbState.holding) ? 1f : 0f;
        canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, Time.deltaTime * fadeSpeed);

        if (canvasGroup.alpha > 0)
        {
            donutFill.fillAmount = ratio;

            bool coffeeActive = pickupEffects != null && pickupEffects.IsCoffeeEffectActive;
            if (coffeeActive)
            {
                float hue = Mathf.Repeat(Time.time * rainbowSpeed, 1f);
                donutFill.color = Color.HSVToRGB(hue, 1f, 1f);
            }
            else
            {
                donutFill.color = staminaColor.Evaluate(ratio);
            }

            HandlePanicEffects(ratio);
        }
        else
        {
            ResetVisuals();
        }
    }

    // Position the UI above the hand
    void UpdatePosition()   
    {
        Vector3 screenPos = mainCam.WorldToScreenPoint(staminaLimb.transform.position);
        targetScreenPos = screenPos + offset;
        rectTransform.position = targetScreenPos;
    }

    // Visual effects when stamina is low
    void HandlePanicEffects(float ratio)
    {
        if (ratio <= panicThreshold)
        {  
            // Pulse
            float pulse = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
            transform.localScale = originalScale * pulse;

            // Jitter
            Vector3 shake = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0) * jitterAmount;
            rectTransform.position = targetScreenPos + shake;
        }
        else
        {
            ResetVisuals();
        }
    }

    // Smoothly return to original scale and position
    void ResetVisuals()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * 5f);
    }
}