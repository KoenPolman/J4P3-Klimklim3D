using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandMovement : MonoBehaviour
{
    [SerializeField] HandType handType;
    [SerializeField] float maxRadius;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float moveSpeed = 10f;

    private Vector3 targetPosition;
    private Vector3 shoulderPosition;
    private Vector3 restingPosition;
    private Func<bool> mouseInput;
    private bool isHolding = true;

    private void Awake()
    {
        switch (handType)
        {
            case HandType.Right:
                mouseInput = () => Mouse.current.rightButton.IsPressed();
                break;
            case HandType.Left:
                mouseInput = () => Mouse.current.leftButton.IsPressed();
                break;
        }            
        if (mainCamera == null)
            mainCamera = Camera.main;

        targetPosition = transform.position;
        shoulderPosition = transform.parent.transform.position;
        restingPosition = transform.parent.GetChild(1).position;
    }

    private void Update()
    {
        if (!mouseInput() && isHolding)
        {
            UpdateTargetPosition();
            MoveTowardsTarget();
        }
        else
        {
            MoveToRest();
        }
    }

    private void UpdateTargetPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, groundLayer))
        {
            targetPosition = hit.point;
        }
    }

    private void MoveTowardsTarget()
    {
        if(Vector3.Distance(shoulderPosition, targetPosition) >= maxRadius)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                GetPointOnCircle(shoulderPosition, targetPosition, maxRadius),
                moveSpeed * Time.deltaTime
            );
        }
        else
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(targetPosition.x, targetPosition.y, 0),
                moveSpeed * Time.deltaTime
            );
        }
    }
    private void MoveToRest()
    {
        transform.position = Vector3.MoveTowards(
                transform.position,
                restingPosition,
                moveSpeed * Time.deltaTime
            );
    }
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
