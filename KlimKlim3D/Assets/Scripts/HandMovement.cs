using System;
using UnityEngine;
//dit script moet gerefactord worden want het doet te veel -Koen
public class HandMovement : MonoBehaviour
{
    [SerializeField] float maxRadius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float moveSpeed = 10f;

    private Vector3 holdPosition;
    private Vector3 shoulderPosition;
    private Vector3 restingPosition;

    private LimbInfo limbInfo;
    private PlayerInput playerInput;

    private void Awake()
    {           
        shoulderPosition = transform.parent.transform.position;
        restingPosition = transform.parent.GetChild(1).position;
        limbInfo = GetComponent<LimbInfo>();
        playerInput = transform.parent.parent.GetComponent<PlayerInput>();
    }

    private void Update()
    {
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
    public void SetHold(Vector3 holdHos)
    {
        holdPosition = holdHos;
    }
    /// <summary>
    /// Moves the hand toward the mouse position and if out range the hand i s placed on the max radius in the direction of the mouse
    /// </summary>
    private void MoveTowardsMouse()
    {
        if(Vector3.Distance(shoulderPosition, playerInput.GetMousePosition()) >= maxRadius)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                GetPointOnCircle(shoulderPosition, playerInput.GetMousePosition(), maxRadius),
                moveSpeed * Time.deltaTime
            );
        }
        else
        {
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
                restingPosition,
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
            holdPosition,
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
            direction = Vector3.right; // fallback direction
        }

        direction.Normalize();
        Vector3 result = center + direction * radius;
        result.z = center.z;
        return result;
    }
}
