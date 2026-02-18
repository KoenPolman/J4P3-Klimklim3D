using UnityEngine;

public class HoldGrabManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private LimbInfo limbInfo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = transform.parent.parent.GetComponent<PlayerInput>();
        limbInfo = GetComponent<LimbInfo>();

        switch (limbInfo.GetTypeStrict())
        {
            case LimbType.LeftHand:
                playerInput.leftHandGrab.AddListener(CheckForValidGrab);    
                playerInput.leftHandRelease.AddListener(Reach);
                break;
            case LimbType.RightHand:
                playerInput.rightHandGrab.AddListener(CheckForValidGrab);
                playerInput.rightHandRelease.AddListener(Reach);
                break;
            case LimbType.LeftFoot:
                break;
            case LimbType.RightFoot:
                break;
        }
    }
    private void CheckForValidGrab()
    {
        //als hold valid is
        if (true)
        {
            //geef reference maa van hold aan de hand movement
            limbInfo.SetState(LimbState.holding);
        }
        else
        {
            limbInfo.SetState(LimbState.resting);
        }
    }
    private void Reach()
    {
        limbInfo.SetState(LimbState.reaching);
    }
}
