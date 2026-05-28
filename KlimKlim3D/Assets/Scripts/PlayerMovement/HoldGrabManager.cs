using Mono.Cecil;
using UnityEngine;
// Script written by Koen Polman for KlimKlim3D i.e. Master project, 2/2026 - 4/2026
public class HoldGrabManager : MonoBehaviour
{
    [SerializeField] float checkingDistance;
    [SerializeField] PlayerInput playerInput;
    private LimbInfo limbInfo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Input can be added trough serialize field, if not the behavior is attempted to be retrieved by code
        if (playerInput == null)
        {
            playerInput = FindFirstObjectByType<PlayerInput>();
        }
        limbInfo = GetComponent<LimbInfo>();

        // Depending on the type of limb this script is on the corresponding input events are assinged to the functions in this script
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
                // Nothing
                break;
            case LimbType.RightFoot:
                // Nothing
                break;
        }
    }
    /// <summary>
    /// Validate the existance and availibility of a hold and if so grab it 
    /// </summary>
    private void GrabHandle()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, checkingDistance))
        {
            if (hit.collider.TryGetComponent<Hold>(out var behaviour))
            {
                if (!behaviour.GetHoldAvailability(limbInfo.GetTypeStrict()))
                {
                    return;
                }
                limbInfo.PlaceLimbOnHold(hit.transform);
                return;
            }
        }
        limbInfo.SetState(LimbState.resting);
    }
    /// <summary>
    /// Sets the LimbState to reaching
    /// </summary>
    private void Reach()
    {
        limbInfo.SetState(LimbState.reaching);
    }
}
