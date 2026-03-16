using NUnit.Framework;
using UnityEngine;

public class FootPlacement : MonoBehaviour
{
    private Hold[] holds;// References to holds
    private Transform restingPosition;
    private HandMovement handMovement;
    private LimbInfo limbInfo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        holds = FindObjectsByType<Hold>(FindObjectsSortMode.None);// Get references to holds
        handMovement = GetComponent<HandMovement>();
        restingPosition = handMovement.GetRestingPosition();
        limbInfo = GetComponent<LimbInfo>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check which hold is closest to the resting position of the foot
        Hold targetHold = holds[0];
        for (int i = 0; i < holds.Length; i++)
        {
            if (Vector3.Distance(holds[i].transform.position, restingPosition.position) < Vector3.Distance(targetHold.transform.position, restingPosition.position))
            {
                targetHold = holds[i];
            }
        }
        // Check if hold is available and within range
        if (targetHold.GetHoldAvailability(limbInfo.GetTypeStrict()) && Vector3.Distance(transform.parent.position, targetHold.transform.position) <= handMovement.GetMaxRadius())
        {
            limbInfo.PlaceLimbOnHold(targetHold.transform);
        }
        else
        {
            // If no hold is available go to the resting position
            limbInfo.SetState(LimbState.resting);
        }
    }
}
