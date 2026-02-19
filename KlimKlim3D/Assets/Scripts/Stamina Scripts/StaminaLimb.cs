using UnityEngine;
using UnityEngine.Events;


public class StaminaLimb : MonoBehaviour
{
    public KeyCode interactionKey;
    public LimbInfo limbInfo;
    
    [Header("Stamina Settings")]
    public float maxStamina = 100f;
    [HideInInspector] public float currentStamina;

    [HideInInspector] public UnityEvent OnGrab;
    [HideInInspector] public UnityEvent OnRelease;

    void Start()
    {
        currentStamina = maxStamina;
        if (limbInfo == null)
        {
            limbInfo = GetComponent<LimbInfo>();
        }
    }

    void Update()
    {
        // Alleen handen reageren op input. 
        // Voeten worden door een extern script (feetsnapping of zoeits) op OnHold gezet.
        if (limbInfo.GetTypeStrict() == LimbType.LeftHand || limbInfo.GetTypeStrict() == LimbType.RightHand)
        {
            if (Input.GetKeyDown(interactionKey)) Grab();
            if (Input.GetKeyUp(interactionKey)) Release();
        }
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
        limbInfo.SetState(LimbState.resting);
        OnRelease?.Invoke();
    }
}