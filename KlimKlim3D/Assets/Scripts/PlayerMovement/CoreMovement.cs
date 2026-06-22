using UnityEngine;
// Script written by Koen Polman for KlimKlim3D i.e. Master project, 2/2026 - 4/2026
public class CoreMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float leftArmLength = 1.5f;
    [SerializeField] float rightArmLength = 1.5f;
    
    private Transform leftHand;
    private Transform rightHand;
    [SerializeField] Transform leftShoulder; 
    [SerializeField] Transform rightShoulder;
    private bool enabledMove = false;

    void Start()
    {
        LimbInfo[] limbs = FindObjectsByType<LimbInfo>(FindObjectsSortMode.None);
        Debug.Log("qty of limbs found: " + limbs.Length);
        
        foreach (LimbInfo limb in limbs)
        {
            switch (limb.GetTypeStrict())
            {
                case LimbType.LeftHand:
                    leftHand = limb.transform;
                    Debug.Log("left hand assigned");
                    break;
                case LimbType.RightHand:
                    rightHand = limb.transform;
                    Debug.Log("right hand assigned");
                    break;
                // Add cases for shoulders if available
                default:
                    break;
            }
        }
    }

    void Update()
    {
        if (enabledMove)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                GetPositionBetweenHands(),
                moveSpeed * Time.deltaTime
            );
        }
    }

    /// <summary>
    /// Calculates the position between the hands respecting arm constraints
    /// </summary>
    private Vector3 GetPositionBetweenHands()
    {
        Vector3 leftHandPos = leftHand.position;
        Vector3 rightHandPos = rightHand.position;

        // Calculate the midpoint
        Vector3 midpoint = (leftHandPos + rightHandPos) / 2f;

        // Calculate actual distance between hands
        float handDistance = Vector3.Distance(leftHandPos, rightHandPos);
        
        // Maximum possible distance when both arms are fully extended
        float maxDistance = leftArmLength + rightArmLength;

        // If hands are too far apart, clamp the torso position
        if (handDistance > maxDistance)
        {
            Debug.LogWarning("Hand distance exceeds arm reach!");
            // Pull torso toward the midpoint but respect arm limits
        }

        // Apply vertical offset and return
        return midpoint - new Vector3(0, 0.75f, 0);
    }

    public void enable()
    {
        enabledMove = true;
    }
}

/*using UnityEngine;
// Script written by Koen Polman for KlimKlim3D i.e. Master project, 2/2026 - 4/2026
public class CoreMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float leftArmLength = 1.5f;
    [SerializeField] float rightArmLength = 1.5f;
    
    private Transform leftHand;
    private Transform rightHand;
    [SerializeField] Transform leftShoulder; 
    [SerializeField] Transform rightShoulder;
    private bool enabledMove = false;

    void Start()
    {
        LimbInfo[] limbs = FindObjectsByType<LimbInfo>(FindObjectsSortMode.None);
        Debug.Log("qty of limbs found: " + limbs.Length);
        
        foreach (LimbInfo limb in limbs)
        {
            switch (limb.GetTypeStrict())
            {
                case LimbType.LeftHand:
                    leftHand = limb.transform;
                    Debug.Log("left hand assigned");
                    break;
                case LimbType.RightHand:
                    rightHand = limb.transform;
                    Debug.Log("right hand assigned");
                    break;
                // Add cases for shoulders if available
                default:
                    break;
            }
        }
    }

    void Update()
    {
        if (enabledMove)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                GetPositionBetweenHands(),
                moveSpeed * Time.deltaTime
            );
        }
    }

    /// <summary>
    /// Calculates the position between the hands using shoulder positions as rotation points
    /// </summary>
    private Vector3 GetPositionBetweenHands()
    {
        Vector3 leftShoulderPos = leftShoulder.position;
        Vector3 rightShoulderPos = rightShoulder.position;
        Vector3 leftHandPos = leftHand.position;
        Vector3 rightHandPos = rightHand.position;

        // Calculate the midpoint between shoulders
        Vector3 shoulderMidpoint = (leftShoulderPos + rightShoulderPos) / 2f;

        // Calculate distances from shoulders to hands
        float leftHandDistance = Vector3.Distance(leftShoulderPos, leftHandPos);
        float rightHandDistance = Vector3.Distance(rightShoulderPos, rightHandPos);

        // Check if arms are within reach constraints
        if (leftHandDistance > leftArmLength)
        {
            Debug.LogWarning("Left hand exceeds arm reach!");
        }
        if (rightHandDistance > rightArmLength)
        {
            Debug.LogWarning("Right hand exceeds arm reach!");
        }

        // Return the shoulder midpoint as the rotation center
        return shoulderMidpoint;
    }

    public void enable()
    {
        enabledMove = true;
    }
}
*/