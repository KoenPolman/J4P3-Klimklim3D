using UnityEngine;
public class HoldGrabManager : MonoBehaviour
{
    [SerializeField] float checkingDistance;
    [SerializeField] PlayerInput playerInput;
    private LimbInfo limbInfo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = PlayerInput.Instance;
        
        limbInfo = GetComponent<LimbInfo>();

        switch (limbInfo.GetTypeStrict())
        {
            case LimbType.LeftHand:
                playerInput.leftHandGrab.AddListener(GrabHandle);    
                playerInput.leftHandRelease.AddListener(Reach);
                break;
            case LimbType.RightHand:
                playerInput.rightHandGrab.AddListener(GrabHandle);
                playerInput.rightHandRelease.AddListener(Reach);
                break;
            case LimbType.LeftFoot:
                //nothing
                break;
            case LimbType.RightFoot:
                //nothing
                break;
        }
    }
    private void GrabHandle()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.forward * checkingDistance, Color.red, 1.0f);

        if (Physics.Raycast(transform.position, transform.forward, out hit, checkingDistance))
        {
            if (hit.collider.TryGetComponent<Hold>(out var behaviour))
            {
                var holdType = behaviour.GetHoldData().holdType;
                var limbType = limbInfo.GetTypeStrict();
                bool canHold = false;
                string logText = null;

                switch (holdType)
                {
                    case HoldType.both:
                        canHold = true;
                        logText = "both";
                        break;
                    case HoldType.hand:
                        canHold = limbType == LimbType.LeftHand || limbType == LimbType.RightHand;
                        logText = "hand";
                        break;
                    case HoldType.foot:
                        canHold = limbType == LimbType.LeftFoot || limbType == LimbType.RightFoot;
                        logText = "foot";
                        break;
                }

                if (canHold)
                {
                    limbInfo.SetState(LimbState.holding);
                    GetComponent<HandMovement>().SetHold(hit.transform);
                    if(logText != null && logText != "foot") // only log for "both" and "hand" as in your original code
                        Debug.Log(logText);
                }
                else
                {
                    Debug.LogWarning("Hold type is not compatible with limb type");
                }
                return;
            }
        }
        limbInfo.SetState(LimbState.resting);
    }
    private void Reach()
    {
        limbInfo.SetState(LimbState.reaching);
    }
}