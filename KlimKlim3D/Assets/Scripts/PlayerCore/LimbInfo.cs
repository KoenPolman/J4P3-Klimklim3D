using UnityEngine;

public class LimbInfo : MonoBehaviour
{
    [SerializeField] LimbType type;
    private LimbState state = LimbState.resting;
    private HandMovement handMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        handMovement = GetComponent<HandMovement>();
    }
    public LimbState GetState()
    {
        return state;
    }
    public void SetState(LimbState newState)
    {
        state = newState;
    }
    /// <summary>
    /// Gets the type of this limb, left foot, right hand, etc
    /// </summary>
    /// <returns></returns>
    public LimbType GetTypeStrict()
    {
        return type;
    }
    /// <summary>
    /// check if limb is at maximum range, not final
    /// </summary>
    /// <returns></returns>
    public bool IsAtMaxRange()
    {
        return false;
    }
    /// <summary>
    /// Places this limb on hold, also takes care of checking in and out of the hold to account for maximum hold capacity
    /// </summary>
    /// <param name="targetHold"></param>
    public void PlaceLimbOnHold(Transform targetHold)
    {
        Debug.Log("Limb has been placed on hold");
        if (handMovement.GetHold() != null)
        {
            handMovement.GetHold().GetComponent<Hold>().CheckOut();
        }
        state = LimbState.holding;
        targetHold.GetComponent<Hold>().CheckIn();
        handMovement.SetHold(targetHold);
    }
}
