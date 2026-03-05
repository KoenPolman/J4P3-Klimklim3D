using NUnit.Framework;
using UnityEngine;

public class FootPlacement : MonoBehaviour
{
    private Hold[] holds;//references to holds
    private Transform restingPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        holds = FindObjectsByType<Hold>(FindObjectsSortMode.None);//get references to holds
        restingPosition = transform.parent.GetChild(1);
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
        //update limb info which cascades into the foot actually moving to that hold
        //if no hold is available go to the resting position
    }
}
