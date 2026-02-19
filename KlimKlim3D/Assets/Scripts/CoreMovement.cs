using System;
using UnityEngine;

public class CoreMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    private Transform leftHand;
    private Transform rightHand;
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
                default:
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(
                transform.position,
                GetPositionBetweenHands(),
                moveSpeed * Time.deltaTime
            );
    }
    private Vector3 GetPositionBetweenHands()
    {
        return (leftHand.position - rightHand.position)/2;
    }
}
