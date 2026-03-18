using System;
using UnityEngine;
// Script written by Koen Polman for KlimKlim3D i.e. Master project, 2/2026 - 4/2026
public class HandMovement : MonoBehaviour
{
    [SerializeField] float maxRadius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float moveSpeed = 10f;

    private Transform hold;
    [SerializeField] private Transform shoulder;
    [SerializeField] Transform resting;

    private LimbInfo limbInfo;
    private PlayerInput playerInput;

    private void Start()
    {
        //if the shoulder and resting position are not assigned they will be attemted to be looked up trough code
        if (shoulder == null)
        {
            shoulder = transform.parent.transform;
        }

        if (resting == null)
        {
            resting = transform.parent.GetChild(1);
        }

        limbInfo = GetComponent<LimbInfo>();
        playerInput = PlayerInput.Instance;
    }

    private void Update()
    {
        //what the limb does is based on the stat in limb info
        switch (limbInfo.GetState())
        {
            case LimbState.resting:
                MoveToRest();
                break;
            case LimbState.holding:
                MoveToHold();
                break;
            case LimbState.reaching:
                MoveTowardsMouse();
                break;
        }
    }
    /// <summary>
    /// Sets the hold transform so that the hand can be moved to this position
    /// </summary>
    /// <param name="newHold"></param>
    public void SetHold(Transform newHold)
    {
        hold = newHold;
    }
    /// <summary>
    /// Gets the current hold
    /// </summary>
    /// <returns></returns>
    public Transform GetHold()
    {
        return hold;
    }
    /// <summary>
    /// Gets the resting position
    /// </summary>
    /// <returns></returns>
    public Transform GetRestingPosition()
    {
        return resting;
    }
    /// <summary>
    /// Gets the max radius
    /// </summary>
    /// <returns></returns>
    public float GetMaxRadius()
    {
        return maxRadius;
    }

    /// <summary>
    /// Moves the hand toward the mouse position and if out range the hand is placed on the max radius in the direction of the mouse
    /// </summary>
    private void MoveTowardsMouse()
    {
        if (Vector3.Distance(shoulder.position, playerInput.GetMousePosition()) >= maxRadius)
        {
            // If within the max range move to the mouse position
            transform.position = Vector3.MoveTowards(
                transform.position,
                GetPointOnCircle(shoulder.position, playerInput.GetMousePosition(), maxRadius),
                moveSpeed * Time.deltaTime
            );
        }
        else
        {
            // If outside of max range move hand to position on circle around shoulder
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(playerInput.GetMousePosition().x, playerInput.GetMousePosition().y, 0),
                moveSpeed * Time.deltaTime
            );
        }
    }

    /// <summary>
    /// Moves the hand to the resting position
    /// </summary>
    private void MoveToRest()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            resting.position,
            moveSpeed * Time.deltaTime
        );
    }

    /// <summary>
    /// Moves the hand to the hold 
    /// </summary>
    /// <param name="holdPosition"></param>
    private void MoveToHold()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            hold.position,
            moveSpeed * Time.deltaTime
        );
    }

    /// <summary>
    /// Get the point on a circle in the direction of the second vector, intended to be used for hand positioning
    /// </summary>
    /// <param name="center"></param>
    /// <param name="target"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    private Vector3 GetPointOnCircle(Vector3 center, Vector3 target, float radius)
    {
        Vector3 direction = target - center;
        direction.z = 0f;

        if (direction == Vector3.zero)
        {
            direction = Vector3.right; // Fallback direction
        }

        direction.Normalize();
        Vector3 result = center + direction * radius;
        result.z = center.z;
        return result;
    }
}