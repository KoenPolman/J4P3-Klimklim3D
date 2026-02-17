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
