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
        //Refactor dit later met een verzoek naar de hold of die beschikbaar is
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.forward * checkingDistance, Color.red, 1.0f);

        if (Physics.Raycast(transform.position, transform.forward, out hit, checkingDistance))
        {
            if (hit.collider.TryGetComponent<Hold>(out var behaviour))
            {
                limbInfo.SetState(LimbState.holding);
                GetComponent<HandMovement>().SetHold(hit.transform);
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
