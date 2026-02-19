using UnityEngine;

public class LimbInfo : MonoBehaviour
{
    [SerializeField] LimbType type;
    private LimbState state = LimbState.resting;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
    /*
    public LimbType GetType()
    {

    }
    */
}
