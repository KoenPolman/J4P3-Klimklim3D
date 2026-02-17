using UnityEngine;
using UnityEngine.Events;


public class StaminaLimb : MonoBehaviour
{
    public LimbType type; 
    public KeyCode interactionKey;
    public LimbInfo limbInfo;
    
    [Header("Stamina Settings")]
    public float maxStamina = 100f;
    public float currentStamina;
    
    public UnityEvent OnGrab;
    public UnityEvent OnRelease;

    void Start()
    {
        currentStamina = maxStamina;
        limbInfo = GetComponent<LimbInfo>();
    }

    void Update()
    {
        // Alleen handen reageren op input. 
        // Voeten worden door een extern script (feetsnapping of zoeits) op OnHold gezet.
        /*
        if (type == LimbType.Hand)
        {
            if (Input.GetKeyDown(interactionKey)) Grab();
            if (Input.GetKeyUp(interactionKey)) Release();
        }
        */
        if (limbInfo.GetState() == LimbState.holding)
        {
            // Als de stamina op is, laat de ledemaat los
            if (currentStamina <= 0) Release();
        }
    }

    public void Grab()
    {
        limbInfo.SetState(LimbState.holding);
        OnGrab?.Invoke();
    }

    public void Release()
    {
        limbInfo.SetState(LimbState.holding);
        OnRelease?.Invoke();
    }
}