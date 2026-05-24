using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
// Script written by Koen Polman for KlimKlim3D i.e. Master project, 2/2026 - 4/2026
public class FootPlacement : MonoBehaviour
{
    private Hold[] holds;// References to holds
    private Transform restingPosition;
    private HandMovement handMovement;
    private LimbInfo limbInfo;
    private LimbInfo otherLimbInfo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        holds = FindObjectsByType<Hold>(FindObjectsSortMode.None);// Get references to holds
        handMovement = GetComponent<HandMovement>();
        restingPosition = handMovement.GetRestingPosition();
        limbInfo = GetComponent<LimbInfo>();
        if (holds.Length <= 0)
        {
            Debug.Log("FootPlacement has been disabled");
            this.enabled = false;
        }
        Debug.Log("FootPlacement has been enabled");

        // +Vind de andere voet
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Hold targetHold = holds[0];
        List<Hold> validHolds = new List<Hold>();
        Debug.Log("Total count of holds in level: " + holds.Length);
        // Check for holds in range
        for (int i = 0; i < holds.Length; i++)
        {
            if (holds[i].GetHoldAvailability(limbInfo.GetTypeStrict()) && Vector3.Distance(transform.parent.position, holds[i].transform.position) < handMovement.GetMaxRadius())
            {
                validHolds.Add(holds[i]);
            }
        }
        Debug.Log("Total count of found valid holds: " + validHolds.Count);
        // If no hold is available go to the resting position
        if (validHolds.Count <= 0)
        {
            Debug.Log("Foot placement ended early for no valid holds were found");
            limbInfo.SetState(LimbState.resting);
            limbInfo.RemoveLimbFromHold();
            return;
        }

        // +Check of dat de hold al de andere voet er op heeft staan

        // Decide which hold is closest to the resting position
        for (int i = 0; i < validHolds.Count; i++)
        {
            if (Vector3.Distance(validHolds[i].transform.position, restingPosition.position) < Vector3.Distance(targetHold.transform.position, restingPosition.position))
            {
                targetHold = validHolds[i];
            }
        }

        // Place limb on the hold with selected hold
        limbInfo.PlaceLimbOnHold(targetHold.transform);
    }
}
