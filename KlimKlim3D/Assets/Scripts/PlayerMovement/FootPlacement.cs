using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// Script written by Koen Polman for KlimKlim3D i.e. Master project, 2/2026 - 4/2026
public class FootPlacement : MonoBehaviour
{
    private Hold[] holds;// References to holds
    private Transform restingPosition;
    private HandMovement handMovement;
    private LimbInfo limbInfo;
    private PlayerInput playerInput;
    void Start()
    {
        // Get references to holds
        holds = FindObjectsByType<Hold>(FindObjectsSortMode.None);
        handMovement = GetComponent<HandMovement>();
        restingPosition = handMovement.GetRestingPosition();
        limbInfo = GetComponent<LimbInfo>();
        playerInput = FindFirstObjectByType<PlayerInput>();

        // Failsafe for a test scenarios
        if (holds.Length <= 0)
        {
            this.enabled = false;
        }

        switch (limbInfo.GetTypeStrict())
        {
            case LimbType.LeftHand:
                // Nothing
                break;
            case LimbType.RightHand:
                // Nothing
                break;
            case LimbType.LeftFoot:
                playerInput.leftFootPlace.AddListener(AttemptToPlaceFeet);
                break;
            case LimbType.RightFoot:
                playerInput.rightFootPlace.AddListener(AttemptToPlaceFeet);
                break;
        }
    }

    private void FixedUpdate()
    {
        Transform currentHold = handMovement.GetHold();
        if (currentHold == null)
            return;

        if (Vector3.Distance(transform.parent.position, currentHold.position) > handMovement.GetMaxRadius())
        {
            limbInfo.SetState(LimbState.resting);
            limbInfo.RemoveLimbFromHold();
        }
    }
    public void AttemptToPlaceFeet()
    {
        if (holds.Length <= 0)
        {
            return;
        }

        // Start out with a list of all the holds in the level
        Hold targetHold = holds[0];
        List<Hold> validHolds = new List<Hold>();

        // Trim down the list to all the holds that are within range of the foot
        for (int i = 0; i < holds.Length; i++)
        {
            if (holds[i].GetHoldAvailability(limbInfo.GetTypeStrict()) && Vector3.Distance(transform.parent.position, holds[i].transform.position) < handMovement.GetMaxRadius())
            {
                validHolds.Add(holds[i]);
            }
        }
        // Trim down the list again but to all the holds that are available (this can also be combined with the previous trim)

        // If no hold is available go to the resting position
        if (validHolds.Count <= 0)
        {
            //Debug.Log("Foot placement ended early for no valid holds were found");
            limbInfo.SetState(LimbState.resting);
            limbInfo.RemoveLimbFromHold();
            return;
        }

        // Select the hold from the double-trimmed list that is the closest to the resting position of the foot
        for (int i = 0; i < validHolds.Count; i++)
        {
            if (Vector3.Distance(validHolds[i].transform.position, restingPosition.position) < Vector3.Distance(targetHold.transform.position, restingPosition.position))
            {
                targetHold = validHolds[i];
            }
        }

        limbInfo.PlaceLimbOnHold(targetHold.transform);
    }
}
