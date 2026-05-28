using UnityEngine;
using UnityEngine.UIElements;
// Script written by Koen Polman for KlimKlim3D i.e. Master project, 2/2026 - 4/2026
public class CoreMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    private Transform leftHand;
    private Transform rightHand;
    private bool enabledMove = false;
    void Start()
    {
        // Get all the limbs
        LimbInfo[] limbs = FindObjectsByType<LimbInfo>(FindObjectsSortMode.None);
        Debug.Log("qty of limbs found: " + limbs.Length);
        foreach (LimbInfo limb in limbs)
        {
            // Identify which limbs retrieved are the hands and assign them to the correct variables
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
                default:
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (enabledMove)
        {
            case true:
                // Player torso is moved to position derived from GetPositionBetweenHands function
                transform.position = Vector3.MoveTowards(
                transform.position,
                GetPositionBetweenHands(),
                moveSpeed * Time.deltaTime
                );
                break;
            case false:
                break;
        }
        
    }
    /// <summary>
    /// Calculates the position between the hands
    /// </summary>
    /// <returns></returns>
    private Vector3 GetPositionBetweenHands()
    {
        return (leftHand.position + rightHand.position) / 2f - new Vector3(0,.75f,0);
    }
    public void enable()
    {
        enabledMove = true;
    }
}
