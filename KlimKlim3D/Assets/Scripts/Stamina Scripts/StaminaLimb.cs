using UnityEngine;
using UnityEngine.Events;

public enum LimbState { OffHold, OnHold, Falling }
public enum LimbType { Hand, Foot }

public class StaminaLimb : MonoBehaviour
{
    public LimbType type; 
    public KeyCode interactionKey;
    public LimbState currentState = LimbState.OffHold;
    
    [Header("Stamina Settings")]
    public float maxStamina = 100f;
    public float currentStamina;
    
    public UnityEvent OnGrab;
    public UnityEvent OnRelease;

    void Start() => currentStamina = maxStamina;

    void Update()
    {
        // Alleen handen reageren op input. 
        // Voeten worden door een extern script (feetsnapping of zoeits) op OnHold gezet.
        if (type == LimbType.Hand)
        {
            if (Input.GetKeyDown(interactionKey)) Grab();
            if (Input.GetKeyUp(interactionKey)) Release();
        }

        if (currentState == LimbState.OnHold)
        {
            // Als de stamina op is, laat de ledemaat los
            if (currentStamina <= 0) Release();
        }
    }

    public void Grab()
    {
        currentState = LimbState.OnHold;
        OnGrab?.Invoke();
    }

    public void Release()
    {
        currentState = LimbState.OffHold;
        OnRelease?.Invoke();
    }
}