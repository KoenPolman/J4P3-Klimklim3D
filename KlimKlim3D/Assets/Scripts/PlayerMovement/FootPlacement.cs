using NUnit.Framework;
using UnityEngine;

public class FootPlacement : MonoBehaviour
{
    private Hold[] holds;//references to holds
    private Transform restingPosition;
    private HandMovement handMovement;
    private LimbInfo limbInfo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        holds = FindObjectsByType<Hold>(FindObjectsSortMode.None);//get references to holds
        handMovement = GetComponent<HandMovement>();
        restingPosition = handMovement.GetRestingPosition();
        limbInfo = GetComponent<LimbInfo>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //check which hold is closest to the resting position of the foot
        Hold targetHold = holds[0];
        for (int i = 0; i < holds.Length; i++)
        {
            if (Vector3.Distance(holds[i].transform.position, restingPosition.position) < Vector3.Distance(targetHold.transform.position, restingPosition.position))
            {
                targetHold = holds[i];
            }
        }
        //check if hold is available and within range
        if (targetHold.GetHoldAvailability(limbInfo.GetTypeStrict()) && Vector3.Distance(transform.parent.position, targetHold.transform.position) <= handMovement.GetMaxRadius())
        {
            //update limb info which cascades into the foot actually moving to that hold
            limbInfo.SetState(LimbState.holding);
            handMovement.SetHold(targetHold.transform);
        }
        else
        {
            //if no hold is available go to the resting position
            limbInfo.SetState(LimbState.resting);
        }
    }
}
