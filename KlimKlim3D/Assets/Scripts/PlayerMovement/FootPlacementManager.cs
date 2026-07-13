using UnityEngine;

// DEFUNCT, dit was een stom idee -Koen

public class FootPlacementManager : MonoBehaviour
{
    private FootPlacement footPlacement1;
    private FootPlacement footPlacement2;
    private int updateCount = 0;
    void Start()
    {
        FootPlacement[] feet = FindObjectsByType<FootPlacement>(FindObjectsSortMode.None);

        // There sould be only two in a level at any time
        footPlacement1 = feet[0];
        footPlacement2 = feet[1];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int i = updateCount % 2;
        switch (i)
        {
            case 0:
                footPlacement1.AttemptToPlaceFeet();
                break;
            case 1:
                footPlacement2.AttemptToPlaceFeet();
                break;
        }
    }
}
