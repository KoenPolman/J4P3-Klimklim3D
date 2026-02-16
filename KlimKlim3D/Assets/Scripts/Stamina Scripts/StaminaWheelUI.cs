using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class StaminaWheelUI : MonoBehaviour
{
    public StaminaLimb targetLimb;
    public Image donutFill;
    
    [Header("Positioning")]
    public bool followHandPosition = true;
    public Vector3 offset = new Vector3(0, 100, 0);

    [Header("Visual Tweaks")]
    public float fadeSpeed = 10f;
    public Gradient staminaColor; 
    
    [Header("Panic Settings")]
    public float panicThreshold = 0.25f; 
    public float pulseSpeed = 15f;
    public float pulseAmount = 0.1f;
    public float jitterAmount = 5f; // Kracht van het trillen

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
        if (targetLimb == null || donutFill == null) return;

        if (followHandPosition) UpdatePosition();

        float ratio = targetLimb.currentStamina / targetLimb.maxStamina;

        // Fade in/out op basis van state
        float targetAlpha = (targetLimb.currentState == LimbState.OnHold) ? 1f : 0f;
        canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, Time.deltaTime * fadeSpeed);

        if (canvasGroup.alpha > 0)
        {
            donutFill.fillAmount = ratio;
            donutFill.color = staminaColor.Evaluate(ratio);
            HandlePanicEffects(ratio);
        }
        else
        {
            ResetVisuals();
        }
    }

    void UpdatePosition()
    {
        // Volg de 3D hand in 2D UI space
        Vector3 screenPos = mainCam.WorldToScreenPoint(targetLimb.transform.position);
        targetScreenPos = screenPos + offset;
        rectTransform.position = targetScreenPos;
    }

    void HandlePanicEffects(float ratio)
    {
        if (ratio <= panicThreshold)
        {
            // Hartslag Pulse
            float pulse = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
            transform.localScale = originalScale * pulse;

            // Paniek Trilling (Jitter)
            Vector3 shake = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0) * jitterAmount;
            rectTransform.position = targetScreenPos + shake;
        }
        else
        {
            ResetVisuals();
        }
    }

    void ResetVisuals()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * 5f);
    }
}