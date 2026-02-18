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
<<<<<<< Updated upstream
    
    public UnityEvent OnGrab;
    public UnityEvent OnRelease;
=======
>>>>>>> Stashed changes

    void Start() => currentStamina = maxStamina;

    void Update()
    {
        // Alleen handen reageren op input. 
        // Voeten worden door een extern script (feetsnapping of zoeits) op OnHold gezet.
<<<<<<< Updated upstream
        if (type == LimbType.Hand)
=======
        if (type == LimbType.LeftHand || type == LimbType.RightHand)
>>>>>>> Stashed changes
        {
            if (Input.GetKeyDown(interactionKey)) Grab();
            if (Input.GetKeyUp(interactionKey)) Release();
        }
<<<<<<< Updated upstream

        if (currentState == LimbState.OnHold)
=======
        
        if (limbInfo.GetState() == LimbState.holding)
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
        currentState = LimbState.OffHold;
=======
        limbInfo.SetState(LimbState.resting);
>>>>>>> Stashed changes
        OnRelease?.Invoke();
    }
}