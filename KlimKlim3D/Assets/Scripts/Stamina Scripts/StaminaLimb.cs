using UnityEngine;
using UnityEngine.Events;

public enum LimbState { OffHold, OnHold, Falling }

public class StaminaLimb : MonoBehaviour
{
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
        if (Input.GetKeyDown(interactionKey)) Grab();
        if (Input.GetKeyUp(interactionKey)) Release();

        if (currentState == LimbState.OnHold)
        {
            // Manager will handle the actual subtraction logic
            if (currentStamina <= 0) Release();
        }
    }

    void Grab()
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