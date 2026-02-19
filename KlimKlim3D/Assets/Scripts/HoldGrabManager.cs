using Unity.VisualScripting;
using UnityEngine;

using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class HoldGrabManager : MonoBehaviour
{
    [SerializeField] float checkingDistance;

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
                playerInput.leftHandGrab.AddListener(GrabHandle);    
                playerInput.leftHandRelease.AddListener(Reach);
                break;
            case LimbType.RightHand:
                playerInput.rightHandGrab.AddListener(GrabHandle);
                playerInput.rightHandRelease.AddListener(Reach);
                break;
            case LimbType.LeftFoot:
                break;
            case LimbType.RightFoot:
                break;
        }
    }
    private void GrabHandle()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, checkingDistance))
        {
            if (hit.collider.TryGetComponent<Hold>(out var behaviour))
            {
                limbInfo.SetState(LimbState.holding);
                GetComponent<HandMovement>().SetHold(hit.transform.position);
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
